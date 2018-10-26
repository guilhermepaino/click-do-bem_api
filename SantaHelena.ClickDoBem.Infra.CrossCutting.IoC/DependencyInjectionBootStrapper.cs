using Microsoft.Extensions.DependencyInjection;
using SantaHelena.ClickDoBem.Application.Interfaces.Cadastros;
using SantaHelena.ClickDoBem.Application.Interfaces.Credenciais;
using SantaHelena.ClickDoBem.Application.Services.Cadastros;
using SantaHelena.ClickDoBem.Application.Services.Credenciais;
using SantaHelena.ClickDoBem.Data;
using SantaHelena.ClickDoBem.Data.Context;
using SantaHelena.ClickDoBem.Data.Repositories.Cadastros;
using SantaHelena.ClickDoBem.Data.Repositories.Credenciais;
using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros;
using SantaHelena.ClickDoBem.Domain.Interfaces.Credenciais;
using SantaHelena.ClickDoBem.Domain.Services.Cadastros;
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

            // Infra.Data
            services.AddScoped<CdbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Reposity
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUsuarioLoginRepository, UsuarioLoginRepository>();
            services.AddScoped<IUsuarioDadosRepository, UsuarioDadosRepository>();
            services.AddScoped<IUsuarioPerfilRepository, UsuarioPerfilRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IDocumentoHabilitadoRepository, DocumentoHabilitadoRepository>();
            services.AddScoped<ITipoItemRepository, TipoItemRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IItemImagemRepository, ItemImagemRepository>();
            services.AddScoped<IItemMatchRepository, ItemMatchRepository>();
            services.AddScoped<ITipoMatchRepository, TipoMatchRepository>();

            // Domain
            services.AddScoped<IUsuarioDomainService, UsuarioDomainService>();
            services.AddScoped<IUsuarioDadosDomainService, UsuarioDadosDomainService>();
            services.AddScoped<IUsuarioLoginDomainService, UsuarioLoginDomainService>();
            services.AddScoped<IUsuarioPerfilDomainService, UsuarioPerfilDomainService>();
            services.AddScoped<ICategoriaDomainService, CategoriaDomainService>();
            services.AddScoped<IDocumentoHabilitadoDomainService, DocumentoHabilitadoDomainService>();
            services.AddScoped<ITipoItemDomainService, TipoItemDomainService>();
            services.AddScoped<IItemDomainService, ItemDomainService>();
            services.AddScoped<IItemImagemDomainService, ItemImagemDomainService>();
            services.AddScoped<IItemMatchDomainService, ItemMatchDomainService>();
            services.AddScoped<ITipoMatchDomainService, TipoMatchDomainService>();
            
            // Application
            services.AddScoped<IUsuarioAppService, UsuarioAppService>();
            services.AddScoped<ICategoriaAppService, CategoriaAppService>();
            services.AddScoped<IItemAppService, ItemAppService>();
            services.AddScoped<ITipoItemAppService, TipoItemAppService>();

            // Infra.CrossCutting.Common
            services.AddScoped<IAppUser, AppUser>();

        }

    }
}
