using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SantaHelena.ClickDoBem.Data.Context;
using SantaHelena.ClickDoBem.Infra.CrossCutting.IoC;
using SantaHelena.ClickDoBem.Services.Api.Configurations.Startup;
using Swashbuckle.AspNetCore.Swagger;

namespace SantaHelena.ClickDoBem.Services.Api
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddHttpContextAccessor(); // TODO: Verificar IoC (Testes)

            services.AddDbContext<CdbContext>(options => options.UseMySql(Infra.CrossCutting.Common.EnvironmentConfigs.Database.MySqlConnectionString));

            //services.AddAuthentication("Bearer")
            //    .AddJwtBearer(JwtHeader.ConfigureService());

            services.AddCors();
            services.AddResponseCaching();
            //services.AddMvc(Mvc.ConfigureService());
            //services.AddAuthorization(Authorization.ConfigureService());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Santa Helena - Click do Bem - API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme { In = "header", Description = "Por favor, digite JWT com Bearer no campo", Name = "Authorization", Type = "apiKey" });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    {
                        "Bearer", Enumerable.Empty<string>()
                    }
                });
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlFile = $"ClickDoBemApi.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            DependencyInjectionBootStrapper.RegisterServices(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            //loggerFactory.AddLogstashLogger(app.ApplicationServices);

            app.UseStaticFiles();
            app.UseCors(Cors.Configure());
            app.UseAuthentication();
            app.UseResponseCaching();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Click do Bem - V1");
            });

        }
    }
}
