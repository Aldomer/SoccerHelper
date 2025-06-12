namespace SoccerHelper.Classes
{
    internal class MatchDetails
    {
        internal int TotalMinutes = 40;
        internal int NumberOfPeriods = 2;
        internal int NumberOfSubstitutionsPerPeriod = 4;
        internal bool ShufflePositiosn = true;

        internal int TotalNumberOfSubstitutions
        {
            get
            {
                return NumberOfPeriods * NumberOfSubstitutionsPerPeriod;
            }
        }
    }
}
