
using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestApiExample.Handlers;
using static System.Net.WebRequestMethods;

namespace RestApiExample
{
    internal static class Program
    {
        #region Public static 

        public static int Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args); //native

                var apiUrl = builder.Configuration["URLS"]; //only work in debug mode
                var apiUrl1 = builder.Configuration["Kestrel:Endpoints:HttpEndpoint:Url"];
                var apiUrl2 = builder.Configuration["Kestrel:Endpoints:HttpsEndpoint:Url"];

                Console.WriteLine("URL0:" + apiUrl);
                Console.WriteLine("URL1:" + apiUrl1);
                Console.WriteLine("URL2:" + apiUrl2);

                //var configurationSection = builder.Configuration.GetChildren();
                //foreach(var x in configurationSection)
                //    Console.WriteLine(x.Key + "|" + x.Path + "|" + x.Value);


                //Add services to the container.
                //builder.Services.AddAuthorization(); //native

                //Add Dependency injection

                //Global Exception
                builder.Services.AddProblemDetails(); //NET8 ref001:GlobalException
                builder.Services.AddExceptionHandler<GlobalExceptionHandler>(); //NET8 ref001:GlobalException

                //Controllers
                builder.Services.AddControllers(); //native

                //Swagger/OpenAPI
                builder.Services.AddEndpointsApiExplorer(); //native
                builder.Services.AddSwaggerGen(); //native

                //app
                var app = builder.Build();

                app.UseExceptionHandler(); //NET8 ref001:GlobalException

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger(); //native
                    app.UseSwaggerUI(); //native
                }

                app.UseHttpsRedirection(); //native

                //app.UseAuthorization(); //native

                app.MapControllers(); //native

                app.Run(); //native
  
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Main method error:[{ex.Message}]");
                return 1;
            }

            return 0;
        }

        #endregion

        #region Private static

        #endregion
    }
}


