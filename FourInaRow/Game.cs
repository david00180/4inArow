using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C21_Ex05_Shai313189490_David_208430165
{
    public partial class Game : Form
    {
        private GameSettings m_gameSettings = null;

        private Rules m_rules = null;

        private char[,] m_board = null;

        private Player m_playerOne = null;
        private Player m_playerTwo = null;

        private Aiplayer m_playerAi = null;

        private string m_firstPlayerName = string.Empty;
        private string m_secondPlayerName = string.Empty;

        public Game(GameSettings i_gameSettings)
        {
            m_gameSettings = i_gameSettings;

            m_board = new char[m_gameSettings.rowSize, m_gameSettings.colSize];

            m_rules = new Rules(ref m_board);

            m_firstPlayerName = m_gameSettings.playerOneName;
            m_secondPlayerName = m_gameSettings.playerTwoName;

            InitializeOrCPU(m_gameSettings);

            InitializeComponent(m_gameSettings);
        }

        /// set the players, pvp or cpu
        private void InitializeOrCPU(GameSettings i_gameSettings)
        {
            m_playerOne = new Player();
            m_playerOne.IsMyTurn = true;

            if (i_gameSettings.isPvpGame)
            {
                m_playerTwo = new Player();
            }
            else
            {
                m_playerAi = new Aiplayer(ref m_board, ref m_rules);
                m_playerAi.setColLength(m_gameSettings.colSize);
                m_playerAi.setRowLength(m_gameSettings.rowSize);
            }
        }

        private void userClickButtomChoice(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (m_gameSettings.isPvpGame)
            {
                gameOfPVP(button);
            }
            else
            {
                gameOfAI(button);
            }
        }

        private void gameOfAI(Button i_button)
        {
            DialogResult? dialogResult;

            byte choice = byte.Parse(i_button.Text); // col index
            choice -= 1;

            if (m_playerOne.IsMyTurn)
            {
                byte rowPosition = m_rules.getPointRowKnownCol(choice, m_gameSettings.rowSize);
                this.m_outPutViewGameState[rowPosition, choice].Text = "x";
                m_board[rowPosition, choice] = 'x';

                m_playerOne.IsMyTurn = false;

                if (m_rules.checkRow(rowPosition, choice, 'x') || m_rules.checkCol(rowPosition, choice, 'x') || m_rules.checkDiagonalDescends(rowPosition, choice, 'x') || m_rules.checkDiagonalRising(rowPosition, choice, 'x'))
                {
                    m_playerOne.points++;

                    dialogResult = MessageBox.Show(m_firstPlayerName + "Win!!\nAnother Round?", "A win!", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        this.m_firstPlayer.Text = m_firstPlayerName + m_playerOne.points.ToString();
                        m_rules.resetBoard(m_gameSettings.rowSize, m_gameSettings.colSize);
                        resetUIboard();
                    }
                    else
                    {
                        Close();
                    }
                }
            }

            /// AI part
                m_playerAi.bestMove();
                m_board[m_playerAi.getFillRow(), m_playerAi.getFillCol()] = 'o';
                this.m_outPutViewGameState[m_playerAi.getFillRow(), m_playerAi.getFillCol()].Text = "o";
                m_playerOne.IsMyTurn = true;
                if (isWin(m_playerAi.getFillRow(), m_playerAi.getFillCol(), 'o'))
                {
                    m_playerAi.Points++;
                    dialogResult = MessageBox.Show("CPU Win!!\nAnother Round?", "A win!", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        this.m_secondPlayer.Text = m_secondPlayerName + m_playerAi.Points.ToString();
                        m_rules.resetBoard(m_gameSettings.rowSize, m_gameSettings.colSize);

                        resetUIboard();
                    }
                    else
                    {
                        Close();
                    }
                }

            /// draw
            if (!m_rules.isBoardFull(m_gameSettings.colSize))
            {
                dialogResult = MessageBox.Show("tir!!\nAnother Round?", "a Tie!", MessageBoxButtons.YesNo);

                m_playerOne.points = 0;
                m_playerAi.Points = 0;

                if (dialogResult == DialogResult.Yes)
                {
                    resetUIboard();
                    m_rules.resetBoard(m_gameSettings.rowSize, m_gameSettings.colSize);
                    this.m_firstPlayer.Text = m_firstPlayerName + m_playerOne.points.ToString();
                    this.m_secondPlayer.Text = m_secondPlayerName + m_playerAi.Points.ToString();
                }
                else
                {
                    Close();
                }
            }
        }  
        
         /// this function is return true only if the player is win 
        private bool isWin(byte i_rowPosition, byte i_userColChoiceCol, char i_sign)
        {
            bool o_rowCheckWin = false;
            bool o_colCheckWin = false;
            bool o_diagonalDescendsCheckWin = false;
            bool o_diagonalRisingCheckWin = false;
            o_rowCheckWin = m_rules.checkRow(i_rowPosition, i_userColChoiceCol, i_sign);
            o_colCheckWin = m_rules.checkCol(i_rowPosition, i_userColChoiceCol, i_sign);
            o_diagonalDescendsCheckWin = m_rules.checkDiagonalDescends(i_rowPosition, i_userColChoiceCol, i_sign);
            o_diagonalRisingCheckWin = m_rules.checkDiagonalRising(i_rowPosition, i_userColChoiceCol, i_sign);

            return o_rowCheckWin || o_colCheckWin || o_diagonalDescendsCheckWin || o_diagonalRisingCheckWin;
        }
        
        /// this method is for PVP game 
        private void gameOfPVP(Button i_button)
        {
            DialogResult? dialogResult;
            byte choice = byte.Parse(i_button.Text); // col index
            choice -= 1;

            if (!(m_board[0, choice] == 'x' || m_board[0, choice] == 'o'))
            {
                if (m_playerOne.IsMyTurn)
                {
                    byte rowPosition = m_rules.getPointRowKnownCol(choice, m_gameSettings.rowSize);
                    this.m_outPutViewGameState[rowPosition, choice].Text = "x";
                    m_board[rowPosition, choice] = 'x';

                    m_playerOne.IsMyTurn = false;

                    if (m_rules.checkRow(rowPosition, choice, 'x') || m_rules.checkCol(rowPosition, choice, 'x') || m_rules.checkDiagonalDescends(rowPosition, choice, 'x') || m_rules.checkDiagonalRising(rowPosition, choice, 'x'))
                    {
                        m_playerOne.points++;
                        dialogResult = MessageBox.Show(m_firstPlayerName + "Win!!\nAnother Round?", "A win!", MessageBoxButtons.YesNo);

                        if(dialogResult == DialogResult.Yes)
                        {
                            this.m_firstPlayer.Text = m_firstPlayerName + m_playerOne.points.ToString();
                            m_rules.resetBoard(m_gameSettings.rowSize, m_gameSettings.colSize);
                            resetUIboard();
                        }
                        else
                        {
                            Close();
                        }
                    }
                }
                else
                {
                    byte rowPosition = m_rules.getPointRowKnownCol(choice, m_gameSettings.rowSize);
                    this.m_outPutViewGameState[rowPosition, choice].Text = "o";
                    m_board[rowPosition, choice] = 'o';

                    m_playerOne.IsMyTurn = true;

                    if (m_rules.checkRow(rowPosition, choice, 'o') || m_rules.checkCol(rowPosition, choice, 'o') || m_rules.checkDiagonalDescends(rowPosition, choice, 'o') || m_rules.checkDiagonalRising(rowPosition, choice, 'o'))
                    {
                        dialogResult = MessageBox.Show(m_secondPlayerName + "Win!!\nAnother Round?", "A win!", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            m_playerTwo.points++;
                            this.m_secondPlayer.Text = m_secondPlayerName + m_playerTwo.points.ToString();
                            m_rules.resetBoard(m_gameSettings.rowSize, m_gameSettings.colSize);
                            resetUIboard();
                        }
                        else
                        {
                            Close();
                        }
                    }
                }

                /// draw
                if (!m_rules.isBoardFull(m_gameSettings.colSize))
                {
                    dialogResult = MessageBox.Show("tir!!\nAnother Round?", "a Tie!", MessageBoxButtons.YesNo);

                    m_playerOne.points = 0;
                    m_playerTwo.points = 0;

                    if (dialogResult == DialogResult.Yes)
                    {
                        resetUIboard();
                        m_rules.resetBoard(m_gameSettings.rowSize, m_gameSettings.colSize);

                        this.m_firstPlayer.Text = m_firstPlayerName + m_playerOne.points.ToString();
                        this.m_secondPlayer.Text = m_secondPlayerName + m_playerTwo.points.ToString();
                    }
                    else
                    {
                        Close();
                    }
                }
            }
        }

        /// this method is reset the UI board 
        private void resetUIboard()
        {
            for (int i = 0; i < m_gameSettings.rowSize; i++)
            {
                for (int j = 0; j < m_gameSettings.colSize; j++)
                {
                    this.m_outPutViewGameState[i, j].Text = string.Empty;
                }
            }
        }
    }
}