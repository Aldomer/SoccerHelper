using System;
using System.Collections.Generic;

namespace SoccerHelper.Classes
{
    internal class MatchHistory
    {
        private Dictionary<Guid, List<string>> _matchHistory = new Dictionary<Guid, List<string>>();

        internal void AddMatch(Guid guid)
        {
            if (!_matchHistory.ContainsKey(guid))
                _matchHistory.Add(guid, new List<string>());
        }

        internal void AddPosition(Guid guid, string position)
        {
            AddMatch(guid);

            _matchHistory[guid].Add(position);
        }
    }
}
