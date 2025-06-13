using System;
using System.Collections.Generic;

namespace SoccerHelper.Classes
{
    internal class Match
    {
        private MatchDetails _details = new MatchDetails();

        private Dictionary<int, LineUp> _lineUps = new Dictionary<int, LineUp>();

        private Queue<string> _playersOnTheBench = new Queue<string>();
        private Queue<string> _playersOnTheField = new Queue<string>();

        internal void SetPlayersAvailableForMatch(Team team)
        {
            _playersOnTheField.Clear();
            _playersOnTheBench.Clear();

            //Phase 2 In match setup, set who is available, then pass in the list of available players

            //Phase 1 Hardcoded
            //foreach (string playerName in team.GetPlayerNames)
            //{
            //    _playersOnTheBench.Enqueue(playerName);
            //}

            _playersOnTheBench.Enqueue("Ezra");
            _playersOnTheBench.Enqueue("Madelyn");
            _playersOnTheBench.Enqueue("Sawyer");
            _playersOnTheBench.Enqueue("Cooper");

            _playersOnTheBench.Enqueue("Zach");
            _playersOnTheBench.Enqueue("Daisy");
            _playersOnTheBench.Enqueue("Allison");
            _playersOnTheBench.Enqueue("Gage");
        }

        internal void GenerateLineUps(Team team)
        {
            // Need to go through the number of Total Number of substitutions
            for (int substitutionId = 1; substitutionId <= _details.TotalNumberOfSubstitutions; substitutionId++)
            {
                LineUp lineUp = new LineUp();

                lineUp.Generate(team, _playersOnTheField, _playersOnTheBench);

                if ((_playersOnTheBench.Count + _playersOnTheField.Count) != team.NumberOfPlayers)
                    throw new Exception($"Missing a player after completing line up for Substitution {substitutionId}");

                _lineUps.Add(substitutionId, lineUp);
            }
        }

        internal Dictionary<int, LineUp> GetLineUps
        {
            get
            {
                return _lineUps;
            }

        }
    }
}
