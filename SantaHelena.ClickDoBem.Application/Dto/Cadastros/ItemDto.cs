using SantaHelena.ClickDoBem.Application.Dto.Credenciais;
using SantaHelena.ClickDoBem.Application.Interfaces;
using System;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Application.Dto.Cadastros
{
    public class ItemDto : IAppDto
    {

        public Guid Id { get; set; }
        public DateTime DataInclusao { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public TipoItemDto TipoItem { get; set; }

        public CategoriaDto Categoria { get; set; }

        public UsuarioDto Usuario { get; set; }

        public bool Anonimo { get; set; }

        public decimal Valor { get; set; }

        public IEnumerable<ItemImagemDto> Imagens { get; set; }

    }
}
