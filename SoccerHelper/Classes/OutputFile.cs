using System.Text;

namespace SoccerHelper.Classes
{
    internal static class OutputFile
    {
        private const string MatchInformationFile = "MatchInformationFile.html";

        internal static void CreateMatchInformationFile(Match match)
        {
            StringBuilder matchInformationFile = new StringBuilder();

            matchInformationFile.AppendLine("<html><table>");

            matchInformationFile.AppendLine($"<tr><td>Match</td><td>{match}</td></tr>");

            foreach (int subsitutionId in match.GetLineUps.Keys)
            {
                matchInformationFile.AppendLine($"<tr><td>Substitution {subsitutionId}</td></tr>");

                LineUp lineUp = match.GetLineUps[subsitutionId];

                foreach (string position in lineUp.GetLineUp.Keys)
                {
                    matchInformationFile.AppendLine($"<tr><td>Position: {position} Player: {lineUp.GetLineUp[position]}</td></tr>");
                }
            }

            matchInformationFile.AppendLine("</table></html>");

            //foreach (int playerNumber in team.TeamMembers.Keys)
            //{
            //    TeamMember player = team.TeamMembers[playerNumber];

            //    if (player.MatchHistory.ContainsKey(match))
            //    {
            //        TeamMember.MatchData matchData = player.MatchHistory[match];

            //        matchInformationFile.AppendLine($"<tr><td>{player.Name}</td></tr>");

            //        foreach (int substitution in matchData.PositionsPlayed.Keys)
            //        {
            //            Positions.PositionOption position = matchData.PositionsPlayed[substitution];

            //            matchInformationFile.AppendLine($"<tr><td>Substitution</td><td>{substitution}</td></tr>");
            //            matchInformationFile.AppendLine($"<tr><td>Position</td><td>{position}</td></tr>");
            //        }

            //        matchInformationFile.Append("<tr><td>&nbsp;</td></tr>");
            //    }
            //}

            matchInformationFile.AppendLine("</table></html>");

            System.IO.File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + MatchInformationFile, matchInformationFile.ToString());
        }
    }
}
