using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// AI calss. The bot was defined as a winning bot. The bot only looks at the current moment and chooses the best option!
/// The methods of the class calculate the best step by means of the highest numerical "probability" by which it chooses the move.
/// The bot is not able to "block" the user's move by noticing his move, but he aims to win according to a sequence of 4,
/// so there may be a brake for the user to win but not in a calculated way, but in favor of the bot's self-victory
/// </summary>
namespace FourInaRow
{
    public class Aiplayer
    {
        private Rules m_GameRule; // null
        private char[,] m_gameBoard;
        private byte m_points = 0;
        private bool m_isMyTurn = false;
        private byte m_rowLength = 0;
        private byte m_colLength = 0;
        private byte m_setRowOnBoard = 0;
        private byte m_setColOnBoard = 0;

        public Aiplayer(ref char[,] i_gameBoard, ref Rules i_GameRule)
        {
            this.m_gameBoard = i_gameBoard;
            this.m_GameRule = i_GameRule;
        }

        // set the Row length it`s happening on the game progress.
        public void setRowLength(byte i_rowLength)
        {
            m_rowLength = i_rowLength;
        }

        // set the Col length it`s happening on the game progress.
        public void setColLength(byte i_colLength)
        {
            m_colLength = i_colLength;
        }

        // get fill col that using to fill the right COL
        public byte getFillCol()
        {
            return m_setColOnBoard;
        }

        // get fill row that using to fill the right ROW
        public byte getFillRow()
        {
            return m_setRowOnBoard;
        }

        // get && set to print the CPU score.
        public byte Points
        {
            get { return m_points; }
            set { m_points = value; }
        }

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
         * The method is responsible for routing the bot selection process.
         * Between the calculations, we initialize different arrays which give us legitimacy for choosing the best step in a given.
         */
        public void bestMove()
        {
            byte[] ArrayStorageIndex = new byte[m_colLength];
            byte[] ArrayStorageBestMove = new byte[m_colLength];
            bool[] isRowFull = new bool[m_colLength];
            byte tempColLength = m_colLength;
            byte tempRowLength = m_rowLength;
            tempRowLength--;
            tempColLength--;

            byte tempRowIndex = 0;
            byte bestIndexOfRow = 0;
            byte bestIndexOfCol = 0;
            byte searchForCorrectRow = 0;

            char xSign = 'x';
            char oSign = 'o';
            byte indexZero = 0;

            for (int i = 0; i < m_colLength; i++)
            {
               if(!(m_gameBoard[indexZero, i] == xSign || m_gameBoard[indexZero, i] == oSign))
               {
                    isRowFull[i] = true;
               }
            }
  
            while (tempColLength >= 0 && tempColLength < m_colLength)
            {
                tempRowIndex = findEmptyRowIndex(tempColLength, tempRowLength);
                ArrayStorageIndex[tempColLength] = tempRowIndex;

                tempColLength--;
            }

            tempColLength = m_colLength;
            tempColLength--;
            
            while (tempColLength >= 0 && tempColLength < m_colLength)
            {
                ArrayStorageBestMove[tempColLength] = fillProbabilitysOfWinning(ArrayStorageIndex[tempColLength], tempColLength);
                tempColLength--;
            }

            bestIndexOfRow = ArrayStorageBestMove[0];

            for (int i = 0; i < m_colLength; i++)
            {
                if ((ArrayStorageBestMove[i] >= bestIndexOfRow) && isRowFull[i])
                {
                    bestIndexOfRow = ArrayStorageBestMove[i];
                    bestIndexOfCol = (byte)i;
                }
            }

            for (int i = m_rowLength - 1; i >= 0; i--)
            {
                if (!(m_gameBoard[i, bestIndexOfCol] == 'x' || m_gameBoard[i, bestIndexOfCol] == 'o'))
                {
                    searchForCorrectRow = (byte)i;
                    break;
                }
            }

            m_setRowOnBoard = searchForCorrectRow;

            m_setColOnBoard = bestIndexOfCol;
        }

        /*
         *find and return the row of cell in the input col index
         *that is empty.
         *otherwise no option to fill that cell.
         */
        private byte findEmptyRowIndex(byte i_colIndexCurrent, byte i_rowIndex)
        {
            byte o_tempRow = 0;
            for (int i = i_rowIndex; i >= 0; i--)
            {
                if (!(m_gameBoard[i, i_colIndexCurrent] == 'x' || m_gameBoard[i, i_colIndexCurrent] == 'o'))
                {
                    o_tempRow = (byte)i;
                    break;
                }
            }

            return o_tempRow;
        }

