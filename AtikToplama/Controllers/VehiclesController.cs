using AutoMapper;
using DataAccess.Uow;
using Entity.Entities;
using Entity.EntityAuto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtikToplama.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<WeatherForecast> _logger;
        private readonly IMapper _mapper;
        public VehiclesController(ILogger<WeatherForecast> logger,IUnitOfWork unitOfWork, IMapper mapper )
        {
            this._mapper = mapper;
            this.unitOfWork = unitOfWork;
            _logger = logger;
        }

        //AutoMapper ile maplendi.
        // Bütün Araçları listelemek için kullanılır.
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var result = await unitOfWork.Vehicle.GetAll();
            if (result is null)
            {
                return NotFound();
            }
             
            var resultAuto =  _mapper.Map<IEnumerable<Vehicle>,IEnumerable< VehicleAuto>>(result);

            return Ok(resultAuto);
        }

        //AutoMapper ile maplendi.
        // Yeni Araç eklemek için kullanılır
        [HttpPost]
        public async Task<IActionResult> AddVehicle([FromBody]VehicleAuto vehicle )
        {
            var result = _mapper.Map<VehicleAuto, Vehicle>(vehicle);
            await unitOfWork.Vehicle.Add(result);
            unitOfWork.Complete();
            
            return Ok(result);
        }

        //AutoMapper ile maplendi.
        // Aracı güncellemek için kullanılır.
        [HttpPut]
        public async Task<IActionResult> UpdateVehicle([FromBody] Vehicle vehicle)
        {
            var result = await unitOfWork.Vehicle.Update(vehicle);
            unitOfWork.Complete();

            return Ok(result);
        }
        //AutoMapper ile maplendi.
        // Aracı silmek için kullanılır
        [HttpDelete]
        public async Task<IActionResult> DeleteVehicle([FromQuery] long id)
        {
            var result = await unitOfWork.Vehicle.DeleteVehicleWithCons(id);
            unitOfWork.Complete();

            return Ok(result);
        }

        //AutoMapper ile maplendi.
        // Aracın gezmesi gereken bütün containerları araç bilgisi ile birlikte getirir.
        [HttpGet("VehiclesDetail")]
        public async Task<IActionResult> GetVehiclesDetail([FromQuery] long id)
        {
            var result =  unitOfWork.Vehicle.GetVehicleWithCons(id);
            return Ok(result);

        }
        
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] long id)
        {
            var vehicle = await unitOfWork.Vehicle.GetById(id);

            if (vehicle is null)
            {
                return NotFound();
            }

            var entity = _mapper.Map<Vehicle, VehicleAuto>(vehicle);
            

            return Ok(entity);
        }

    }
}
