using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Reflection;

namespace SwashbuckleWithUtf8JsonTest
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
            services
                .AddMvcCore()
                .AddApiExplorer()
                // note: If I comment out the AddMvcOptions section, it works
                .AddMvcOptions(option =>
                {
                    option.OutputFormatters.Clear();
                    option.OutputFormatters.Add(new Utf8JsonOutputFormatter(Utf8JsonContractResolver.Instance));

                    option.InputFormatters.Clear();
                    option.InputFormatters.Add(new Utf8JsonInputFormatter(Utf8JsonContractResolver.Instance));
                });

            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Weather API",
                    Version = "v1",
                    Description = "Weather API examples",
                    TermsOfService = new Uri("http://example.com/terms/"),
                    Contact = new OpenApiContact
                    {
                        Name = "Weatherteam",
                        Email = "Weather.net",
                        Url = new Uri("http://weather.net")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license")
                    }
                });
 
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                o.IncludeXmlComments(xmlPath);
                o.CustomSchemaIds(i => i.FullName);
            });
         //   services.AddSwaggerGenUtf8JsonSupport();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //app.UseAuthorization();

            app.UseSwagger(o =>
            {
                o.RouteTemplate = "api-docs/{documentName}/swagger.json";

                // https://docs.microsoft.com/it-it/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-5.0&tabs=visual-studio
                //o.SerializeAsV2 = true; // without this swagger not allow body on request ... 
            });
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/api-docs/v1/swagger.json", "Weather API V1");
                o.RoutePrefix = "swagger/ui";

                o.DocumentTitle = "Weather API - Swagger";
                o.DocExpansion(DocExpansion.None);
                o.DisplayRequestDuration();
                o.EnableFilter();
                o.ShowExtensions();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
