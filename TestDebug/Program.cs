using System;
using Codenation.Challenge;

namespace TestDebug
{
    class Program
    {
        static void Main(string[] args)
        {
            SoccerTeamsManager TeamManager = new SoccerTeamsManager();
            TeamManager.AddTeam(1, "Flamengo", DateTime.Now, "Vermelho", "Preto");
            TeamManager.AddTeam(2, "Inter", DateTime.Now, "Vermelho", "Branco");
            TeamManager.AddTeam(3, "Vasco", DateTime.Now, "Branco", "Preto");
            //TeamManager.AddTeam(4, "Gremio", DateTime.Now, "Azul", "Branco");
            //TeamManager.AddTeam(5, "Palmeiras", DateTime.Now, "Verde", "Branco");
            //TeamManager.AddTeam(6, "Atletico Parananense", DateTime.Now, "Preto", "Vermelho");


            TeamManager.AddPlayer(7, 1, "Edmundo", DateTime.Now, 19, 1000);
            TeamManager.AddPlayer(10, 1, "Romario", DateTime.Now, 9, 1000);

            TeamManager.AddPlayer(3, 2, "Dida", DateTime.Now, 11, 10000);
            TeamManager.AddPlayer(4, 2, "Dunga", DateTime.Now, 3, 8000);

            TeamManager.AddPlayer(5, 3, "Tafarel", DateTime.Now, 19, 20000);
            TeamManager.AddPlayer(6, 3, "Felipao", DateTime.Now, 4, 100000);

            TeamManager.SetCaptain(3);
            TeamManager.SetCaptain(2);
          







        }
    }
}
