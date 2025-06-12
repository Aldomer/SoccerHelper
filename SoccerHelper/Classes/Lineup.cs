using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SoccerHelper.Classes
{
    internal class Lineup
    {
        private Dictionary<int, Dictionary<Positions.PositionOption, string>> _lineUpPerSubstitution = new Dictionary<int, Dictionary<Positions.PositionOption, string>>();

        private List<string> _needToPlay = new List<string>();

        private List<string> _playersInLastSubstituion = new List<string>();

        private Dictionary<string, List<Positions.PositionOption>> _whichPositionsHasAPlayerPlayed = new Dictionary<string, List<Positions.PositionOption>>();

        internal void Generate(Team team, int match)
        {
            CleanUp(team);

            PopulateLineUpPerSubsitution(team, match);
            CreateLineUpPerSubstitutionFile(team);
            CreateMatchInformationFile(team, match);
        }

        private void CleanUp(Team team)
        {
            _lineUpPerSubstitution.Clear();
            _playersInLastSubstituion.Clear();
            _whichPositionsHasAPlayerPlayed.Clear();
            _needToPlay = team.CloneTeamMembers;
        }

        private void PopulateLineUpPerSubsitution(Team team, int match)
        {
            Random random = new Random();

            for (int substitution = 1; substitution < team.MatchInfo.TotalNumberOfSubstitutions + 1; substitution++)
            {
                Dictionary<Positions.PositionOption, string> lineUp = new Dictionary<Positions.PositionOption, string>();

                foreach (Positions.PositionOption position in Enum.GetValues(typeof(Positions.PositionOption)))
                {
                    if (_needToPlay.Count == 0)
                        SetPlayersThatNeedToPlay(team);

                    string playerToAssignPosition = GetPlayerToAssignPosition(team, random, position, lineUp);

                    lineUp.Add(position, playerToAssignPosition);

                    if (!_whichPositionsHasAPlayerPlayed.ContainsKey(playerToAssignPosition))
                        _whichPositionsHasAPlayerPlayed.Add(playerToAssignPosition, new List<Positions.PositionOption>());

                    if (!_whichPositionsHasAPlayerPlayed[playerToAssignPosition].Contains(position))
                        _whichPositionsHasAPlayerPlayed[playerToAssignPosition].Add(position);
                }

                SetPayersInLastSubstituion(lineUp);

                _lineUpPerSubstitution.Add(substitution, lineUp);
            }

            UpdatePositionsPlayedByPlayerForMatch(team, match);
        }

        private void SetPlayersThatNeedToPlay(Team team)
        {
            foreach (int playerNumber in team.TeamMembers.Keys)
            {
                string playerName = team.TeamMembers[playerNumber].Name;

                if (!_playersInLastSubstituion.Contains(playerName))
                {
                    _needToPlay.Add(playerName);
                }
            }

            if (_needToPlay.Count == 0)
                _needToPlay = team.CloneTeamMembers;
        }

        private string GetPlayerToAssignPosition(Team team, Random random, Positions.PositionOption position, Dictionary<Positions.PositionOption, string> lineUp)
        {
            List<string> localNeedToPlay = SetLocalNeedToPlay();

            string playerToAssignPosition = String.Empty;

            bool foundPlayer = false;

            do
            {
                int playerToAssignPositionLocation = random.Next(1, localNeedToPlay.Count);

                playerToAssignPosition = localNeedToPlay[playerToAssignPositionLocation - 1];

                if (_whichPositionsHasAPlayerPlayed.ContainsKey(playerToAssignPosition))
                {
                    if (_whichPositionsHasAPlayerPlayed[playerToAssignPosition].Contains(position))
                    {
                        localNeedToPlay.Remove(playerToAssignPosition);

                        if (localNeedToPlay.Count == 0) // See if can fix this by adjusting positions, at the very least don't repeat a position between concurrent substitutions
                        {
                            foreach (Positions.PositionOption lineUpPosition in lineUp.Keys)
                            {
                                string playerName = lineUp[lineUpPosition];

                                if (!_whichPositionsHasAPlayerPlayed[playerName].Contains(position) && !_whichPositionsHasAPlayerPlayed[playerToAssignPosition].Contains(lineUpPosition))
                                {
                                    _whichPositionsHasAPlayerPlayed[playerName].Remove(lineUpPosition);
                                    _whichPositionsHasAPlayerPlayed[playerToAssignPosition].Add(lineUpPosition);

                                    lineUp[lineUpPosition] = playerToAssignPosition;

                                    _needToPlay.Remove(playerToAssignPosition);
                                    _needToPlay.Add(playerName);

                                    playerToAssignPosition = playerName;

                                    break;
                                }
                            }

                            foundPlayer = true;
                        }
                    }
                    else
                        foundPlayer = true;
                }
                else
                    foundPlayer = true;
            }
            while (foundPlayer == false);

            _needToPlay.Remove(playerToAssignPosition);

            return playerToAssignPosition;
        }

        private List<string> SetLocalNeedToPlay()
        {
            List<string> localNeedToPlay = new List<string>();
            foreach (string player in _needToPlay)
            {
                localNeedToPlay.Add(player);
            }

            return localNeedToPlay;
        }

        private void SetPayersInLastSubstituion(Dictionary<Positions.PositionOption, string> lineUp)
        {
            _playersInLastSubstituion.Clear();

            foreach (Positions.PositionOption position in lineUp.Keys)
            {
                string playerName = lineUp[position];

                _playersInLastSubstituion.Add(playerName);
            }
        }

        private void UpdatePositionsPlayedByPlayerForMatch(Team team, int match)
        {
            foreach (int substitution in _lineUpPerSubstitution.Keys)
            {
                Dictionary<Positions.PositionOption, string> lineUp = _lineUpPerSubstitution[substitution];

                foreach (Positions.PositionOption position in lineUp.Keys)
                {
                    string playerName = lineUp[position];

                    TeamMember teamMember = team.FindPlayer(playerName);

                    teamMember.AddPositionsPlayed(match, substitution, position);
                }
            }
        }

        private const string LineUpPerSubstitutionFile = "LineUpPerSubstitution.html";

        private void CreateLineUpPerSubstitutionFile(Team team)
        {
            StringBuilder lineUpPerSubstitutionFile = new StringBuilder();

            lineUpPerSubstitutionFile.AppendLine("<html><table>");

            int period = 1;
            bool periodChanged = true;

            foreach (int substitution in _lineUpPerSubstitution.Keys)
            {
                if (periodChanged)
                {
                    lineUpPerSubstitutionFile.AppendLine($"<tr><td>Period</td><td>{period}</td></tr>");
                    periodChanged = false;
                }

                lineUpPerSubstitutionFile.AppendLine($"<tr><td>Substitution</td><td>{substitution}</td></tr>");

                foreach (Positions.PositionOption position in _lineUpPerSubstitution[substitution].Keys)
                {
                    string player = _lineUpPerSubstitution[substitution][position];

                    lineUpPerSubstitutionFile.AppendLine($"<tr><td>{position}</td><td>{player}</td></tr>");
                }

                lineUpPerSubstitutionFile.Append("<tr><td>&nbsp;</td></tr>");

                if (substitution % team.MatchInfo.NumberOfSubstitutionsPerPeriod == 0)
                {
                    period++;
                    periodChanged = true;
                }
            }

            lineUpPerSubstitutionFile.AppendLine("</table></html>");

            System.IO.File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + LineUpPerSubstitutionFile, lineUpPerSubstitutionFile.ToString());
        }

        private const string MatchInformationFile = "MatchInformationFile.html";

        private void CreateMatchInformationFile(Team team, int match)
        {
            StringBuilder matchInformationFile = new StringBuilder();

            matchInformationFile.AppendLine("<html><table>");

            matchInformationFile.AppendLine($"<tr><td>Match</td><td>{match}</td></tr>");

            foreach (int playerNumber in team.TeamMembers.Keys)
            {
                TeamMember player = team.TeamMembers[playerNumber];

                if (player.MatchHistory.ContainsKey(match))
                {
                    TeamMember.MatchData matchData = player.MatchHistory[match];

                    matchInformationFile.AppendLine($"<tr><td>{player.Name}</td></tr>");

                    foreach (int substitution in matchData.PositionsPlayed.Keys)
                    {
                        Positions.PositionOption position = matchData.PositionsPlayed[substitution];

                        matchInformationFile.AppendLine($"<tr><td>Substitution</td><td>{substitution}</td></tr>");
                        matchInformationFile.AppendLine($"<tr><td>Position</td><td>{position}</td></tr>");
                    }

                    matchInformationFile.Append("<tr><td>&nbsp;</td></tr>");
                }
            }

            matchInformationFile.AppendLine("</table></html>");

            System.IO.File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + MatchInformationFile, matchInformationFile.ToString());
        }
    }
}
