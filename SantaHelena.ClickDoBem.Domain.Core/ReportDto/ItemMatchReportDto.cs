using System;

namespace SantaHelena.ClickDoBem.Domain.Core.ReportDto
{
    public class ItemMatchReportDto
    {

        public Guid Id { get; set; }

        public DateTime Data { get; set; }

        public string TipoMatch { get; set; }

        public string NomeDoador { get; set; }

        public string NomeReceptor { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public string Categoria { get; set; }

        public int Pontuacao { get; set; }
        public decimal Valor { get; set; }

        public bool GerenciadaRh { get; set; }

        public bool Efetivado { get; set; }

    }

}
