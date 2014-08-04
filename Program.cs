using System;

namespace Monde
{
    class Program
    {
        static void Main(string[] args)        
        {
            Console.WriteLine("Para sair utilize Ctrl + C.\n");            
            
            var trelloConsole = new TrelloConsole();
            var me = trelloConsole.Autenticar();

            while (true)
            {
                var board = trelloConsole.ObterBoard(me);
                var lista = trelloConsole.ObterLista(board);
                trelloConsole.SalvarCardsEmTxt(lista);
            }
       }

    }


}
