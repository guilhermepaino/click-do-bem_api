using SantaHelena.ClickDoBem.Application.Interfaces;
using System;

namespace SantaHelena.ClickDoBem.Application.Dto.Cadastros
{
    public class CategoriaDto : IAppDto
    {

        public Guid Id { get; set; }

        public DateTime DataInclusao { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string Descricao { get; set; }

        public int Pontuacao { get; set; }

        public bool GerenciadaRh { get; set; }

    }
}
