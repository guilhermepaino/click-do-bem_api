using System;
using System.Text;

namespace SantaHelena.ClickDoBem.Domain.Core.Tools
{

    /// <summary>
    /// Fornece métodos utilitários para Data e Hora
    /// </summary>
    public static class DateAndTime
    {

        /// <summary>
        /// Identifica o número correspondente ao último dia do mês da data informada
        /// </summary>
        /// <param name="data">Data a ser analisada</param>
        /// <returns>Retorna o número correspondente ao último dia do mês da data informada</returns>
        public static int UltimoDiaDoMes(DateTime data)
        {
            return DateTime.DaysInMonth(data.Year, data.Month);
        }

        /// <summary>
        /// Calcular a idade com base na data informada
        /// </summary>
        /// <param name="nascimento">Data de nascimento para cálculo de idade</param>
        /// <returns>Uma expressão indicando a idade por extenso (ex: 11 anos, 3 meses e 4 dias) </returns>
        public static string Idade(DateTime nascimento)
        {

            StringBuilder retorno = new StringBuilder();

            DateTime hoje = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime dataBase = nascimento;
            TimeSpan diferenca;

            int anos = 0;
            int meses = 0;
            int dias = 0;

            // ---------------------------------------------------------------
            // Calculando os anos
            // ---------------------------------------------------------------
            diferenca = hoje.Subtract(dataBase);
            if (diferenca.Days > 365)
            {

                anos = (hoje.Year - nascimento.Year);

                if (nascimento.Month > hoje.Month)
                    anos--;
                else
                    if (nascimento.Month == hoje.Month)
                    if (nascimento.Day > hoje.Day)
                        anos--;

                dataBase = dataBase.AddYears(anos);

            }

            // ---------------------------------------------------------------
            // Calculando os meses
            // ---------------------------------------------------------------
            meses = (hoje.Month - dataBase.Month);
            if (meses <= 0)
            {
                if (meses == 0)
                {
                    if (hoje.Day < nascimento.Day)
                        meses = 11;
                }
                else
                {
                    meses = (12 + meses);
                }
            }

            dataBase = dataBase.AddMonths(meses);

            // ---------------------------------------------------------------
            // Calculando os dias
            // ---------------------------------------------------------------
            dias = (hoje.Day - nascimento.Day);
            if (dias > 0)
                dataBase = dataBase.AddDays(dias);
            else if (dias < 0)
            {
                dias = (30 + dias);
                dataBase = new DateTime(hoje.Year, hoje.Month, hoje.Day);
            }

            if (hoje == dataBase)
            {

                // Anos
                if (anos > 0)
                {
                    retorno.Append(anos);
                    retorno.Append(" ano");
                    if (anos > 1) retorno.Append("s");
                }

                // Meses
                if (meses > 0)
                {

                    if (retorno.Length > 0)
                        retorno.Append(((dias == 0) ? " e " : ", "));

                    retorno.Append(meses);

                    if (meses == 0)
                        retorno.Append(" mês");
                    else
                        retorno.Append(" meses");

                }

                // Dias
                if (dias > 0)
                {

                    if (dias == 30)
                        dias = 29;

                    if (retorno.Length > 0)
                        retorno.Append(" e ");

                    retorno.Append(dias);
                    retorno.Append(" dia");

                    if (dias > 1)
                        retorno.Append("s");

                }

            }
            else
            {
                throw new InvalidOperationException("Falha no cálculo da idade");
            }

            return retorno.ToString();

        }

    }

}
