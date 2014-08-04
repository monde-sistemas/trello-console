using System.IO;

namespace Monde
{
    static class ArquivoUtils
    {
        public static string SubstituirCaracteresInvalidosNomeArquivo(string texto, char substituirPor)
        {
            return SubstituirCaracteres(texto, Path.GetInvalidFileNameChars(), substituirPor);
        }

        public static string SubstituirCaracteresInvalidosNomeDiretorio(string texto, char substituirPor)
        {
            return SubstituirCaracteres(texto, Path.GetInvalidPathChars(), substituirPor);
        }

        public static string SubstituirCaracteres(string texto, char[] caracteres, char substituirPor)
        {
            var result = texto;
            foreach (char c in caracteres)
            {
                result = result.Replace(c, substituirPor);
            }
            return result;
        }
    }
}
