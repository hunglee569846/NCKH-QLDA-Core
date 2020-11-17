using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NCKH.Core.Infrastructure.AutofacModules;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Linq;

namespace QLDA.Core.API
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
            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                services.AddCors();
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    //TermsOfService = "http://daotao.humg.edu.vn/",
                    Version = "v1",
                    Title = "NCKH-QLDA API",
                    Description = "CÔNG TY CỔ PHẦN PHẦN MỀM HUMG",
                    Contact = new OpenApiContact
                    {
                        Name = "HUMG",
                        Email = string.Empty,
                        Url = new Uri("http://daotao.humg.edu.vn/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Về chúng tôi.",
                        Url = new Uri("http://daotao.humg.edu.vn/")
                    },
                });
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ApplicationModule(Configuration.GetConnectionString("WebsiteConnection")));
            builder.RegisterModule(new ValidationModule());
            
         }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            #region Allow Origins
            var allowOrigins = Configuration.GetSection("AllowOrigins")
                .GetChildren().Select(x => x.Value).ToArray();
            app.UseCors(builder =>
            {
                builder.WithOrigins(allowOrigins);
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowCredentials();
            });
            #endregion
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "HUMG API V1");
                ////options.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");
                options.RoutePrefix = "api";
                options.DocumentTitle = "Website Api Documentation";
                options.DisplayOperationId();
                options.DisplayRequestDuration();
                options.EnableDeepLinking();
                //options.MaxDisplayedTags(20);
                options.EnableFilter();
                options.ShowExtensions();
                options.DocExpansion(DocExpansion.None);
            });

        }
    }
}
