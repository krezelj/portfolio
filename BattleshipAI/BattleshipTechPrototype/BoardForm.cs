using BattleshipTechPrototypeLIB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipTechPrototype
{
    public partial class BoardForm : Form
    {
        public int counter = 0;

        public BoardForm()
        {
            InitializeComponent();
            ClientSize = new Size(1000, 500);
            GameManager.Canvas = this;
            Refresh();
        }

        private void BoardForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (ShowEnemyValuesCheckBox.CheckState == CheckState.Checked)
            {
                GameManager.Enemy2.ShowValues(int.Parse(ShowValueModeBox.Text));
                if (GameManager.Stage < 3)
                {
                    ShowEnemyValuesCheckBox.CheckState = CheckState.Unchecked;
                    ShowEnemyValuesCheckBox.Enabled = false;
                }
            }
            if (GameManager.Player1Board.EditMode || GameManager.Player1Board.PlayMode)
            {
                CanvasManager.DrawBoard(g, GameManager.Player1Board, DisplayMode.ShowAll);
            }
            else
            {
                CanvasManager.DrawBoard(g, GameManager.Player1Board, DisplayMode.ShowHit);
            }
            if (GameManager.Player2Board.EditMode || GameManager.Player2Board.PlayMode)
            {
                CanvasManager.DrawBoard(g, GameManager.Player2Board, DisplayMode.ShowAll);
            }
            else
            {
                CanvasManager.DrawBoard(g, GameManager.Player2Board, DisplayMode.ShowHit);
            }
        }

        private void BoardForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (GameManager.Player1Board.UpdateHighlightedCell(e.X, e.Y, true) || GameManager.Player2Board.UpdateHighlightedCell(e.X, e.Y, true))
            {
                Invalidate();
            }
        }

        private void BoardForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (GameManager.Player1Board.EditMode)
                {
                    GameManager.Player1Board.UpdateShips(e.X, e.Y, true);
                }
                else if (GameManager.Player2Board.EditMode)
                {
                    GameManager.Player2Board.UpdateShips(e.X, e.Y, true);
                }
                
            }  
            Invalidate();
        }

        private void BoardForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (GameManager.Player1Board.PlayMode && !GameManager.IsFinished)
                {
                    counter++;
                    this.Text = counter.ToString();
                    if (GameManager.Player2Board.HitCell(e.X, e.Y, true))
                    {
                        Invalidate();
                        GameManager.AdvanceGame();
                    }

                }
                else if (GameManager.Player2Board.PlayMode && !GameManager.IsFinished)
                {
                    if (GameManager.Player1Board.HitCell(e.X, e.Y, true))
                    {
                        if (ShowEnemyValuesCheckBox.CheckState == CheckState.Checked)
                        {
                            GameManager.Enemy1.ShowValues(int.Parse(ShowValueModeBox.Text));
                        }
                        Invalidate();
                        GameManager.AdvanceGame();
                    }
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                GameManager.Player1Board.ShowValue(e.X, e.Y, true, int.Parse(ShowValueModeBox.Text));
                GameManager.Player2Board.ShowValue(e.X, e.Y, true, int.Parse(ShowValueModeBox.Text));
            }
        }

        private void ClearBoardButton_Click(object sender, EventArgs e)
        {
            if (GameManager.Player1Board.EditMode)
            {
                GameManager.Player1Board.ClearBoard();
            }
            if (GameManager.Player2Board.EditMode)
            {
                GameManager.Player2Board.ClearBoard();
            }
            Invalidate();
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            string validationMessage = "";
            if (GameManager.Player1Board.EditMode)
            {
                //GameManager.Player1Board.CreateShips();
                if (!GameManager.Player1Board.HardValidateBoard(out validationMessage))
                {
                    ValidationMessageLabel.ForeColor = Color.Red;
                }
                else
                {
                    ValidationMessageLabel.ForeColor = Color.Black;
                    ShowEnemyValuesCheckBox.Enabled = true;
                    GameManager.AdvanceGame();
                }
            }
            else if (GameManager.Player2Board.EditMode)
            {
                //GameManager.Player2Board.CreateShips();
                if (!GameManager.Player2Board.HardValidateBoard(out validationMessage))
                {
                    ValidationMessageLabel.ForeColor = Color.Red;
                }
                else
                {
                    ValidationMessageLabel.ForeColor = Color.Black;
                    ShowEnemyValuesCheckBox.Enabled = true;
                    GameManager.AdvanceGame();
                }
            }
            ValidationMessageLabel.Text = validationMessage;
            Invalidate();
        }

        private void ShowEnemyValuesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowEnemyValuesCheckBox.CheckState == CheckState.Unchecked)
            {
                for (int i = 0; i < GameManager.Player1Board.NoRows; i++)
                {
                    for (int j = 0; j < GameManager.Player1Board.NoCols; ++j)
                    {
                        GameManager.Player1Board.Cells[i, j].UpdateCellColor();
                    }
                }
            }
            Invalidate();
        }

        private void BoardForm_Load(object sender, EventArgs e)
        {
            //int sum = 0;
            //for (int i = 0; i < 10000; i++)
            //{
            //    GameManager.InitializeGame();
            //    sum += GameManager.Stage;
            //    //MessageBox.Show(GameManager.Stage.ToString());
            //}
            //MessageBox.Show($"Player1: {GameManager.player1wins }, Player2: { GameManager.player2wins }. Avg: { (double)sum / 10000 }");
            //MessageBox.Show($"{ (double)sum/1000 }");
            //Text = GameManager.Stage.ToString();
            GameManager.InitializeGame();
        }
    }
}
