using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ToDo.BoardManager;
using ToDo.ToDoApp.Concretes;

namespace ToDo
{
    internal class Program
    {
        public class ProgramManager
        {
            private TeamManager teamManager;
            private PersonManager personManager;
            private CardManager cardManager;
            private BoardManager boardManager;

            public ProgramManager()
            {
                Console.WriteLine("The program is started.\n");
            }

            public ProgramManager(TeamManager teamManager, PersonManager personManager, CardManager cardManager, BoardManager boardManager)
            {
                this.teamManager = teamManager;
                this.personManager = personManager;
                this.cardManager = cardManager;
                this.boardManager = boardManager;
            }

            public void AssignTeamManager(TeamManager manager)
            {
                this.teamManager = manager;
            }

            public void AssignPersonManager(PersonManager manager)
            {
                this.personManager = manager;
            }

            public void AssignCardManager(CardManager manager)
            {
                this.cardManager = manager;
            }

            public void AssignBoardManager(BoardManager manager)
            {
                this.boardManager = manager;
            }

            public void ToDoAppMainMenu()
            {
                Console.WriteLine("\n****************** TODO APP MAIN MENU ******************");
                Console.WriteLine("Please write the number of the transaction you want to do.");
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine(""
                    + "(1) LIST The Board,\n"
                    + "(2) ADD Card to Board,\n"
                    + "(3) DELETE Card from Board,\n"
                    + "(4) MOVE The Card,\n"
                    + "(5) UPDATE The Card,\n"
                    + "(0) EXIT The Todo App,\n"
                    );

                string input = Console.ReadLine().Trim();

                switch (input)
                {
                    case "1":
                        ListTheBoardItems();
                        break;

                    case "2":
                        AddCardToBoardMenu();
                        break;

                    case "3":
                        DeleteCardMenu();
                        break;

                    case "4":
                        MoveCardMenu();
                        break;

                    case "5":
                        UpdateCardMenu();
                        break;
                    case "0":
                        ExitToApp();
                        break;

                    default:
                        Console.WriteLine("Invalid entry!\n");
                        ToDoAppMainMenu();
                        break;
                }

            }

            private void ListTheBoardItems()
            {
                boardManager.ListTheBoardItems();
            }

            private void AddCardToBoardMenu()
            {
                Console.WriteLine("\n******** ADD CART MENU *********");
                Console.WriteLine("Please enter the requested data.");

                string title = ""; string content = ""; int personId = -1; int size = -1; int line = -1;

            TryAgain:
                if (string.IsNullOrEmpty(title))
                {
                    Console.Write("Please enter the card title: ");
                    title = Console.ReadLine().Trim();

                    if (string.IsNullOrEmpty(title))
                    {
                        Console.WriteLine("Title cannot be empty!");
                        goto TryAgain;
                    }
                }
                else if (string.IsNullOrEmpty(content))
                {
                    Console.Write("Please enter the card content: ");
                    content = Console.ReadLine().Trim();

                    if (string.IsNullOrEmpty(content))
                    {
                        Console.WriteLine("Content cannot be empty!");
                        goto TryAgain;
                    }
                }
                else if (size == -1)
                {
                    Console.Write("Please enter the card size number. -> XS(1),S(2),M(3),L(4),XL(5) : ");

                    if (int.TryParse(Console.ReadLine().Trim(), out size))
                    {
                        if (size < 1 || size > 5)
                        {
                            Console.WriteLine("You entered an invalid number. A digit from 1 to 5 should be entered.");
                            size = -1;
                            goto TryAgain;
                        }
                    }
                    else
                    {
                        Console.WriteLine("You entered an invalid number. A digit from 1 to 5 should be entered.");
                        size = -1;
                        goto TryAgain;
                    }
                }
                else if (personId == -1)
                {
                    Console.Write("Please enter the person id number: ");

                    if (int.TryParse(Console.ReadLine().Trim(), out personId))
                    {
                        Person person = personManager.GetPersonById(personId);
                        if (person == null)
                        {
                            Console.WriteLine("The person was not found!");
                            personId = -1;
                            goto TryAgain;
                        }
                    }
                    else
                    {
                        Console.WriteLine("You entered an invalid number.");
                        personId = -1;
                        goto TryAgain;
                    }
                }
                else if (line == -1)
                {
                    Console.Write("Please enter the card line. -> TODO(1), IN-PROGRESS(2), DONE(3) : ");

                    if (int.TryParse(Console.ReadLine().Trim(), out line))
                    {
                        if (line < 1 || line > 3)
                        {
                            Console.WriteLine("You entered an invalid number. A digit from 1 to 3 should be entered.");
                            line = -1;
                            goto TryAgain;
                        }
                    }
                    else
                    {
                        Console.WriteLine("You entered an invalid number. A digit from 1 to 3 should be entered.");
                        line = -1;
                        goto TryAgain;
                    }
                }

                Console.WriteLine();
                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content) || size == -1 || personId == -1 || line == -1)
                    goto TryAgain;

