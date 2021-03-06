﻿using SantaHelena.ClickDoBem.Domain.Core;
using SantaHelena.ClickDoBem.Domain.Entities.Credenciais;
using SantaHelena.ClickDoBem.Domain.Interfaces.Credenciais;
using System;
using System.Collections.Generic;
using System.Text;

namespace SantaHelena.ClickDoBem.Domain.Services.Credenciais
{

    /// <summary>
    /// Objeto de domínio da entidade UsuarioLogin
    /// </summary>
    public class UsuarioLoginDomainService : DomainServiceBase<UsuarioLogin, IUsuarioLoginRepository>, IUsuarioLoginDomainService
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do domínio
        /// </summary>
        /// <param name="repository">Contexto de banco de dados</param>
        public UsuarioLoginDomainService(IUsuarioLoginRepository repository) : base(repository) { }

        #endregion

    }

}
