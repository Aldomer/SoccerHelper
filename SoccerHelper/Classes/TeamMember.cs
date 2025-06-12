using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerHelper.Classes
{
    internal class TeamMember
    {
        internal string Name;
        internal Dictionary<int, MatchData> MatchHistory = new Dictionary<int, MatchData>(); // Will need more than match number, matchHistory, need a int season or string season in cases of multiple season in a year

        internal TeamMember(string name)
        {
            Name = name;
        }

        internal class MatchData
        {
            internal int TimePlayed;
            internal Dictionary<int, Positions.PositionOption> PositionsPlayed = new Dictionary<int, Positions.PositionOption>();

            internal MatchData(int substitution, Positions.PositionOption position)
            {
                PositionsPlayed.Add(substitution, position);
            }
        }

        internal void AddPositionsPlayed(int match, int substitution, Positions.PositionOption position)
        {
            if (!MatchHistory.ContainsKey(match))
                MatchHistory.Add(match, new MatchData(substitution, position));
            else
                MatchHistory[match].PositionsPlayed.Add(substitution, position);
        }
    }
}
