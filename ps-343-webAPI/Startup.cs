using AutoMapper;
using CourseLibrary.API.DbContexts;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace CourseLibrary.API
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(config =>
           {
               // 02/26/2022 10:51 pm - SSN - [20220226-2222] - [001] - M03-13 - Demo: Working with content negotiation and output formatter
               config.ReturnHttpNotAcceptable = true;
               // config.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());

           })
                // Ad of 2.2, use:
                .AddXmlDataContractSerializerFormatters()
                ;



            // 02/27/2022 07:12 pm - SSN - [20220227-1912] - [001] - M04-05 - Demo: Adding AutoMapper to our project
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



            services.AddScoped<ICourseLibraryRepository, CourseLibraryRepository>();

            // 02/26/2022 04:11 am - SSN - [20220226-0348] - [002] - M02-05 Demo - Creating an API project
            // databaseConnectionString
            string databaseConnectionString = Environment.GetEnvironmentVariable("ps-343-connectionstring");

            if (string.IsNullOrWhiteSpace(databaseConnectionString))
            {
                throw new Exception("\n\nps-343-webApp:Startup: Environment variable 'ps-343-connectionstring' is not defined.\n\n");
            }

            services.AddDbContext<CourseLibraryContext>(options =>
            {
                options.UseSqlServer(databaseConnectionString);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
