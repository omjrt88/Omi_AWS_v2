using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OMI_AWS_v2.Models;

namespace OMI_AWS_v2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            #region Adding swagger Section
            // https://www.c-sharpcorner.com/article/asp-net-core-3-1-web-api-and-swagger/
            // services.AddSingleton<IPlaceInfoService, PlaceInfoService>();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Omi Testing aws with swagger",
                    Version = "v2",
                    Description = "Omi Testing aws with swagger",
                });
            });
            #endregion
            // Okta configuration
            services.Configure<OktaSettings>(Configuration.GetSection("Okta"));
            services.AddOptions();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda, Omi is testing!");
                });
            });

            app.UseAuthentication();

            #region Adding swagger Section
            app.UseSwagger();
            //app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v2/swagger.json", "Omi Testing aws with swagger"));
            app.UseSwaggerUI(c =>
            {
                string endpointUrl =
                    $"{(string.IsNullOrEmpty(Environment.GetEnvironmentVariable("AWS_LAMBDA_FUNCTION_NAME")) ? string.Empty : "/Prod")}/swagger/v2/swagger.json";
                c.SwaggerEndpoint(endpointUrl, "Omi Testing aws with swagger");
            });
            #endregion
        }
    }
}
