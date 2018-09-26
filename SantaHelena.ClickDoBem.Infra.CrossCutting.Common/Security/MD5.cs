using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SantaHelena.ClickDoBem.Infra.CrossCutting.Common.Security
{

    /// <summary>
    /// Classe utilitária de conversão MD5
    /// </summary>
    public static class MD5
    {

        /// <summary>
        /// Gerar o hash MD5 do texto informado
        /// </summary>
        /// <param name="input">Texto para geração de hash</param>
        /// <returns></returns>
        public static byte[] HashMD5(string input)
        {

            string discard;
            return HashMD5(input, out discard);

        }

        /// <summary>
        /// Gerar o hash md5 do texto informado
        /// </summary>
        /// <param name="input">Texto para geração de hash</param>
        /// <param name="result">Variável de retorno do hash em formato de string</param>
        public static byte[] HashMD5(string input, out string result)
        {

            byte[] data = new MD5CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(input));

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
                sb.Append(data[i].ToString("x2"));

            result = sb.ToString().ToUpper();

            return data;

        }

        /// <summary>
        /// Converte uma string hexadecimal para um array de bytes
        /// </summary>
        /// <param name="hex">String hexadecimal</param>
        /// <returns>Uma instância de array de bytes</returns>
        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        /// <summary>
        /// Converte um array de bytes em uma string hexadecimal
        /// </summary>
        /// <param name="bytes">Array de bytes para conversão</param>
        /// <returns>Uma expressão contendo a string hexadecimal convertida</returns>
        public static string ByteArrayToString(byte[] bytes)
        {
            StringBuilder hex = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

    }

}
