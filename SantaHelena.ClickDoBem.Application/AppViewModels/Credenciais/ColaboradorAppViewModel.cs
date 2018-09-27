using System;
using System.Collections.Generic;
using System.Text;

namespace SantaHelena.ClickDoBem.Application.ViewModels.Credenciais
{
    public class ColaboradorAppViewModel 
    {

        #region Propriedades

        public Guid Id { get; set; }

        public DateTime DataInclusao { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string Cpf { get; set; }

        public bool Ativo { get; set; }

        #endregion

    }
}
