using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    
    
        public class Team
        {
            private int id;
            private List<Person> members = new List<Person>();

            public Team(int id)
            {
                this.id = id;
            }

            public int Id { get { return id; } set { id = value; } }

            public string Name
            {
                get
                {
                    if (id == 0) return "";
                    else return "Team_" + id.ToString();
                }
            }

            public void AddMemberToTeam(Person member)
            {
                members.Add(member);
            }
        }
    
}
