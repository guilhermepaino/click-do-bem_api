using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SantaHelena.ClickDoBem.Data.Context;
using SantaHelena.ClickDoBem.Infra.CrossCutting.IoC;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SantaHelena.ClickDoBem.Services.Api.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace SantaHelena.ClickDoBem.Services.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddHttpContextAccessor();

            services.AddDbContext<CdbContext>(options => options.UseMySql(Infra.CrossCutting.Common.EnvironmentConfigs.Database.MySqlConnectionString));

            services.AddCors();
            services.AddResponseCaching();

            #region Jwt

            IConfigurationSection jwtAppSettingOptions = Configuration.GetSection(nameof(JwtTokenOptions));
            IConfigurationSection jwtAppSettingSecurity = Configuration.GetSection(nameof(JwtTokenSecurity));
            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtAppSettingSecurity[nameof(JwtTokenSecurity.SecretKey)]));


            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(bearerOptions =>
                {
                    var paramsValidation = bearerOptions.TokenValidationParameters;

                    paramsValidation.ValidateIssuer = true;
                    paramsValidation.ValidIssuer = jwtAppSettingOptions[nameof(JwtTokenOptions.Issuer)];

                    paramsValidation.ValidateAudience = true;
                    paramsValidation.ValidAudience = jwtAppSettingOptions[nameof(JwtTokenOptions.Audience)];

                    paramsValidation.ValidateIssuerSigningKey = true;
                    paramsValidation.IssuerSigningKey = signingKey;

                    paramsValidation.RequireExpirationTime = true;
                    paramsValidation.ValidateLifetime = true;

                    paramsValidation.ClockSkew = TimeSpan.Zero;
                });

            services.Configure<JwtTokenOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtTokenOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtTokenOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser()
                    .Build());
            });

            #endregion

            #region Swagger

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

            #endregion

            DependencyInjectionBootStrapper.RegisterServices(services);

        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Click do Bem - V1");
            });

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseStaticFiles();
            app.UseResponseCaching();

            app.UseAuthentication();
            app.UseMvc();

            

        }
    }
}
