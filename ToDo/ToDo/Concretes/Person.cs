using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    internal class BoardManager
    {
        public class Person
        {
            private int id;
            private string name;
            private Team team;

            public Person(int id, string name, Team team)
            {
                this.id = id;
                this.name = name;
                this.team = team;
            }

            public int Id { get { return id; } set { id = value; } }

            public string Name { get { return name; } set { name = value; } }

            public int TeamId { get { return team.Id; } set { team.Id = value; } }
        }

    }
}
