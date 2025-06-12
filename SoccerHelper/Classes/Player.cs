namespace SoccerHelper.Classes
{
    internal class Player
    {
        private string _name;
        private MatchHistory _matchHistory = new MatchHistory();

        internal Player(string name)
        {
            _name = name;
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }
    }
}
