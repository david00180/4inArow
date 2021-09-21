using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C21_Ex05_Shai313189490_David_208430165
{
   public class Player
    {
        private byte m_points = 0;
        private bool m_isMyTurn = false;

        /*
         * get that return if is a current player turn 
         * set to change player 
         */
        public bool IsMyTurn
        {
            get { return m_isMyTurn; }
            set { m_isMyTurn = value; }
        }

        /*
         * get && set option for Player score
         */
        public byte points
        {
            get { return m_points; }
            set { m_points = value; }
        }
    }
}