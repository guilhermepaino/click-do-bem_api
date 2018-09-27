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
    /// Objeto de serviço de colaborador
    /// </summary>
    public class ColaboradorAppService : IColaboradorAppService
    {

        #region Objetos/Variáveis Locais

        protected readonly IUnitOfWork _uow;
        protected readonly IColaboradorDomainService _dmn;

        #endregion

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do ApplicationService
        /// </summary>
        public ColaboradorAppService
        (
            IUnitOfWork uow,
            IColaboradorDomainService dmn
        )
        {
            _uow = uow;
            _dmn = dmn;
        }

        #endregion


        #region Métodos Públicos

        /// <summary>
        /// Obter registro pelo Cpf
        /// </summary>
        /// <param name="cpf">Cpf a ser localizado</param>
        public ColaboradorAppViewModel ObterPorCpf(string cpf)
        {

            ColaboradorAppViewModel modelResult = new ColaboradorAppViewModel();

            Colaborador entidade = _dmn.ObterPorCpf(cpf);

            if (entidade != null)
            {
                modelResult.Id = entidade.Id;
                modelResult.DataInclusao = entidade.DataInclusao;
                modelResult.DataAlteracao = entidade.DataAlteracao;
                modelResult.Cpf = entidade.Cpf;
                modelResult.Ativo = entidade.Ativo;
            }

            return modelResult;

        }

        /// <summary>
        /// Obter todos os registros
        /// </summary>
        public IEnumerable<ColaboradorAppViewModel> ObterTodos()
        {

            IEnumerable<Colaborador> result = _dmn.ObterTodos();
            if (result == null)
                return null;

            return
                (
                    from r in result
                    select new ColaboradorAppViewModel()
                    {
                        Id = r.Id,
                        DataInclusao = r.DataInclusao,
                        DataAlteracao = r.DataAlteracao,
                        Cpf = r.Cpf,
                        Ativo = r.Ativo
                    }

                ).ToList();

        }

        #endregion

    }
}
