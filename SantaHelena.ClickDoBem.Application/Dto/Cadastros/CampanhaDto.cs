using SantaHelena.ClickDoBem.Application.Interfaces;
using System;

namespace SantaHelena.ClickDoBem.Application.Dto.Cadastros
{
    public class CampanhaDto : IAppDto
    {

        public Guid Id { get; set; }

        public DateTime DataInclusao { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string Descricao { get; set; }

        public DateTime? DataInicial { get; set; }

        public DateTime? DataFinal { get; set; }

        public int Prioridade { get; set; }

    }
}
