using System;
using System.Collections.Generic;
using System.Text;


namespace FourInARowLogic
{
    public class GameEndedEventArgs : EventArgs
    {
        private readonly FourInARow.eEndOfGameReason? r_EndOfGameReason = null;
        private readonly Player r_Winner;

        public GameEndedEventArgs()
        {
            r_EndOfGameReason = FourInARow.eEndOfGameReason.Tie;
        }

        public GameEndedEventArgs(Player i_Winner)
        {
            r_Winner = i_Winner;
            r_EndOfGameReason = FourInARow.eEndOfGameReason.Win;
        }

        public Player Winner
        {
            get { return r_Winner; }
        }

        public FourInARow.eEndOfGameReason? EndOfGameReason
        {
            get { return r_EndOfGameReason; }
        }
    } 
}
