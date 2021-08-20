using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex02.ConsoleUtils;

namespace FourInaRow
{
   public class GameUI
    {
        private char[,] m_gameBoad = null;
        private byte m_rowSize = 0;
        private byte m_colSize = 0;

        public GameUI(ref char[,] i_gameBoad, byte i_rowSize, byte i_colSize)
        {
            m_gameBoad = i_gameBoad;
            m_rowSize = i_rowSize;
            m_colSize = i_colSize;
        }

        /*
         * this method is reset the board if there is 
         * draw, exit or game finished option.
         */
        public void resetBoard()
        {
            for (int i = 0; i < m_rowSize; i++)
            {
                for (int j = 0; j < m_colSize; j++)
                {
                    m_gameBoad[i, j] = ' ';
                }
            }
        }
  
        /*
         * this method is print the Game Board.
         */
        public void printBoard()
        {
            for (int i = 0; i < m_colSize; i++)
            {
                Console.Write("  " + i + "   ");
            }

            Console.WriteLine();

            for (int i = 0; i < m_rowSize; i++)
            {
                for (int j = 0; j < m_colSize; j++)
                {
                    Console.Write("| " + m_gameBoad[i, j] + " | ");
                }

                Console.WriteLine();

                for (int k = 0; k < m_colSize; k++)
                {
                    Console.Write("======");
                }

                Console.WriteLine();
            }
        }

        /*
         * this method is reset / clean from the consol the board 
         */
        public void resetOutPutPrint()
        {
            Ex02.ConsoleUtils.Screen.Clear();
        }
    }
}
