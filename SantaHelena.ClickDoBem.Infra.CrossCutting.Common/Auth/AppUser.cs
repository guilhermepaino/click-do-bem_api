using Microsoft.AspNetCore.Http;
using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SantaHelena.ClickDoBem.Infra.CrossCutting.Common.Auth
{

    public class AppUser : IAppUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AppUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Nome => _accessor.HttpContext.User.Identity.Name;

        public IEnumerable<string> Perfis => _accessor.HttpContext.User.Claims.Where(x => x.Type.ToLower().Contains("role")).Select(x => x.Value);

        Guid IAppUser.Id
        { 
            get
            {
                return Guid.Parse(_accessor.HttpContext.User.Claims.Where(x => x.Type.ToLower().Contains("hash")).Select(x => x.Value).FirstOrDefault());
            }
        }

        string IAppUser.Login => _accessor.HttpContext.User.Claims.Where(x => x.Type.ToLower().Contains("surname")).Select(x => x.Value).FirstOrDefault();
    }

}
