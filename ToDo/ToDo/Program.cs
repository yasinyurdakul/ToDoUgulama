using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    internal class Board
    {
        public class Program
        {
            static void Main(string[] args)
            {
                ProgramManager programManager = new ProgramManager();

                TeamManager teamManager = new TeamManager();

                PersonManager personManager = new PersonManager(teamManager, programManager);

                CardManager cardManager = new CardManager(personManager, programManager);

                BoardManager boardManager = new BoardManager(cardManager, programManager);

                programManager.ToDoAppMainMenu();
            }
        }
    }
}