                cardManager.CreateANewCard(title, content, personManager.GetPersonById(personId), (CardSizeType)size, (BoardLineType)line);

                Card card = cardManager.GetCardByCardId(cardManager.GetLastCardId);

                if (card == null)
                {
                    Console.WriteLine("The operation failed. Please try again.");
                    AddCardToBoardMenu();
                }
                else
                {
                    boardManager.AddCard(card);
                    Console.WriteLine("Operation successful. A new card has been added to the board.");

                    ToDoAppMainMenu();
                }
            }

            private void DeleteCardMenu()
            {
                Console.WriteLine("\n******* DELETE CART MENU *******");
                Console.WriteLine("Please enter the title of the card you want to delete.");

                string title = Console.ReadLine().Trim();

                if (cardManager.GetCardByCardTitle(title) == null)
                {
                    Console.WriteLine("No cards found for your search criteria."
                        + "\n* Quit Deleting -> (1) :"
                        + "\n* Try Again -> (2) :"
                        );

                    string input = Console.ReadLine().Trim();
                    if (input.Equals("1"))
                    {
                        Console.WriteLine("The deletion has been cancelled.\n");
                        ToDoAppMainMenu();
                    }
                    else if (input.Equals("2"))
                    {
                        DeleteCardMenu();
                    }
                    else
                    {
                        Console.WriteLine("You entered an invalid number.");
                        DeleteCardMenu();
                    }
                }
                else
                {
                    List<int> theyWillDeletedIds = new List<int>();

                    foreach (Card card in cardManager.GetAllCardsList())
                    {
                        if (card.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase))
                        {
                            theyWillDeletedIds.Add(card.GetId);
                        }
                    }

                    foreach (int id in theyWillDeletedIds)
                    {
                        boardManager.RemoveCard(id);
                        cardManager.RemoveCard(id);
                    }

                    Console.WriteLine("The deletion was successful.");

                    ToDoAppMainMenu();
                }
            }

            private void MoveCardMenu()
            {
                Console.WriteLine("\n******* MOVE CART MENU *******");
                Console.WriteLine("Please enter the title of the card you want to move.");

                string title = Console.ReadLine().Trim();

                if (cardManager.GetCardByCardTitle(title) == null)
                {
                    Console.WriteLine("No cards found for your search criteria."
                        + "\n* Quit Moving -> (1) :"
                        + "\n* Try Again -> (2) :"
                        );

                    string input = Console.ReadLine().Trim();
                    if (input.Equals("1"))
                    {
                        Console.WriteLine("The moving has been cancelled.\n");
                        ToDoAppMainMenu();
                    }
                    else if (input.Equals("2"))
                    {
                        MoveCardMenu();
                    }
                    else
                    {
                        Console.WriteLine("You entered an invalid number.");
                        MoveCardMenu();
                    }
                }
                else
                {
                    List<int> theyWillMovedIds = new List<int>();

                    foreach (Card card in cardManager.GetAllCardsList())
                    {
                        if (card.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase))
                        {
                            theyWillMovedIds.Add(card.GetId);
                        }
                    }

                    if (theyWillMovedIds.Count > 1)
                    {
                    TryAgainForMoving:
                        Console.WriteLine("Cards with the title you are looking for.");
                        Console.WriteLine("--------------------------");

                        foreach (int id in theyWillMovedIds)
                        {
                            Console.WriteLine("Card ID: " + id);
                            Console.WriteLine(cardManager.GetCardByCardId(id).ToString());
                            Console.WriteLine("--------------------------");
                        }

                        Console.Write("Enter the ID number of the card you want to move: ");

                        string input = Console.ReadLine().Trim();

                        if (int.TryParse(input, out int choosenId))
                        {
                            if (theyWillMovedIds.Contains(choosenId))
                            {
                                MoveChoosenCard(choosenId);
                            }
                            else
                            {
                                Console.WriteLine("You entered an invalid number.");
                                goto TryAgainForMoving;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please enter valid ID number as integer.");
                            goto TryAgainForMoving;
                        }

                    }
                    else if (theyWillMovedIds.Count == 1)
                    {
                        MoveChoosenCard(theyWillMovedIds[0]);
                    }
                }
            }

            private void MoveChoosenCard(int id)
            {
                Console.WriteLine("\nDetails of the card found.\n********************************");
                Console.WriteLine(cardManager.GetCardByCardId(id).ToString());

            TryAgainForLine:
                Console.WriteLine("\nPlease enter the line you want to move the card to."
                + "\n(1) TODO"
                + "\n(2) IN-PROGRESS"
                + "\n(3) DONE"
                );

                string input = Console.ReadLine().Trim();
                if (int.TryParse(input, out int choosenLine))
                {
                    if (choosenLine >= 1 && choosenLine <= 3)
                    {
                        cardManager.GetCardByCardId(id).Line = (BoardLineType)choosenLine;
                        boardManager.MoveCard(cardManager.GetCardByCardId(id));
                        Console.WriteLine("The card has been moved.");
                    }
                    else
                    {
                        Console.WriteLine("You entered an invalid line number.");
                        goto TryAgainForLine;
                    }
                }
                else
                {
                    Console.WriteLine("Please enter valid Line number as integer. (1, 2, or 3)");
                    goto TryAgainForLine;
                }

                ToDoAppMainMenu();
            }

            private void UpdateCardMenu()
            {
                Console.WriteLine("\n******* UPDATE CART MENU *******");
                Console.WriteLine("Please enter the title of the card you want to update.");

                string title = Console.ReadLine().Trim();

                if (cardManager.GetCardByCardTitle(title) == null)
                {
                    Console.WriteLine("No cards found for your search criteria."
                        + "\n* Quit Updating -> (1) :"
                        + "\n* Try Again -> (2) :"
                        );

                    string input = Console.ReadLine().Trim();
                    if (input.Equals("1"))
                    {
                        Console.WriteLine("The updating has been cancelled.\n");
                        ToDoAppMainMenu();
                    }
                    else if (input.Equals("2"))
                    {
                        UpdateCardMenu();
                    }
                    else
                    {
                        Console.WriteLine("You entered an invalid number.");
                        UpdateCardMenu();
                    }
                }
                else
                {
                    List<int> idsToUpdate = new List<int>();

                    foreach (Card card in cardManager.GetAllCardsList())
                    {
                        if (card.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase))
                        {
                            idsToUpdate.Add(card.GetId);
                        }
                    }

                    if (idsToUpdate.Count > 1)
                    {
                    TryAgainForUpdating:
                        Console.WriteLine("Cards with the title you are looking for.");
                        Console.WriteLine("--------------------------");

                        foreach (int id in idsToUpdate)
                        {
                            Console.WriteLine("Card ID: " + id);
                            Console.WriteLine(cardManager.GetCardByCardId(id).ToString());
                            Console.WriteLine("--------------------------");
                        }

                        Console.Write("Enter the ID number of the card you want to update: ");

                        string input = Console.ReadLine().Trim();

                        if (int.TryParse(input, out int choosenId))
                        {
                            if (idsToUpdate.Contains(choosenId))
                            {
                                UpdateChoosenCard(choosenId);
                            }
                            else
                            {
                                Console.WriteLine("You entered an invalid number.");
                                goto TryAgainForUpdating;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please enter valid ID number as integer.");
                            goto TryAgainForUpdating;
                        }
                    }
                    else if (idsToUpdate.Count == 1)
                    {
                        UpdateChoosenCard(idsToUpdate[0]);
                    }
                }

            }

            private void UpdateChoosenCard(int id)
            {
                Console.WriteLine("\nDetails of the card found.\n********************************");
                Console.WriteLine(cardManager.GetCardByCardId(id).ToString());
                Console.WriteLine("-----------------------------");

                Console.WriteLine("\nPlease enter the requested data.");

                string title = ""; string content = ""; int personId = -1; int size = -1; int line = -1;

            TryAgainForUpdate:
                if (string.IsNullOrEmpty(title))
                {
                    Console.Write("Please enter the new title: ");
                    title = Console.ReadLine().Trim();

                    if (string.IsNullOrEmpty(title))
                    {
                        Console.WriteLine("Title cannot be empty!");
                        goto TryAgainForUpdate;
                    }
                }
                else if (string.IsNullOrEmpty(content))
                {
                    Console.Write("Please enter the new content: ");
                    content = Console.ReadLine().Trim();

                    if (string.IsNullOrEmpty(content))
                    {
                        Console.WriteLine("Content cannot be empty!");
                        goto TryAgainForUpdate;
                    }
                }
                else if (size == -1)
                {
                    Console.Write("Please enter the new size number. -> XS(1),S(2),M(3),L(4),XL(5) : ");

                    if (int.TryParse(Console.ReadLine().Trim(), out size))
                    {
                        if (size < 1 || size > 5)
                        {
                            Console.WriteLine("You entered an invalid number. A digit from 1 to 5 should be entered.");
                            size = -1;
                            goto TryAgainForUpdate;
                        }
                    }
                    else
                    {
                        Console.WriteLine("You entered an invalid number. A digit from 1 to 5 should be entered.");
                        size = -1;
                        goto TryAgainForUpdate;
                    }
                }
                else if (personId == -1)
                {
                    Console.Write("Please enter the new person id: ");

                    if (int.TryParse(Console.ReadLine().Trim(), out personId))
                    {
                        Person person = personManager.GetPersonById(personId);
                        if (person == null)
                        {
                            Console.WriteLine("The person was not found!");
                            personId = -1;
                            goto TryAgainForUpdate;
                        }
                    }
                    else
                    {
                        Console.WriteLine("You entered an invalid number.");
                        personId = -1;
                        goto TryAgainForUpdate;
                    }
                }
                else if (line == -1)
                {
                    Console.Write("Please enter the new line. -> TODO(1), IN-PROGRESS(2), DONE(3) : ");

                    if (int.TryParse(Console.ReadLine().Trim(), out line))
                    {
                        if (line < 1 || line > 3)
                        {
                            Console.WriteLine("You entered an invalid number. A digit from 1 to 3 should be entered.");
                            line = -1;
                            goto TryAgainForUpdate;
                        }
                    }
                    else
                    {
                        Console.WriteLine("You entered an invalid number. A digit from 1 to 3 should be entered.");
                        line = -1;
                        goto TryAgainForUpdate;
                    }
                }

                Console.WriteLine();
                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content) || size == -1 || personId == -1 || line == -1)
                    goto TryAgainForUpdate;

                Card card = cardManager.GetCardByCardId(id);
                card.Title = title;
                card.Content = content;
                card.SetAppointedPerson(personManager.GetPersonById(personId));
                card.Size = (CardSizeType)size;
                card.Line = (BoardLineType)line;

                boardManager.MoveCard(card);

                Console.WriteLine("\nThe update is complete.\n");
                ToDoAppMainMenu();

            }

            private void ExitToApp()
            {
                Console.WriteLine("Program terminated.");
            }
        }
    }
}
