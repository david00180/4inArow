using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourInaRow
{
    public class FourinARow
    {
        private Rules m_GameRule; // null 
        private GameUI m_userInterface = null;
        private Player m_playerOne = null;
        private Player m_playerTow = null;
        private Aiplayer m_playerAi = null;
        private char[,] m_gameBoard = null;
        private byte m_numOfRow = 0;
        private byte m_numOfCol = 0;
        private byte m_userChoice = 0;
        
        public FourinARow()
        {
            Console.WriteLine("Hello and Wellcome to 4 in a row GAME !");
            defineUserChoicePlayStyle();
            userDefineBoardSize();
            defineDirectionOfGameStyle();
        }

        /*
         * Routes the game process
         * against Player or AI
         */
        public void startPlay()
        {
            m_userInterface = new GameUI(ref m_gameBoard, m_numOfRow, m_numOfCol);
            m_userInterface.resetOutPutPrint();
            m_userInterface.printBoard();

            if (m_userChoice == 1)
            {
                pvpGame();
            }
            else
            {
                aiVsPlayer();
            }
        }

        /*
         * this method is operate the game of Player Vs AI
         */
        private void aiVsPlayer()
        {
            bool isGameContinue = true;
            bool isAiPlay = true;
            m_playerAi.setRowLength(m_numOfRow);
            m_playerAi.setColLength(m_numOfCol);

            while (isGameContinue)
            {
                isGameContinue = progressOfUserInputGame(1, 'x', isAiPlay);

                if (isGameContinue)
               {
                    m_playerAi.bestMove();

                    m_gameBoard[m_playerAi.getFillRow(), m_playerAi.getFillCol()] = 'o';
                    m_userInterface.resetOutPutPrint();
                    m_userInterface.printBoard();

                    if (isWin(m_playerAi.getFillRow(), m_playerAi.getFillCol(), 'o'))
                    {
                        m_playerAi.Points++;
                        Console.WriteLine("Congratulations! CPU win.\ncpu score: " + m_playerAi.Points + "\nPlayer 1 score: " + m_playerOne.points);
                        isGameContinue = userChoiceToContinueGame(0);
                        m_userInterface.resetBoard();
                    }

                    if (!m_GameRule.isBoardFull(m_numOfCol))
                    {
                        Console.WriteLine("draw!");
                        isGameContinue = userChoiceToContinueGame(10);
                    }
                }
            }
        }

        /*
         * this method is the Engine of the PVP game (player Vs player)
         */
        private void pvpGame()
        {
            bool whichPlayerStart = true;
            bool isGameContinue = true;
            bool isAiPlay = false;

            /*
             * if returned value is TRUE then player one begin.
             * if returned value is FALSE then player two begin.
             */
        whichPlayerStart = choiceRandomWhichPlayerBegin();

            while (isGameContinue)
            {    
                ///player 1 - > ' x '
                if (whichPlayerStart)
                {
                    isGameContinue = progressOfUserInputGame(1, 'x', isAiPlay);
                    whichPlayerStart = false;
                }
                else
                {
                    isGameContinue = progressOfUserInputGame(2, 'o', isAiPlay);
                    whichPlayerStart = true;
                }
            }
        }

        /*
         * this function is return true only if the player is win 
         * 
         */
        private bool isWin(byte i_rowPosition, byte i_userColChoiceCol, char i_sign)
        {
            bool o_rowCheckWin = false;

            bool o_colCheckWin = false;

            bool o_diagonalDescendsCheckWin = false;
            bool o_diagonalRisingCheckWin = false;
            o_rowCheckWin = m_GameRule.checkRow(i_rowPosition, i_userColChoiceCol, i_sign);
            o_colCheckWin = m_GameRule.checkCol(i_rowPosition, i_userColChoiceCol, i_sign);
            o_diagonalDescendsCheckWin = m_GameRule.checkDiagonalDescends(i_rowPosition, i_userColChoiceCol, i_sign);
            o_diagonalRisingCheckWin = m_GameRule.checkDiagonalRising(i_rowPosition, i_userColChoiceCol, i_sign);

            return o_rowCheckWin || o_colCheckWin || o_diagonalDescendsCheckWin || o_diagonalRisingCheckWin;
        }

        /*
         * this function return true to continue the game.
         * how?
         * 1. win
         * 2. draw 
         * 3. exit
         * 
         * otherwise, false and the game is stop.
         */
        private bool progressOfUserInputGame(byte i_indexPlayer, char i_sign, bool i_isAI)
        {
            bool isGameContinue = true;
            byte userColChoiceCol = 0, rowPosition = 0;

            Console.WriteLine("player " + i_indexPlayer + "  turn - you play with - '" + i_sign + "' - choice col:\npress 'q' to exit game ");
            userColChoiceCol = m_GameRule.checkUserChoiceCol(m_numOfCol, m_numOfRow);
            /// 10 = draw, 9 == exit 
            if (userColChoiceCol == 10 || userColChoiceCol == 9)
            {
                isGameContinue = userChoiceToContinueGame(userColChoiceCol);
            }
            else
            {
                rowPosition = m_GameRule.getPointRowKnownCol(userColChoiceCol, m_numOfRow);
                m_gameBoard[rowPosition, userColChoiceCol] = i_sign; // set here the sign in the cell of the board.

                if (isWin(rowPosition, userColChoiceCol, i_sign))
                {
                    m_userInterface.resetOutPutPrint();
                    m_userInterface.printBoard();
                    if(i_indexPlayer == 1)
                    {
                        m_playerOne.points++;
                    }
                    else
                    {
                        m_playerTow.points++;
                    }

                    if (i_isAI)
                    {
                        Console.WriteLine("Congratulations! player 1 win!\nplayer 1 score: " + m_playerOne.points + "\nCPU score: " + m_playerAi.Points);
                    }
                    else
                    {
                        Console.WriteLine("Congratulations! player " + i_indexPlayer + " win!\nplayer 1 score: " + m_playerOne.points + "\nplayer 2 score: " + m_playerTow.points);
                    }

                    isGameContinue = userChoiceToContinueGame(0);
                }
                else
                {
                    m_userInterface.resetOutPutPrint();
                    m_userInterface.printBoard();
                }
            }

            return isGameContinue;
        }

        /*
         * this function return true if the use want to continue the game 
         * or not for any reason - win, exit, draw
         */
        private bool userChoiceToContinueGame(byte i_gameState)
        {
            bool o_isGameContinue = false;
            string tempUserChoiceCheck = null;
            while (!(tempUserChoiceCheck == "y" || tempUserChoiceCheck == "n"))
            {
                if (i_gameState == 10)
                {
                   if(m_playerTow != null)
                    {
                        Console.WriteLine("draw!\nplayer 1 score: " + m_playerOne.points + "\nplayer 2 score: " + m_playerTow.points);
                    }
                    else
                    {
                        Console.WriteLine("draw!\nplayer 1 score: " + m_playerOne.points + "\nCPU score: " + m_playerAi.Points);
                    }

                    Console.WriteLine("would you want to continue the game? y/n");
                }
                else if (i_gameState == 9)
                {
                    if (m_playerTow != null)
                    {
                        Console.WriteLine("you choiced exit!\nplayer 1 score: " + m_playerOne.points + "\nplayer 2 score: " + m_playerTow.points);
                    }
                    else
                    {
                        Console.WriteLine("you choiced exit!\nplayer 1 score: " + m_playerOne.points + "\nCPU score: " + m_playerAi.Points);
                    }

                    Console.WriteLine("would you want to continue the game? y/n");
                }
                else if(i_gameState == 0)
                {
                    Console.WriteLine("want to continue the game? y/n");
                }

                tempUserChoiceCheck = Console.ReadLine();

                if (!(tempUserChoiceCheck == "y" || tempUserChoiceCheck == "n"))
                {
                    Console.WriteLine("invalid value! input y/n");
                }

                i_gameState = 11; // to not repeat the print 
            }

            if (tempUserChoiceCheck == "n")
            {
                Console.WriteLine("thank you for playing. have a nice day");
                o_isGameContinue = false;
            }
            else
            {
                m_userInterface.resetBoard();
                m_userInterface.resetOutPutPrint();
                m_userInterface.printBoard();

                o_isGameContinue = true;
            }

            return o_isGameContinue;
        }
       
        /*
         * define which player will start first you play. 
         * if the it`s player 1 - then the function will return true
         * otherwise for second player 2 - the function will return false.
         */
        private bool choiceRandomWhichPlayerBegin()
        {
            bool o_tempRandomChoice = true;
            Random randomNum = new Random();
            int randomStart = randomNum.Next(1, 2);
            
            if(randomStart == 1)
            {
                o_tempRandomChoice = true;
            }
            else
            {
                o_tempRandomChoice = false;
            }

            return o_tempRandomChoice;
        }

        /*
         * this method is check user input of size board game.
         * and define the board size.
         */
        private void userDefineBoardSize()
        {
            byte rowSize = 0;
            byte colSize = 0;
            string tempUserChoiceRowSize = null;
            string tempUserChoiceColSize = null;
            bool checkIsNum = false;

            Console.WriteLine("please enter size of Row should be between 4 and 8  - incould");
            while(!(rowSize >= 4 && rowSize <= 8))
            {
                tempUserChoiceRowSize = Console.ReadLine();
                checkIsNum = byte.TryParse(tempUserChoiceRowSize, out rowSize);

                if (checkIsNum)
                {
                    if (rowSize >= 4 && rowSize <= 8)
                    {
                        m_numOfRow = rowSize;
                    }
                    else
                    {
                        Console.WriteLine("invalid size - input between 4 and 8 - incould");
                    }
                }
                else
                {
                    Console.WriteLine("invalid size - input between 4 and 8 - incould");
                }
            }

            checkIsNum = false;

            Console.WriteLine("please enter size of Col should be between 4 and 8  - incould");

            while (!(colSize >= 4 && colSize <= 8))
            {
                tempUserChoiceColSize = Console.ReadLine();
                checkIsNum = byte.TryParse(tempUserChoiceColSize, out colSize);

                if (checkIsNum)
                {
                    if (colSize >= 4 && colSize <= 8)
                    {
                        m_numOfCol = colSize;
                    }
                    else
                {
                    Console.WriteLine("invalid size - input between 4 and 8 - incould");
                }
                }
                else
                {
                    Console.WriteLine("invalid size - input between 4 and 8 - incould");
                }
            }

            Console.WriteLine(m_numOfRow + "  " + m_numOfCol);
        }

        /*
         * user define against who he want to play,
         * player OR AI
         */
        private void defineUserChoicePlayStyle()
        {
            byte userChoiceNum = 0;
            string tempUserChoiceCheck = null;
            bool checkIsNum = false;
            Console.WriteLine("now, choice against who you want to play:\nfor another player press 1\nagainst AI press 2");

            while(!(userChoiceNum == 1 || userChoiceNum == 2))
            {
                tempUserChoiceCheck = Console.ReadLine();
                checkIsNum = byte.TryParse(tempUserChoiceCheck, out userChoiceNum);

                if (checkIsNum)
                {
                    if (!(userChoiceNum == 1 || userChoiceNum == 2))
                    {
                        Console.WriteLine("invalid value! input number 1 or 2");
                    }
                }
                else
                {
                    Console.WriteLine("invalid value! input number 1 or 2");
                }
            }

            /// else -> 2
            if(userChoiceNum == 1)
            {
                Console.WriteLine("well done! you play against Player, gl!");
            }
            else
            {
                Console.WriteLine("well done! you play against AI, gl!");
            }

            m_userChoice = userChoiceNum;
        }

        /*
         * this method is operate the players OR AI game
         */
        private void defineDirectionOfGameStyle()
        {
            m_gameBoard = new char[m_numOfRow, m_numOfCol];
            m_GameRule = new Rules(ref m_gameBoard);

            if (m_userChoice == 1)
            {
                m_playerOne = new Player();
                m_playerTow = new Player();
            }
            else
            {
                m_playerOne = new Player();
                m_playerAi = new Aiplayer(ref m_gameBoard, ref m_GameRule);
            }
        }
    }
}
