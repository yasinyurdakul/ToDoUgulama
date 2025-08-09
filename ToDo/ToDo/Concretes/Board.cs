using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    namespace ToDoApp.Concretes
    {
        public enum BoardLineType
        {
            TODO = 1,
            IN_PROGRESS,
            DONE
        }

        public class Board
        {
            private List<Card> ToDoLine;
            private List<Card> InProgressLine;
            private List<Card> DoneLine;
            private BoardManager boardManager;
            public Board(BoardManager boardManager)
            {
                this.boardManager = boardManager;
                ToDoLine = new List<Card>();
                InProgressLine = new List<Card>();
                DoneLine = new List<Card>();
            }

            public void AddCard(Card card)
            {
                switch (card.Line)
                {
                    case BoardLineType.TODO:
                        ToDoLine.Add(card);
                        break;
                    case BoardLineType.IN_PROGRESS:
                        InProgressLine.Add(card);
                        break;
                    case BoardLineType.DONE:
                        DoneLine.Add(card);
                        break;
                }
            }

            public void ListAllLines()
            {
                Console.WriteLine("\n\nTODO Line\n************************************");

                if (ToDoLine.Count == 0) Console.WriteLine("~ EMPTY ~\n");
                else
                    foreach (Card card in ToDoLine)
                    {
                        Console.WriteLine(card.ToString() + "\n-");
                    }

                Console.WriteLine("\nIN-PROGRESS Line\n************************************");
                if (InProgressLine.Count == 0) Console.WriteLine("~ EMPTY ~\n");
                else
                    foreach (Card card in InProgressLine)
                    {
                        Console.WriteLine(card.ToString() + "\n-");
                    }

                Console.WriteLine("\nDONE Line\n************************************");
                if (DoneLine.Count == 0) Console.WriteLine("~ EMPTY ~\n-");
                else
                    foreach (Card card in DoneLine)
                    {
                        Console.WriteLine(card.ToString() + "\n-");
                    }

                boardManager.ReturnToMainMenu();

            }

            public void DeleteCardOnBoard(int cardId)
            {
                if (GetIndexFromToDoLine(cardId) != -1)
                {
                    ToDoLine.RemoveAt(GetIndexFromToDoLine(cardId));
                }
                else if (GetIndexFromInProgressLine(cardId) != -1)
                {
                    InProgressLine.RemoveAt(GetIndexFromInProgressLine(cardId));
                }
                else if (GetIndexFromDoneLine(cardId) != -1)
                {
                    DoneLine.RemoveAt(GetIndexFromDoneLine(cardId));
                }
            }

            public int GetIndexFromToDoLine(int cardId)
            {
                foreach (Card item in ToDoLine)
                {
                    if (item.GetId == cardId) return ToDoLine.IndexOf(item);
                }
                return -1;
            }

            public int GetIndexFromInProgressLine(int cardId)
            {
                foreach (Card item in InProgressLine)
                {
                    if (item.GetId == cardId) return InProgressLine.IndexOf(item);
                }
                return -1;
            }

            public int GetIndexFromDoneLine(int cardId)
            {
                foreach (Card item in DoneLine)
                {
                    if (item.GetId == cardId) return DoneLine.IndexOf(item);
                }
                return -1;
            }

            public void MoveCardToLine(Card card)
            {
                if (ToDoLine.Contains(card)) ToDoLine.Remove(card);
                if (InProgressLine.Contains(card)) InProgressLine.Remove(card);
                if (DoneLine.Contains(card)) DoneLine.Remove(card);

                switch (card.Line)
                {
                    case BoardLineType.TODO: ToDoLine.Add(card); break;
                    case BoardLineType.IN_PROGRESS: InProgressLine.Add(card); break;
                    case BoardLineType.DONE: DoneLine.Add(card); break;
                }
            }

        }

    }
}
