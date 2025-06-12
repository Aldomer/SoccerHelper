using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerHelper.Classes
{
    internal class LineUp
    {
        private Dictionary<string, string> _lineUp = new Dictionary<string, string>();

        internal void Generate(Team team, Queue<string> playersOnTheField, Queue<string> playersOnTheBench)
        {
            try
            {
                Queue<string> playersToMoveToTheBench = GetPlayersToMoveToTheBench(playersOnTheField);

                foreach (string position in team.GetPositions)
                {
                    //Need to get the right name.
                    //First which players are available?
                    //Then from this list determine who should play per position

                    string playerNameToAddToLineUp = String.Empty;

                    if (playersOnTheBench.Count > 0)
                    {
                        playerNameToAddToLineUp = playersOnTheBench.Dequeue();
                        playersOnTheField.Enqueue(playerNameToAddToLineUp);
                    }
                    else if (playersOnTheField.Count > 0)
                    {
                        //Need to check first if the player is already in the lineup
                        playerNameToAddToLineUp = playersToMoveToTheBench.Dequeue();
                    }

                    _lineUp.Add(position, playerNameToAddToLineUp);
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to generate LineUp", exception);
            }
        }

        private Queue<string> GetPlayersToMoveToTheBench(Queue<string> playersOnTheField)
        {
            int numberOfPlayersOnTheField = playersOnTheField.Count;

            Queue<string> playersToMoveToTheBench = new Queue<string>();

            for (int i = 0; i < numberOfPlayersOnTheField; i++)
            {
                playersToMoveToTheBench.Enqueue(playersOnTheField.Dequeue());
            }

            return playersToMoveToTheBench;
        }
    }
}
