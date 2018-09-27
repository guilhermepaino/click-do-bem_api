using SantaHelena.ClickDoBem.Domain.Core;
using SantaHelena.ClickDoBem.Domain.Entities.Credenciais;
using SantaHelena.ClickDoBem.Domain.Interfaces.Credenciais;
using System;
using System.Collections.Generic;
using System.Text;

namespace SantaHelena.ClickDoBem.Domain.Services.Credenciais
{

    /// <summary>
    /// Objeto de domínio da entidade UsuarioSenha
    /// </summary>
    public class ColaboradorDomainService : DomainServiceBase<Colaborador, IColaboradorRepository>, IColaboradorDomainService
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do domínio
        /// </summary>
        /// <param name="repository">Contexto de banco de dados</param>
        public ColaboradorDomainService(IColaboradorRepository repository) : base(repository) { }

        #endregion

        #region Métodos públicos

        /// <summary>
        /// Obter registro pelo Cpf
        /// </summary>
        /// <param name="cpf">Cpf a ser localizado</param>
        public Colaborador ObterPorCpf(string cpf) => _repository.ObterPorCpf(cpf);


        #endregion

    }

}
