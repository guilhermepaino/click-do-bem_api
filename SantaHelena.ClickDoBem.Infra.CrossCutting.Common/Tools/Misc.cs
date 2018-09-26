using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SantaHelena.ClickDoBem.Infra.CrossCutting.Common.Tools
{

    /// <summary>
    /// Fornece métodos para funções diversas
    /// </summary>
    public static class Misc
    {

        /// <summary>
        /// Remove o caminho da expressão deixando apenas o nome do arquivo
        /// </summary>
        /// <param name="path">Expressão contendo o caminho e o nome do arquivo</param>
        /// <returns>Um System.String contendo apenas o nome do arquivo</returns>
        public static string RemovePath(string path)
        {
            if (path.Contains('\\'))
                return path.Split('\\').Last<string>();
            else
                return path;
        }

        /// <summary>
        /// Retorna a base do caminho de rede
        /// </summary>
        /// <param name="fullPath">Caminho completo de rede</param>
        /// <returns>Uma string contendo a base do caminho de rede</returns>
        public static string NetworkPath(string fullPath)
        {
            List<string> lCaminho = fullPath.Split('\\').ToList<string>();

            if (lCaminho.Count >= 4)
                return @"\\" + lCaminho[2] + "\\" + lCaminho[3];
            else
                return string.Empty;
        }

        /// <summary>
        /// Limpar a expressão fornecida deixando apenas os números
        /// </summary>
        /// <param name="expressao">Expressão a ser limpa</param>
        /// <returns>Uma expressão contendo o resultado da limpeza de caracteres efetuada</returns>
        public static string ClearNumber(string expressao)
        {

            StringBuilder ret = new StringBuilder();

            for (int pos = 0; pos < expressao.Length; pos++)
            {
                string caractere = expressao.Substring(pos, 1);
                if ("0123456789".IndexOf(caractere) >= 0)
                    ret.Append(caractere);
            }

            return ret.ToString();

        }

        /// <summary>
        /// Limpar a expressão fornecida deixando apenas letras
        /// </summary>
        /// <param name="expression">Expressão a ser limpa</param>
        /// <returns>Uma expressão contendo o resultado da limpeza de caracteres efetuada</returns>
        public static string ClearText(string expression)
        {
            return ClearText(expression, false);
        }

        /// <summary>
        /// Limpar a expressão fornecida deixando apenas letras
        /// </summary>
        /// <param name="expression">Expressão a ser limpa</param>
        /// <param name="keepSpace">Booleando indicando se o espaço deve também ser mantido</param>
        /// <returns>Uma expressão contendo o resultado da limpeza de caracteres efetuada</returns>
        public static string ClearText(string expression, bool keepSpace)
        {
            return ClearText(expression, (keepSpace ? " " : string.Empty));
        }

        /// <summary>
        /// Limpar a expressão fornecida deixando apenas letras
        /// </summary>
        /// <param name="expression">Expressão a ser limpa</param>
        /// <param name="charsToKeep">Lista de characteres a serem mantidos</param>
        /// <returns>Uma expressão contendo o resultado da limpeza de caracteres efetuada</returns>
        public static string ClearText(string expression, string charsToKeep)
        {

            StringBuilder ret = new StringBuilder();

            string validos = string.Format("ABCDEFGHIJKLMNOPQRSTUVWXYZ{0}", charsToKeep);
            foreach (char c in expression.ToUpper().ToCharArray())
            {
                if (validos.Contains<char>(c))
                    ret.Append(c);
            }

            return ret.ToString();

        }

        /// <summary>
        /// Realiza uma tentativa de conversão do objeto de entrada para o tipo de saída informado
        /// </summary>
        /// <typeparam name="T">Tipo de objeto de saída</typeparam>
        /// <param name="obj">Objeto de entrada</param>
        /// <param name="result">objeto de saída</param>
        /// <returns>Uma objeto com o resultado da conversão</returns>
        public static bool TryCast<T>(object obj, out T result)
        {

            try
            {
                result = (T)Convert.ChangeType(obj, typeof(T));
                return true;
            }
            catch
            {

                result = default(T);
                return false;
            }
        }

    }

}
