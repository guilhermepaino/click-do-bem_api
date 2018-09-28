﻿using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Entities.Credenciais;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Domain.Interfaces.Credenciais
{
    public interface IUsuarioRepository : IMySqlRepositoryBase<Usuario>
    {
        Usuario ObterPorLogin(string login, string senha);
    }
}
