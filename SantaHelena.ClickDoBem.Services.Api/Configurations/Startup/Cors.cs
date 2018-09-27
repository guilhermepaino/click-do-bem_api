using Microsoft.AspNetCore.Cors.Infrastructure;
using System;

namespace SantaHelena.ClickDoBem.Services.Api.Configurations.Startup
{
    public class Cors
    {

        public static Action<CorsPolicyBuilder> Configure()
        {
            return cors =>
            {
                cors.AllowAnyHeader();
                cors.AllowAnyMethod();
                cors.AllowAnyOrigin();
            };
        }

    }
}
