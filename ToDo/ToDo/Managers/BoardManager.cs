using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class BoardManager
    {
        private ProgramManager programManager;
        private Board board;
        private CardManager cardManager;

        public BoardManager(CardManager cardManager, ProgramManager programManager)
        {
            this.cardManager = cardManager;
            this.programManager = programManager;
            programManager.AssignBoardManager(this);

            board = new Board(this);

            AddCard(cardManager.GetCardByCardId(1));
            AddCard(cardManager.GetCardByCardId(2));
            AddCard(cardManager.GetCardByCardId(3));
            AddCard(cardManager.GetCardByCardId(4));
            AddCard(cardManager.GetCardByCardId(5));
            this.programManager = programManager;
        }

        public void AddCard(Card card)
        {
            board.AddCard(card);
        }

        public void ListTheBoardItems()
        {
            board.ListAllLines();
        }

        public void MoveCard(Card card)
        {
            board.MoveCardToLine(card);
        }

        public void RemoveCard(int cardId)
        {
            board.DeleteCardOnBoard(cardId);
        }

        public void UpdateCard()
        {
            throw new NotImplementedException();
        }

        public void ReturnToMainMenu()
        {
            programManager.ToDoAppMainMenu();
        }

    }
}
