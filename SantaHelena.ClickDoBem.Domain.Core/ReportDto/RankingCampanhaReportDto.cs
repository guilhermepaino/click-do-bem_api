using System;

namespace SantaHelena.ClickDoBem.Domain.Core.ReportDto
{

    /// <summary>
    /// Dto de relatório de ranking de campanhas
    /// </summary>
    public class RankingCampanhaReportDto
    {

        /// <summary>
        /// Id da campanha
        /// </summary>
        public Guid CampanhaId { get; set; }

        /// <summary>
        /// Descrição da Campanha
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Pontuação atingida
        /// </summary>
        public int Pontuacao { get; set; }

    }

}
