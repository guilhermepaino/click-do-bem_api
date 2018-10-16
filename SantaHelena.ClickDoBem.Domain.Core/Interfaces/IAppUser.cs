using System;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Domain.Core.Interfaces
{

    public interface IAppUser
    {

        Guid Id { get; }

        string Nome { get; }

        IEnumerable<string> Perfis { get; }

        string Login { get; }

    }

}
