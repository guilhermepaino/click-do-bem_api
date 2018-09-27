using System;
using System.Collections.Generic;
using System.Text;

namespace SantaHelena.ClickDoBem.Application.ViewModels.Credenciais
{
    public class UsuarioAppViewModel 
    {

        #region Propriedades

        public Guid Id { get; set; }

        public DateTime DataInclusao { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string Nome { get; set; }

        #endregion

    }
}
