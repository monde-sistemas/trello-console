using System;
using System.IO;

namespace Monde
{    
    static class ConsoleUtils
    {
        public static int ObterInteiro()
        {
            var valido = false;
            int indice;
            do
            {
                Console.WriteLine("informe um número:");
                var input = Console.ReadLine();
                valido = int.TryParse(input, out indice);
            } while (!valido);
            return indice;
        }

        public static string ObterString(string mensagem)
        {
            while (true)
            {
                Console.WriteLine(mensagem);
                Console.WriteLine("Utilize o botão direito para colar.");
                var texto = Console.ReadLine();
                if (texto == "")
                {
                    Console.WriteLine("Valor inválido.");
                    continue;
                }
                return texto;
            }
        }

        public static string ObterCaminho()
        {
            var pasta = "";
            while (true)
            {
                pasta = ObterString(@"Informe um caminho completo, ex: C:\ArquivosTxt\");
                pasta = ArquivoUtils.SubstituirCaracteresInvalidosNomeDiretorio(pasta, '_');

                if (!Path.IsPathRooted(pasta))
                {
                    Console.WriteLine("Caminho inválido.");
                    continue;
                }

                Directory.CreateDirectory(pasta);
                return pasta;
            }
        }
    }
}
