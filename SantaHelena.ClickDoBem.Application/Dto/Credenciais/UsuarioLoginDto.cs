using SantaHelena.ClickDoBem.Application.Interfaces;
using System;

namespace SantaHelena.ClickDoBem.Application.Dto.Credenciais
{
    public class UsuarioLoginDto : IAppDto
    {

        public string Login { get; set; }

        public string Senha { get; set; }

    }
}
