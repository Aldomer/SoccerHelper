using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerHelper.Classes
{
    internal class Team
    {
        internal MatchInfo MatchInfo = new MatchInfo();

        internal Dictionary<int, TeamMember> TeamMembers = new Dictionary<int, TeamMember>();

        internal Team()
        {
            AddTeamMembers();
        }

        internal List<string> CloneTeamMembers
        {
            get
            {
                List<string> clone = new List<string>();

                foreach (int number in TeamMembers.Keys)
                {
                    clone.Add(TeamMembers[number].Name);
                }

                return clone;
            }
        }

        internal void AddTeamMembers()
        {
            TeamMembers.Add(1, new TeamMember("Ezra"));
            TeamMembers.Add(2, new TeamMember("Jared"));
            TeamMembers.Add(3, new TeamMember("Evelyn"));
            TeamMembers.Add(4, new TeamMember("Sawyer"));
            TeamMembers.Add(5, new TeamMember("Cooper"));

            //TeamMembers.Add(1, new TeamMember("Asher"));
            //TeamMembers.Add(2, new TeamMember("Dexter"));
            //TeamMembers.Add(3, new TeamMember("Eli"));
            //TeamMembers.Add(4, new TeamMember("Enoch"));
            //TeamMembers.Add(5, new TeamMember("Hazen"));
            //TeamMembers.Add(6, new TeamMember("Isaac"));
            //TeamMembers.Add(7, new TeamMember("Jackson"));
            //TeamMembers.Add(8, new TeamMember("Jayce"));
        }

        internal int NumberOfPlayers
        {
            get
            {
                return TeamMembers.Count;
            }
        }

        internal TeamMember FindPlayer(string playerName)
        {
            foreach (int playerNumber in TeamMembers.Keys)
            {
                TeamMember teamMember = TeamMembers[playerNumber];

                if (playerName == teamMember.Name)
                    return teamMember;
            }

            return null;
        }
    }
}
