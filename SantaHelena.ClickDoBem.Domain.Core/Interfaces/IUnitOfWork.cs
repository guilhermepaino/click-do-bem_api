using System;

namespace SantaHelena.ClickDoBem.Domain.Core.Interfaces
{

    public interface IUnitOfWork : IDisposable
    {
        void Efetivar();
    }

}
