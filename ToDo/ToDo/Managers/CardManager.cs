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
        public class CardManager
        {
            private int nextCardId = 1;
            private List<Card> cards;
            private PersonManager personManager;
            private ProgramManager programManager;


            public CardManager(PersonManager personManager, ProgramManager programManager)
            {
                this.personManager = personManager;
                this.programManager = programManager;
                programManager.AssignCardManager(this);
                cards = new List<Card>();

                CreateANewCard("Title_A", "content A", personManager.GetPersonById(1), CardSizeType.XL, BoardLineType.DONE);
                CreateANewCard("Title_B", "content B", personManager.GetPersonById(2), CardSizeType.L, BoardLineType.DONE);
                CreateANewCard("Title_C", "content C", personManager.GetPersonById(3), CardSizeType.XS, BoardLineType.TODO);
                CreateANewCard("Title_D", "content D", personManager.GetPersonById(4), CardSizeType.S, BoardLineType.TODO);
                CreateANewCard("Title_E", "content E", personManager.GetPersonById(5), CardSizeType.M, BoardLineType.IN_PROGRESS);

            }

            public void CreateANewCard(string title, string content, Person appointedPerson, CardSizeType size, BoardLineType line)
            {
                Card card = new Card(GetNextCardIdAndIncreaseIt, title, content, appointedPerson, size, line);
                cards.Add(card);
            }

            private int GetNextCardIdAndIncreaseIt { get { return nextCardId++; } }

            public int GetLastCardId { get { return nextCardId - 1; } }

            public Card GetCardByCardId(int id)
            {
                foreach (Card card in cards)
                {
                    if (card.GetId == id) return card;
                }
                return null;
            }

            public Card GetCardByCardTitle(string title)
            {
                foreach (Card card in cards)
                {
                    if (card.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase)) return card;
                }
                return null;
            }

            public List<Card> GetAllCardsList() { return cards; }

            public void RemoveCard(int cardId)
            {
                cards.RemoveAt(cards.IndexOf(GetCardByCardId(cardId)));
            }
        }
    }
}