        /*
         * find the high number that will lead the AI win. 
         * and return it.
         */
        private byte fillProbabilitysOfWinning(byte i_rowIndex, byte i_colIndex)
        {
            byte rowNum = bestRowProbability(i_rowIndex, i_colIndex, 'o');
            byte colNum = bestColProbability(i_rowIndex, i_colIndex, 'o');
            byte diagonalDescendsNum = bestDiagonalDescendsProbability(i_rowIndex, i_colIndex, 'o');
            byte diagonalRisingNum = bestDiagonalRisingProbability(i_rowIndex, i_colIndex, 'o');
            byte o_tempMax = Math.Max(rowNum, colNum);

            o_tempMax = Math.Max(o_tempMax, diagonalDescendsNum);

            o_tempMax = Math.Max(o_tempMax, diagonalRisingNum);

            return o_tempMax;
        }

        /*
         * this function is return the length of row`s to win. 
         */
        private byte bestRowProbability(byte i_rowIndex, byte i_colIndex, char i_signs)
        {
            byte o_bestNum = 0;
            byte correctScore = 1;
            byte i_tempColIndexRigth = i_colIndex;
            byte i_tempColIndexLeft = i_colIndex;
            try
            {
                while (correctScore <= 4 && m_gameBoard[i_rowIndex, i_tempColIndexRigth + 1] == i_signs)
                {
                    correctScore++;
                    i_tempColIndexRigth++;
                }
            }
            catch (IndexOutOfRangeException)
            {
            }

                try
                {
                    while (correctScore <= 4 && m_gameBoard[i_rowIndex, i_tempColIndexLeft - 1] == i_signs)
                    {
                        correctScore++;
                        i_tempColIndexLeft--;
                    }
                }
                catch(IndexOutOfRangeException) 
                { 
                }

            o_bestNum = correctScore;
            return o_bestNum;
        }

        /*
         * this function is return the length of col`s to win. 
         */
        private byte bestColProbability(byte i_rowIndex, byte i_colIndex, char i_signs)
        {
            byte o_bestNum = 0;
            byte correctScore = 1;
            byte i_tempRowIndexUpper = i_rowIndex;
            byte i_tempRowIndexDown = i_rowIndex;
            try
            {
                while (correctScore <= 4 && m_gameBoard[i_tempRowIndexUpper + 1, i_colIndex] == i_signs)
                {
                    correctScore++;
                    i_tempRowIndexUpper++;
                }
            }
            catch (IndexOutOfRangeException)
            {
            }

                try
                {
                    while (correctScore <= 4 && m_gameBoard[i_tempRowIndexDown - 1, i_colIndex] == i_signs)
                    {
                        correctScore++;
                        i_tempRowIndexDown--;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                }

            o_bestNum = correctScore;

            return o_bestNum;
        }

        /*
         * this function is return the length of Diagonal Descends to win.
         */

        private byte bestDiagonalDescendsProbability(byte i_rowIndex, byte i_colIndex, char i_signs)
        {
            byte o_bestNum = 0;
            byte correctScore = 1;
            byte i_tempDiagonalIndexI = i_rowIndex;
            byte i_tempDiagonalIndexJ = i_colIndex;
            try
            {
                while (correctScore <= 4 && m_gameBoard[i_tempDiagonalIndexI - 1, i_tempDiagonalIndexJ - 1] == i_signs)
                {
                    correctScore++;
                    i_tempDiagonalIndexI--;
                    i_tempDiagonalIndexJ--;
                }
            }
            catch (IndexOutOfRangeException)
            { 
            }

                i_tempDiagonalIndexI = i_rowIndex;
                i_tempDiagonalIndexJ = i_colIndex;
                try
                {
                    while (correctScore <= 4 && m_gameBoard[i_tempDiagonalIndexI + 1, i_tempDiagonalIndexJ + 1] == i_signs)
                    {
                        correctScore++;
                        i_tempDiagonalIndexI++;
                        i_tempDiagonalIndexJ++;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                }

            o_bestNum = correctScore;

            return o_bestNum;
        }

        /*
         * this function is return the length of Diagonal Rising to win. 
         */
        private byte bestDiagonalRisingProbability(byte i_rowIndex, byte i_colIndex, char i_signs)
        {
            byte o_bestNum = 0;
            byte correctScore = 1;
            byte i_tempDiagonalIndexI = i_rowIndex;
            byte i_tempDiagonalIndexJ = i_colIndex;
            try
            {
                while (correctScore <= 4 && m_gameBoard[i_tempDiagonalIndexI - 1, i_tempDiagonalIndexJ + 1] == i_signs)
                {
                    correctScore++;
                    i_tempDiagonalIndexI--;
                    i_tempDiagonalIndexJ++;
                }
            }
            catch (IndexOutOfRangeException)
            {
            }

            i_tempDiagonalIndexI = i_rowIndex;
            i_tempDiagonalIndexJ = i_colIndex;

            try
            {
                while (correctScore <= 4 && m_gameBoard[i_tempDiagonalIndexI + 1, i_tempDiagonalIndexJ - 1] == i_signs)
                {
                    correctScore++;
                    i_tempDiagonalIndexI++;
                    i_tempDiagonalIndexJ--;
                }
            }
            catch (IndexOutOfRangeException)
            {
            }

            o_bestNum = correctScore;
            return o_bestNum;
        }
    }
}