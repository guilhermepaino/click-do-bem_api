using SantaHelena.ClickDoBem.Application.Interfaces;
using System;

namespace SantaHelena.ClickDoBem.Application.Dto.Credenciais
{
    public class UsuarioDadosDto : IAppDto
    {
        public Guid Id { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public string TelefoneCelular { get; set; }
        public string TelefoneFixo { get; set; }
        public string Email { get; set; }
    }
}
