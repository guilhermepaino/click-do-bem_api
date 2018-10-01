using SantaHelena.ClickDoBem.Application.Interfaces;
using System;

namespace SantaHelena.ClickDoBem.Application.Dto.Cadastros
{
    public class ItemImagemDto : IAppDto
    {

        public Guid Id { get; set; }
        public DateTime DataInclusao { get; set; }

        public DateTime? DataAlteracao { get; set; }
        public string NomeOriginal { get; set; }
        public string Caminho { get; set; }

    }
}
