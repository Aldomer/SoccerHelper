using System;
using System.Collections.Generic;

namespace SoccerHelper.Classes
{
    internal class Team
    {
        private Dictionary<string, Player> _players = new Dictionary<string, Player>();
        private List<string> _positions = new List<string>();

        internal Team()
        { 
        
        }

        internal void AddPlayer(string playerName)
        {
            if (!_players.ContainsKey(playerName))
                _players.Add(playerName, new Player(playerName));
        }

        internal void AddPosition(string position)
        {
            if (!_positions.Contains(position))
                _positions.Add(position);
        }

        internal List<string> GetPlayerNames
        {
            get
            {
                List<string> playerNames = new List<string>();

                foreach (Player player in _players.Values)
                {
                    playerNames.Add(player.Name);
                }

                return playerNames;
            }
        }

        internal List<string> GetPositions
        {
            get
            {
                return _positions;
            }
        }

        internal bool LoadPlayerList()
        {
            //Phase 2 this will go to where the team data is stored and load it in

            //Phase 1 Hardcoded team
            AddPlayer("Ezra");
            AddPlayer("Madelyn");
            AddPlayer("Sawyer");
            AddPlayer("Cooper");
            AddPlayer("Zach");
            AddPlayer("Daisy");
            AddPlayer("Allison");
            AddPlayer("Gage");

            return true;
        }

        internal bool LoadPositions()
        {
            //Phase 2 this will go to where the team data is stored and load it in

            //Phase 1 Hardcoded positions
            AddPosition("Striker");
            AddPosition("MidfielderRight");
            AddPosition("MidfielderLeft");
            AddPosition("Defender");

            return true;
        }

        internal int NumberOfPlayers
        {
            get
            {
                return _players.Count;
            }
        }

        internal Player GetPlayer(string searchForThisPlayerName)
        {
            if (_players.ContainsKey(searchForThisPlayerName))
                return _players[searchForThisPlayerName];
            else
                throw new Exception($"{searchForThisPlayerName} is not part of this team");
        }
    }
}
