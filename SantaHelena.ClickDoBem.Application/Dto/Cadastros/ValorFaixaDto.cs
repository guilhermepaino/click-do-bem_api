using SantaHelena.ClickDoBem.Application.Interfaces;
using System;

namespace SantaHelena.ClickDoBem.Application.Dto.Cadastros
{
    public class ValorFaixaDto : IAppDto
    {

        public Guid Id { get; set; }

        public string Descricao { get; set; }

        public decimal ValorInicial { get; set; }

        public decimal ValorFinal { get; set; }

        public bool Inativo { get; set; }

    }
}
