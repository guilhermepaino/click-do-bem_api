using SantaHelena.ClickDoBem.Application.Interfaces;
using System;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Application.Dto.Credenciais
{
    public class UsuarioDto : IAppDto
    {

        #region Propriedades

        public Guid Id { get; set; }

        public DateTime DataInclusao { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string CpfCnpj { get; set; }
        public string Nome { get; set; }

        #endregion

        #region Navigation

        public UsuarioLoginDto UsuarioLogin { get; set; }
        public UsuarioDadosDto UsuarioDados { get; set; }
        public IEnumerable<string> UsuarioPerfil { get; set; }

        #endregion

    }
}
