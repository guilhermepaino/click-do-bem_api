using SantaHelena.ClickDoBem.Application.Interfaces;
using SantaHelena.ClickDoBem.Domain.Core.Interfaces;

namespace SantaHelena.ClickDoBem.Application.Services
{

    public abstract class AppServiceBase<TDto, TEntity> : IAppServiceBase
        where TDto : IAppDto
        where TEntity : IEntity
    {

        protected abstract TDto ConverterEntidadeEmDto(TEntity entidade);

    }

}