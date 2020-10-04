using System;
using System.Linq;
using Application.Interfaces;
using AutoMapper;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Persistence;
using Swashbuckle.AspNetCore.Swagger;
using WebAPI.Common;
using WebAPI.Models;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPersistence(Configuration);
            services.AddHttpContextAccessor();

            #region Controller
            services.AddControllers(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;

            }).AddNewtonsoftJson(setupAction =>
            {
                setupAction.SerializerSettings.ContractResolver =
                   new CamelCasePropertyNamesContractResolver();
            })
             .AddXmlDataContractSerializerFormatters()
            .AddFluentValidation(s =>
            {
                s.RegisterValidatorsFromAssemblyContaining<IClientRepository>();
                s.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                s.ValidatorFactoryType = typeof(HttpContextServiceProviderValidatorFactory);
            })
            .ConfigureApiBehaviorOptions(setupAction =>
            {
                setupAction.InvalidModelStateResponseFactory = context =>
                {
                    var errorsInModelState = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();

                    var errorResponse = new ErrorDetails()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "One or more validation errors occurred."
                    };

                    foreach (var error in errorsInModelState)
                    {
                        foreach (var subError in error.Value)
                        {
                            var errorModel = new ErrorModel
                            {
                                FieldName = error.Key,
                                Message = subError
                            };

                            errorResponse.Errors.Add(errorModel);
                        }
                    }

                    return new BadRequestObjectResult(errorResponse)
                    {
                        ContentTypes = { "application/json" }
                    };
                };
            });
            #endregion

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(string.Format(@"{0}\ClientMatters.xml", AppDomain.CurrentDomain.BaseDirectory));
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ClientMatters API",
                    Description = "An API For Managing Client and their Associated Matters",
                    Contact = new OpenApiContact()
                    {
                        Email = "victorchike247@gmail.com",
                        Name = "Victor Onyebuchi",
                        Url = new Uri("https://www.github.com/CodeVee")
                    },
                });
                c.AddFluentValidationRules();
                c.SchemaFilter<ModelSchemaFilter>();

            });
            #endregion

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureExceptionHandler();
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();

            app.UseRouting();

            #region Swagger
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClientMatters API");
            });
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
