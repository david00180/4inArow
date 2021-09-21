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
    public partial class GameSettings : Form
    { 
        private byte m_rowSize = 4;
        private byte m_colSize = 4;

        private string m_playerOneName = string.Empty;
        private string m_playerTwoName = string.Empty;

        private bool m_isPVP = false;
        private bool m_isGameContinue = false;

        public GameSettings()
        {
            InitializeComponent();
        }

        public bool isGameContinue
        {
            get { return this.m_isGameContinue; }
        }

        public byte rowSize
        {
            get { return this.m_rowSize; }
        }

        public byte colSize
        {
            get { return this.m_colSize; }
        }

        public string playerOneName
        {
            get { return this.m_playerOneName; }
        }

        public string playerTwoName
        {
            get { return this.m_playerTwoName; }
        }

        public bool isPvpGame
        {
            get { return this.m_isPVP; }
        } 

        /// Here it will be defined whether the user will play against the computer or against another player
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.textBox2.Enabled)
            {
                this.textBox2.Enabled = true;
                this.m_isPVP = true;
            }
            else
            {
                this.textBox2.Enabled = false;
                this.m_isPVP = true;
            }
        }

        /// Here we close the " game settings " form 
        private void button1_Click(object sender, EventArgs e)
        {
            if (m_isPVP)
            {
                if (this.textBox1.Text != string.Empty && this.textBox2.Text != string.Empty)
                {
                    this.m_playerOneName = this.textBox1.Text + ": ";
                    this.m_playerTwoName = this.textBox2.Text + ": ";
                    this.m_isGameContinue = true;
                    this.Close();
                }
            }
            else
            {
                if (this.textBox1.Text != string.Empty)
                {
                    this.m_playerOneName = this.textBox1.Text + ": ";
                    this.m_playerTwoName = "computer: ";
                    this.m_isGameContinue = true;
                    this.Close();
                }
            }         
        }

        /// Here we save the Row size from user choice
        private void numericUpDownRowSize(object sender, EventArgs e)
        {
            this.m_rowSize = byte.Parse(this.numericUpDown1.Value.ToString());
        }

        /// Here we save the Col size from user choice
        private void numericUpDownColSize(object sender, EventArgs e)
        {
            this.m_colSize = byte.Parse(this.numericUpDown2.Value.ToString());
        }
    }
}