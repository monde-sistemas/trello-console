using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TrelloNet;

namespace Monde
{
    class TrelloConsole
    {
        const string APIKey = "< YOUR TRELLO API KEY >";
        const string APPName = "< APPLICATION NAME >";

        ITrello trello;
        public TrelloConsole()
        {
            this.trello = new Trello(APIKey);
        }

        public Member Autenticar()
        {
            var url = this.trello.GetAuthorizationUrl(APPName, Scope.ReadOnly, Expiration.OneHour);
            Process.Start(url.AbsoluteUri);

            var token = "";
            while (token == "")
                token = ConsoleUtils.ObterString("Informe o token gerado pelo Trello:");
            
            this.trello.Authorize(token);
            
            var me = this.trello.Members.Me();
            Console.WriteLine(String.Format("Bem vindo, {0}.", me.FullName));

            return me;
        }

        public Board ObterBoard(IMemberId pessoa)
        {
            Console.WriteLine("\nBoards:");
            IEnumerable<Board> allMyBoards = this.trello.Boards.ForMember(pessoa);

            var i = 0;
            foreach (Board b in allMyBoards)
            {
                Console.WriteLine(String.Format("{0} - {1}", i, b.Name));
                i += 1;
            }

            return allMyBoards.ElementAt(ConsoleUtils.ObterInteiro());
        }

        public List ObterLista(Board board)
        {
            Console.WriteLine(String.Format("\nListas da board [{0}]:", board.Name));
            IEnumerable<List> listas = trello.Lists.ForBoard(board);

            var i = 0;
            foreach (List l in listas)
            {
                Console.WriteLine(String.Format("{0} - {1}", i, l.Name));
                i += 1;
            }
            return listas.ElementAt(ConsoleUtils.ObterInteiro());
        }

        public void SalvarCardsEmTxt(List lista)
        {
            var caminho = ConsoleUtils.ObterCaminho();

            Console.WriteLine(String.Format("\nSalvando cards da lista {0} em txt...", lista.Name));
            IEnumerable<Card> cards = this.trello.Cards.ForList(lista);
            foreach (Card c in cards)
            {
                Console.WriteLine(String.Format("-- {0}", c.Name));
                var arquivo = string.Format("{0}.txt", ArquivoUtils.SubstituirCaracteresInvalidosNomeArquivo(c.Name, ' '));
                TextWriter txt = new StreamWriter(Path.Combine(caminho, arquivo));
                txt.Write(c.Desc);
                txt.Close();
            }

            Console.WriteLine(String.Format("\nCards salvos em txt na pasta {0}.", caminho));
            Console.ReadKey();
        }


    }
}
