using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerHelper.Classes
{
    internal class MatchInfo
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
