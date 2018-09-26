using Microsoft.AspNetCore.Http;
using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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

    }
}
