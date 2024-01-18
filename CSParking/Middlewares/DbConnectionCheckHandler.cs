using CSParking.Models.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Middlewares
{
    public class DbConnectionCheckHandler
    {
        private readonly RequestDelegate _next;

        public DbConnectionCheckHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var db = httpContext.RequestServices.GetRequiredService<CSParkingContext>();
            var configuration = httpContext.RequestServices.GetRequiredService<IConfiguration>();

            var isDbAvilable = await db.Database.CanConnectAsync();

            if (!isDbAvilable)
            {
                var response = configuration.GetRequiredSection("Responses").GetRequiredSection("DatabaseNotAvailable").Value ;

                if (string.IsNullOrEmpty(response))
                    throw new Exception("The \"Responses\" or \"DatabaseNotAvailable\" sections were not found in the appsettings file or their value is null");

                await httpContext.Response.WriteAsync(response);
                return;
            }

            await _next.Invoke(httpContext);
        }
    }
}
