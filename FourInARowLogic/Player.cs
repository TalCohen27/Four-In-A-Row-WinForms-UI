using System;
using System.Collections.Generic;
using System.Text;

namespace FourInARowLogic
{
    public class Player
    {
        public enum ePlayerType
        {
            Human,
            Computer
        }

        private int m_Score;
        private ePlayerType m_Type;
        private Board.eTileStatus m_Order;
        private string m_Name;

        public int Score
        {
            get { return m_Score; }
            set { m_Score = value; }
        }

        public ePlayerType Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }

        public Board.eTileStatus Order
        {
            get { return m_Order; }
            set { m_Order = value; }
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
    }
}