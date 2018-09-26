using Microsoft.Extensions.DependencyInjection;
using SantaHelena.ClickDoBem.Data;
using SantaHelena.ClickDoBem.Data.Context;
using SantaHelena.ClickDoBem.Data.Repositories.Credenciais;
using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Interfaces.Credenciais;
using SantaHelena.ClickDoBem.Domain.Services.Credenciais;
using SantaHelena.ClickDoBem.Infra.CrossCutting.Common.Auth;

namespace SantaHelena.ClickDoBem.Infra.CrossCutting.IoC
{

    /// <summary>
    /// Classe de serviço de injeção de dependência
    /// </summary>
    public class DependencyInjectionBootStrapper
    {

        /// <summary>
        /// Registrar os serviços de dependência
        /// </summary>
        /// <param name="services">Coleção de serviço</param>
        public static void RegisterServices(IServiceCollection services)
        {

            // Application

            // Domain
            services.AddScoped<IUsuarioDomainService, UsuarioDomainService>();
            services.AddScoped<IUsuarioSenhaDomainService, UsuarioSenhaDomainService>();
            services.AddScoped<IColaboradorDomainService, ColaboradorDomainService>();

            // Infra.Data
            services.AddScoped<CdbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUsuarioSenhaRepository, UsuarioSenhaRepository>();
            services.AddScoped<IColaboradorRepository, ColaboradorRepository>();

            // Infra.CrossCutting.Common
            services.AddScoped<IAppUser, AppUser>();

        }

    }
}
