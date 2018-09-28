using SantaHelena.ClickDoBem.Application.Interfaces.Credenciais;
using SantaHelena.ClickDoBem.Application.ViewModels.Credenciais;
using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Entities.Credenciais;
using SantaHelena.ClickDoBem.Domain.Interfaces.Credenciais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SantaHelena.ClickDoBem.Application.Services.Credenciais
{

    /// <summary>
    /// Objeto de serviço de Usuario
    /// </summary>
    public class UsuarioAppService : IUsuarioAppService
    {

        #region Objetos/Variáveis Locais

        protected readonly IUnitOfWork _uow;
        protected readonly IUsuarioDomainService _dmn;

        #endregion

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do ApplicationService
        /// </summary>
        public UsuarioAppService
        (
            IUnitOfWork uow,
            IUsuarioDomainService dmn
        )
        {
            _uow = uow;
            _dmn = dmn;
        }

        #endregion


        #region Métodos Públicos
        

        /// <summary>
        /// Obter todos os registros
        /// </summary>
        public IEnumerable<UsuarioAppViewModel> ObterTodos()
        {

            IEnumerable<Usuario> result = _dmn.ObterTodos();
            if (result == null)
                return null;

            return
                (
                    from r in result
                    select new UsuarioAppViewModel()
                    {
                        Id = r.Id,
                        DataInclusao = r.DataInclusao,
                        DataAlteracao = r.DataAlteracao,
                        Nome = r.Nome
                    }

                ).ToList();

        }

        /// <summary>
        /// Autenticar usuário através de usuário e senha
        /// </summary>
        /// <param name="usuario">Nome do usuário</param>
        /// <param name="senha">Senha do usuário (Hash Md5)</param>
        /// <param name="mensagem">Mensagem de saída do resultado da autenticação</param>
        public bool Autenticar(string usuario, string senha, out string mensagem)
        {

            Usuario usr = _dmn.ObterPorLogin(usuario, senha);
            if (usr == null)
            {
                mensagem = "Usuário e/ou senha inválido!";
                return false;
            }
            mensagem = "Usuário autenticado";
            return true;
        }

        #endregion

    }
}
