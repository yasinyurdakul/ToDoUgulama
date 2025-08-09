using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    internal class Program
    {
        public class TeamManager
        {
            private int nextTeamId = 1;
            private List<Team> teams = new List<Team>();

            public TeamManager()
            {
                // Team_1 and team_2 are defined by default.
                CreateNewTeam();
                CreateNewTeam();
            }

            public void CreateNewTeam()
            {
                Team team = new Team(GetNextId);
                teams.Add(team);
            }

            private int GetNextId { get { return nextTeamId++; } }

            public Team GetTeamById(int teamId)
            {
                foreach (Team team in teams)
                {
                    if (team.Id == teamId) return team;
                }
                return null;
            }

        }
    }
}
