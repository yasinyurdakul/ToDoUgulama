using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ToDo.BoardManager;

namespace ToDo
{
    internal class Program
    {
        public class PersonManager
        {

            private int nextPersonId = 1;
            private List<Person> people = new List<Person>();
            private TeamManager teamManager;
            private ProgramManager programManager;

            public PersonManager(TeamManager teamManager, ProgramManager programManager)
            {
                this.teamManager = teamManager;
                this.programManager = programManager;
                programManager.AssignPersonManager(this);

                CreateANewPerson(GetNextPersonId, "Raif", teamManager.GetTeamById(1));
                CreateANewPerson(GetNextPersonId, "Elif", teamManager.GetTeamById(1));

                CreateANewPerson(GetNextPersonId, "Sedef", teamManager.GetTeamById(2));
                CreateANewPerson(GetNextPersonId, "Gökhan", teamManager.GetTeamById(2));
                CreateANewPerson(GetNextPersonId, "Duygu", teamManager.GetTeamById(2));

            }

            private void CreateANewPerson(int id, string name, Team team)
            {
                Person person = new Person(id, name, team);
                people.Add(person);
            }

            private int GetNextPersonId { get { return nextPersonId++; } }

            public Person GetPersonById(int id)
            {
                foreach (Person person in people)
                {
                    if (id == person.Id) return person;
                }

                return null;
            }
        }
    }
}
