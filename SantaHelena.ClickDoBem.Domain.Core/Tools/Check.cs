namespace SantaHelena.ClickDoBem.Domain.Core.Tools
{

    /// <summary>
    /// Fornece métodos para Validações e verificações
    /// </summary>
    public static class Check
    {

        #region Documentos

        /// <summary>
        /// Verifica se o cpf informado é um número sintaticamente válido
        /// </summary>
        /// <param name="cpf">Número do cpf a ser analisado (apenas números)</param>
        /// <returns>Um booleando com o resultado do teste</returns>
        public static bool VerificarCpf(string cpf)
        {

            // Invalidar pelo tamanho
            if (cpf.Length != 11)
                return false;

            int dv;

            // Calculando o DV1
            dv = 0;
            for (int pos = 0; pos < 9; pos++)
                dv += (int.Parse(cpf.Substring(pos, 1)) * (pos + 1));

            dv = (dv % 11);
            if (dv == 10) dv = 0;

            // Testando DV1
            if (dv.ToString() != cpf.Substring(9, 1))
                return false;

            // Calculando o DV2
            dv = 0;
            for (int pos = 0; pos < 10; pos++)
                dv += (int.Parse(cpf.Substring(pos, 1)) * pos);

            dv = (dv % 11);
            if (dv == 10) dv = 0;

            // Testando DV2
            if (dv.ToString() != cpf.Substring(10, 1))
                return false;

            return true;

        }

        /// <summary>
        /// Verifica se o cpnj informado é um número sintaticamente válido
        /// </summary>
        /// <param name="cnpj">Número do cnpj a ser analisado (apenas números)</param>
        /// <returns>Um booleando com o resultado do teste</returns>
        public static bool VerificarCnpj(string cnpj)
        {

            // Invalidar pelo tamanho
            if (cnpj.Length != 14)
                return false;

            int dv;
            string[] peso = { "678923456789", "5678923456789" };

            // Calculando DV1
            dv = 0;
            for (int i = 0; i < 12; i++)
                dv += (int.Parse(cnpj.Substring(i, 1)) * int.Parse(peso[0].Substring(i, 1)));

            dv = (dv % 11);
            if (dv == 10) dv = 0;

            // Validando DV1
            if (dv.ToString() != cnpj.Substring(12, 1))
                return false;

            // Calculando DV2
            dv = 0;
            for (int i = 0; i < 12; i++)
                dv += (int.Parse(cnpj.Substring(i, 1)) * int.Parse(peso[1].Substring(i, 1)));

            dv = (dv % 11);
            if (dv == 10) dv = 0;

            // Validando DV2
            if (dv.ToString() != cnpj.Substring(13, 1))
                return false;

            return true;

        }

        /// <summary>
        /// Verifica se o documento informado é um número sintaticamente válido
        /// </summary>
        /// <param name="cpfCnpj">Número do documento, Cpf ou Cnpj (apenas números), a ser analisado</param>
        /// <returns>Um booleando com o resultado do teste</returns>
        public static bool VerificarDocumento(string cpfCnpj)
        {

            if (cpfCnpj.Length == 11)
                return VerificarCpf(cpfCnpj);
            else if (cpfCnpj.Length == 14)
                return VerificarCnpj(cpfCnpj);
            else
                return false;

        }

        #endregion

    }

}
