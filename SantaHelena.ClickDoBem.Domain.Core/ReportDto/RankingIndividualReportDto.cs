using System;

namespace SantaHelena.ClickDoBem.Domain.Core.ReportDto
{

    /// <summary>
    /// Dto de relatório de ranking
    /// </summary>
    public class RankingIndividualReportDto
    {

        /// <summary>
        /// Id do usuário
        /// </summary>
        public Guid UsuarioId { get; set; }

        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Pontuação atingida
        /// </summary>
        public int Pontuacao { get; set; }

    }

}
