using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.ToDoApp.Concretes;

namespace ToDo
{
    public enum CardSizeType
    {
        XS = 1,
        S,
        M,
        L,
        XL
    }

    public class Card
    {
        private int id;
        private string title;
        private string content;
        private Person appointedPerson;
        private CardSizeType size;
        private BoardLineType line;


        public Card(int id, string title, string content, Person appointedPerson, CardSizeType size, BoardLineType line)
        {
            this.id = id;
            this.title = title;
            this.content = content;
            this.appointedPerson = appointedPerson;
            this.size = size;
            this.line = line;
        }

        public int GetId { get { return id; } }
        public string Title { get { return title; } set { title = value; } }
        public string Content { get { return content; } set { content = value; } }
        public int GetAppointedPersonId { get { return appointedPerson.Id; } }
        public void SetAppointedPerson(Person person) { appointedPerson = person; }
        public CardSizeType Size { get { return size; } set { size = value; } }
        public BoardLineType Line { get { return line; } set { line = value; } }
        public string CardContent { get { return content; } set { content = value; } }
        public override string ToString()
        {
            return ""
                + "\n Title   : " + Title
                + "\n Content : " + Content
                + "\n Person  : " + appointedPerson.Name + " (id= " + GetAppointedPersonId + ")"
                + "\n Size    : " + Size
                + "\n Line    : " + Line;
        }
    }
}
