using AutoMapper;
using Entity.Entities;
using Entity.EntityAuto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Vehicle, VehicleAuto>()
                .ForMember(x => x.NameOfPlate, opt => opt.MapFrom(y => y.VehicleName))
                .ForMember(x => x.NameOfVehicle, opt => opt.MapFrom(y => y.VehiclePlate));
            CreateMap<VehicleAuto, Vehicle>()
                .ForMember(x => x.VehicleName, opt => opt.MapFrom(y => y.NameOfPlate))
                .ForMember(x => x.VehiclePlate, opt => opt.MapFrom(y => y.NameOfVehicle));
            CreateMap<Container, ContainerAuto>()
                .ForMember(x => x.NameOfContainer, opt => opt.MapFrom(y => y.ContainerName))
                .ForMember(x => x.LatitudeOfContainer, opt => opt.MapFrom(y => y.Latitude))
                .ForMember(x => x.LongitudeOfContainer, opt => opt.MapFrom(y => y.Longitude));
            CreateMap<ContainerAuto, Container>()
                .ForMember(x => x.ContainerName, opt => opt.MapFrom(y => y.NameOfContainer))
                .ForMember(x => x.Latitude, opt => opt.MapFrom(y => y.LatitudeOfContainer))
                .ForMember(x => x.Longitude, opt => opt.MapFrom(y => y.LongitudeOfContainer));
        }
    }
}
