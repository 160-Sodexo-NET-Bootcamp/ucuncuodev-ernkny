using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Core.MiddleWare
{
    public class HeartBeatMiddleware
    {
        private readonly RequestDelegate next;

        public HeartBeatMiddleware(RequestDelegate next)
        {
            this.next = next;
        }


        public async Task Invoke (HttpContext context)
        { 
            // Gelen Controller isteği GetById methodu ise controller bloklanır.
            var requestQuery = context.Request.Path.StartsWithSegments("/api/Vehicles/GetById");
            if (requestQuery)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Selamın Aleyküm Adım Azrail");
                return;
            }
           
            // do job 
            await next.Invoke(context);
        }
    }
}
