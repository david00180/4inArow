using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourInaRow
{
    /*
     * this Struct Rules is responsible to take care about game rule,
     * And decisive using true or false if a point is given to a current player
     */
    public class Rules
    {
        private char[,] m_board;

        public Rules(ref char[,] i_Board)
        {
            m_board = i_Board;
        }

        /*
         * this function return is the board is full or not.
         * true - full
         * false - not full
         */
        public bool isBoardFull(byte i_colLength)
        {
            bool isBoardFull = false;
            for (int i = 0; i < i_colLength; i++)
            {
                if(!(m_board[0, i] == 'x' || m_board[0, i] == 'o'))
                {
                    isBoardFull = true;
                    break;
                }
            }

            return isBoardFull;
        }

        /*
         * this method is return use value only IF his cohice is correct.
         * if the functoin return 10 - it`s mean the board is full and it`s DRAW!
         * if the function return 9 - it`s mean the use \ player choiced to exit the game. 
         */
        public byte checkUserChoiceCol(byte i_colLength, byte i_rowLength)
        {
            bool checkIsNum = false;
            bool correctInputCol = true;
            bool rightValueToContinueProgress = true;
            string userTempInput = null;
            byte freeCells = 0;
            byte o_userChoice = 0;
            byte tempUserChoiceToByte = 0;
            byte tempCheck = 11;

            if (!isBoardFull(i_colLength))
            {
                o_userChoice = 10; // means there is a full board and game is DRAW !
            }
            else
            {
                while (correctInputCol)
                {
                    do
                    {
                        userTempInput = Console.ReadLine();
                        checkIsNum = byte.TryParse(userTempInput, out tempCheck);
                        if (checkIsNum)
                        {
                            if (tempCheck >= 0 && tempCheck < i_colLength)
                            {
                                rightValueToContinueProgress = false;
                            }
                            else
                            {
                                Console.WriteLine("invalid value! input number only in a rage or 'q' to exit game.");
                            }
                        }
                        else
                        {
                            if (userTempInput == "q")
                            {
                                rightValueToContinueProgress = false;
                            }
                            else
                            {
                                Console.WriteLine("invalid value! input number only in a rage or 'q' to exit game.");
                            }
                        }
                    }
                    while(rightValueToContinueProgress);

                    if (userTempInput == "q")
                    {
                        o_userChoice = 9;
                        correctInputCol = false;
                        break;
                    }
                    else
                    {
                        tempUserChoiceToByte = byte.Parse(userTempInput);

                        for (int i = 0; i < i_rowLength; i++)
                        {
                            if (!(m_board[i, tempUserChoiceToByte] == 'x' || m_board[i, tempUserChoiceToByte] == 'o'))
                            {
                                freeCells++;
                            }
                        }

                        if (freeCells != 0)
                        {
                            o_userChoice = tempUserChoiceToByte;
                            correctInputCol = false;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("cannot fill this col! choice another number of col!");
                            freeCells = 0;
                        }
                    }  
                }
            }

            return o_userChoice;
        }

        /*
         * this functoin return the row index.
         * after we know the Col index,
         * and he Has undergone a process of testing
         */
        public byte getPointRowKnownCol(byte i_colValue, byte i_rowLengthBoard)
        {
            int tempRowLength = i_rowLengthBoard - 1;
            byte o_numberRol = 0;
            for (int i = tempRowLength; i >= 0; i--)
            {
                if (m_board[i, i_colValue] != 'x' && m_board[i, i_colValue] != 'o')
                {
                    o_numberRol = (byte)i;
                    break;
                }
            }

            return o_numberRol;
        }

        /*
         * checkRow function check and return ture only if 
         * left signs OR right signs from the user choice move by
         * 2 his Coordinates is filling 4 signs.
         * 
         * otherwise false
         */
        public bool checkRow(byte i_rowIndex, byte i_colIndex, char i_signs)
        {
            bool o_isWin = false;
            byte correctScore = 1;
            byte i_tempColIndexRigth = i_colIndex;
            byte i_tempColIndexLeft = i_colIndex;
            try
            {
                while (correctScore <= 4 && m_board[i_rowIndex, i_tempColIndexRigth + 1] == i_signs)
                {
                    correctScore++;
                    i_tempColIndexRigth++;
                }
            }
            catch(IndexOutOfRangeException)
            {
            }

            if (correctScore == 4)
            {
                o_isWin = true;
            }
            else
            {
                try
                {
                    while (correctScore <= 4 && m_board[i_rowIndex, i_tempColIndexLeft - 1] == i_signs)
                    {
                        correctScore++;
                        i_tempColIndexLeft--;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                }

                if (correctScore == 4)
                {
                    o_isWin = true;
                }
            }

            return o_isWin;
        }

        /*
         * checkCol function check and return ture only if 
         * upper signs OR lower (down by row) signs from the user choice move by
         * 2 his Coordinates is filling 4 signs.
         * 
         * otherwise false
         */
        public bool checkCol(byte i_rowIndex, byte i_colIndex, char i_signs)
        {
            bool o_isWin = false;
            byte correctScore = 1;
            byte i_tempRowIndexUpper = i_rowIndex;
            byte i_tempRowIndexDown = i_rowIndex;
            try
            {
                while (correctScore <= 4 && m_board[i_tempRowIndexUpper + 1, i_colIndex] == i_signs)
                {
                    correctScore++;
                    i_tempRowIndexUpper++;
                }
            }
            catch (IndexOutOfRangeException)
            {
            }

            if (correctScore == 4)
            {
                o_isWin = true;
            }
            else
            {
                try
                {
                    while (correctScore <= 4 && m_board[i_tempRowIndexDown - 1, i_colIndex] == i_signs)
                    {
                        correctScore++;
                        i_tempRowIndexDown--;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                }

                if (correctScore == 4)
                {
                    o_isWin = true;
                }           
            }

            return o_isWin;
        }

        /*
         * checkDiagonalDescends function is check if the upper half part
         * of the Diagonal is seria of 4 signs OR if the second half of the 
         * Diagonal is a seria of 4 signs. also -> return ture the we merger both option 
         * by half part for upper and the second for lower.
         * 
         * otherwise false
         */
        public bool checkDiagonalDescends(byte i_rowIndex, byte i_colIndex, char i_signs)
        {
            bool o_isWin = false;
            byte correctScore = 1;
            byte i_tempDiagonalIndexI = i_rowIndex;
            byte i_tempDiagonalIndexJ = i_colIndex;
            try
            {
                while (correctScore <= 4 && m_board[i_tempDiagonalIndexI - 1, i_tempDiagonalIndexJ - 1] == i_signs)
                {
                    correctScore++;
                    i_tempDiagonalIndexI--;
                    i_tempDiagonalIndexJ--;
                }
            }
            catch (IndexOutOfRangeException)
            {
            }

            if (correctScore == 4)
            {
                o_isWin = true;
            }
            else
            {
                i_tempDiagonalIndexI = i_rowIndex;
                i_tempDiagonalIndexJ = i_colIndex;
                try
                {
                    while (correctScore <= 4 && m_board[i_tempDiagonalIndexI + 1, i_tempDiagonalIndexJ + 1] == i_signs)
                    {
                        correctScore++;
                        i_tempDiagonalIndexI++;
                        i_tempDiagonalIndexJ++;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                }

                if (correctScore == 4)
                {
                    o_isWin = true;
                }
            }

            return o_isWin;
        }

        /*
         * checkDiagonalRising function is check if the upper half part
         * of the Diagonal is seria of 4 signs OR if the second half of the 
         * Diagonal is a seria of 4 signs. also -> return ture the we merger both option 
         * by half part for upper and the second for lower.
         * 
         * otherwise false
         */
        public bool checkDiagonalRising(byte i_rowIndex, byte i_colIndex, char i_signs)
        {
            bool o_isWin = false;
            byte correctScore = 1;
            byte i_tempDiagonalIndexI = i_rowIndex;
            byte i_tempDiagonalIndexJ = i_colIndex;
            try
            {
                while (correctScore <= 4 && m_board[i_tempDiagonalIndexI - 1, i_tempDiagonalIndexJ + 1] == i_signs)
                {
                    correctScore++;
                    i_tempDiagonalIndexI--;
                    i_tempDiagonalIndexJ++;
                }
            }
            catch (IndexOutOfRangeException) 
            { 
            }

            if (correctScore == 4)
            {
                o_isWin = true;
            }
            else
            {
                i_tempDiagonalIndexI = i_rowIndex;
                i_tempDiagonalIndexJ = i_colIndex;

                try
                {
                    while (correctScore <= 4 && m_board[i_tempDiagonalIndexI + 1, i_tempDiagonalIndexJ - 1] == i_signs)
                    {
                        correctScore++;
                        i_tempDiagonalIndexI++;
                        i_tempDiagonalIndexJ--;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                }

                if (correctScore == 4)
                {
                    o_isWin = true;
                }
            }

            return o_isWin;
        }
    }
}
