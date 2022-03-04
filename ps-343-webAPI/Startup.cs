using AutoMapper;
using CourseLibrary.API.DbContexts;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
//using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
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


                // 03/03/2022 11:16 am - SSN - [20220302-1116] - [003] - M08-11 -Demo:  Partially updating a resource
                // Must be added before AddXmlDataContractSerializerFormatters
                .AddNewtonsoftJson(config =>
                {
                    config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })

                // Ad of 2.2, use:
                .AddXmlDataContractSerializerFormatters()



                // 03/01/2022 03:27 pm - SSN - [20220301-1526] - [001] - M07-09 - Demo: Customizing validation error responses
                .ConfigureApiBehaviorOptions(config =>
                    {
                        config.InvalidModelStateResponseFactory = context =>
                        {
                            var problemDetailsFactory = context.HttpContext.RequestServices
                                    .GetRequiredService<ProblemDetailsFactory>();

                            var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                                                        context.HttpContext,
                                                        context.ModelState
                                                        );
                            problemDetails.Detail = "See the errors field for defaults. (20220301-1534)";
                            problemDetails.Instance = context.HttpContext.Request.Path;

                            Type type_test = context.GetType();

                            var actionExecutingContext = context as Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;


                            // 03/03/2022 07:50 pm - SSN - Fix for context as ActionContent. Applies on PartiallyUpdateCourse
                            var actionContext = context as Microsoft.AspNetCore.Mvc.ActionContext;



                            if (
                                    context.ModelState.ErrorCount > 0 &&
                                    (
                                            actionExecutingContext?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count
                                     ||
                                            ((!actionContext?.ModelState.IsValid) ?? false)
                                    )
                                )
                            {
                                problemDetails.Type = "https://localhost:51044/modelvalidationproblem";
                                problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                                problemDetails.Title = "One or more validation errors occurred. (20220301-1542)";

                                return new UnprocessableEntityObjectResult(problemDetails)
                                {
                                    ContentTypes = { "application/problem+json" }
                                };

                            }

                            problemDetails.Status = StatusCodes.Status400BadRequest;
                            problemDetails.Title = "One or more errors on input occurred. (20220301-1546)";
                            return new BadRequestObjectResult(problemDetails)
                            {
                                ContentTypes = { "application/problem+json" }
                            };



                        };
                    })


;



            // 02/27/2022 07:12 pm - SSN - [20220227-1912] - [001] - M04-05 - Demo: Adding AutoMapper to our project
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



            services.AddScoped<ICourseLibraryRepository, CourseLibraryRepository>();

            // 02/26/2022 04:11 am - SSN - [20220226-0348] - [002] - M02-05 Demo - Creating an API project
            // databaseConnectionString
            // c:>setx ps-343-connectionstring = "<ConnectionString>"
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
            else
            {
                // 02/27/2022 11:54 pm - SSN - [20220227-2336] - [001] - M04-09 - Demo: Handling faults
                app.UseExceptionHandler(builder =>
                   {
                       builder.Run(async context =>
                       {
                           // Need to log error.
                           await context.Response.WriteAsync("20220227-2357: ps-343-webAPI - Unexpected error.  Please try again later.");
                       });
                   });
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
