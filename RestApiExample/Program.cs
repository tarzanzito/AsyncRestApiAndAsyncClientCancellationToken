
using System;
using System.IO;
using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Hosting;
using System.Text;
using System.Diagnostics.Metrics;
using System.Collections.Generic;
using RestApiExample.Handlers;


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
                //Log.Fatal(ex, $"ERROR:[{ex.Message}]");
                return 1;
            }

            return 0;
        }

        #endregion

        #region Private static

        #endregion
    }
}


