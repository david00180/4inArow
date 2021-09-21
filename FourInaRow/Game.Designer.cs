using System.Windows.Forms;

namespace C21_Ex05_Shai313189490_David_208430165
{
    public partial class Game
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent(GameSettings i_gameSettings)
        {
            byte rowSize = i_gameSettings.rowSize;
            byte colSize = i_gameSettings.colSize;

            string playerOneName = i_gameSettings.playerOneName;
            string playerTwoName = i_gameSettings.playerTwoName;

            bool isPVP = i_gameSettings.isPvpGame;

            setFormSize(rowSize, colSize);

            setClickButtomInARow(colSize);

            setGameStateView(rowSize, colSize);

            setPlayersNames(playerOneName, playerTwoName, rowSize, colSize);

            checkClick();

        }
        private void checkClick()
        {
            for (int i = 0; i < m_arrayOfButtonsUserChoice.Length; i++)
            {
                this.m_arrayOfButtonsUserChoice[i].Click += new System.EventHandler(this.userClickButtomChoice);
            }
        }

        // set the Form size
        private void setFormSize(byte i_rowSize, byte i_colSize)
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size((i_colSize * 100) + 50, (i_rowSize * 100) + 50);
            this.Text = "Game";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        }

        // set the buttoms row line 
        private void setClickButtomInARow(byte i_colSize)
        {
            m_arrayOfButtonsUserChoice = new Button[i_colSize];
            short positionLeft = 50;
            short positionTop = 25;

            for (int i = 0; i < i_colSize; i++)
            {
                m_arrayOfButtonsUserChoice[i] = new Button();
                m_arrayOfButtonsUserChoice[i].Location = new System.Drawing.Point(positionLeft, positionTop);
                m_arrayOfButtonsUserChoice[i].Text = (i + 1).ToString();
                m_arrayOfButtonsUserChoice[i].Size = new System.Drawing.Size(50, 50);
                m_arrayOfButtonsUserChoice[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));

                positionLeft += 100;

                this.Controls.Add(m_arrayOfButtonsUserChoice[i]);
            }

        }

        // set the view of the game progress 
        private void setGameStateView(byte i_rowSize, byte i_colSize)
        {
            m_outPutViewGameState = new Button[i_rowSize, i_colSize];

            short positionLeft = 50;
            short positionTop = 100;

            for (int i = 0; i < i_rowSize; i++)
            {
                for (int j = 0; j < i_colSize; j++)
                {
                    m_outPutViewGameState[i, j] = new Button();
                    m_outPutViewGameState[i, j].Enabled = false;
                    m_outPutViewGameState[i, j].Text = string.Empty;
                    m_outPutViewGameState[i, j].Size = new System.Drawing.Size(50, 50);
                    m_outPutViewGameState[i, j].Location = new  System.Drawing.Point(positionLeft, positionTop);
                    m_outPutViewGameState[i, j].Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
                    m_outPutViewGameState[i, j].BackColor = System.Drawing.SystemColors.ActiveCaption;
                    m_outPutViewGameState[i, j].FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    positionLeft += 100;

                    this.Controls.Add(m_outPutViewGameState[i, j]);
                }

                positionTop += 60;
                positionLeft = 50;

            }

        }

        // set the players names 
        private void setPlayersNames(string i_firstPlayerName, string i_secondPlayerName, byte i_rowSize, byte i_colSize)
        {
            m_firstPlayer = new Label();
            m_secondPlayer = new Label();

            m_firstPlayer.Text = i_firstPlayerName + m_playerOne.points.ToString();

            if (m_gameSettings.isPvpGame)
            {
                m_secondPlayer.Text = i_secondPlayerName + m_playerTwo.points.ToString();
            }
            else
            {
                m_secondPlayer.Text = i_secondPlayerName + m_playerAi.Points.ToString();
            }

            m_firstPlayer.Size = new System.Drawing.Size(150, 70);
            m_secondPlayer.Size = new System.Drawing.Size(150, 70);

            m_firstPlayer.Location = new System.Drawing.Point((i_colSize * 100) / (i_colSize), (i_rowSize * 100) - 30);
            m_secondPlayer.Location = new System.Drawing.Point((i_colSize * 100) - ((i_colSize * 100) / i_colSize), (i_rowSize * 100) - 30);

            m_firstPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            m_secondPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));

            this.Controls.Add(m_firstPlayer);
            this.Controls.Add(m_secondPlayer);
        }

        #endregion

        private Button[] m_arrayOfButtonsUserChoice = null;
        private Button[,] m_outPutViewGameState = null;

        private Label m_firstPlayer = null;
        private Label m_secondPlayer = null;
    }
}