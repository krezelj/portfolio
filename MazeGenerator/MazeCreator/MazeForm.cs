using MazeCreatorLIB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeCreator
{
    public partial class MazeForm : Form
    {
        public CanvasManager canvasManager;

        public int MazeScale;



        public MazeForm()
        {
            InitializeComponent();
        }

        private void MazeForm_Load(object sender, EventArgs e)
        {
            MazeScale = Manager.Maze.Rows * 10 > 800 || Manager.Maze.Cols * 10 > 800 ? Manager.Maze.Rows * 6 > 800 || Manager.Maze.Cols * 6 > 800 ? 3 : 6 : 10;
            ClientSize = new Size(Manager.Cols * MazeScale, Manager.Rows * MazeScale);
            canvasManager = new CanvasManager();
            BackgroundMazeCreator.RunWorkerAsync();
            Manager.MazeForm = this;
        }

        private void MazeForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            canvasManager.DrawMaze(g, ClientSize);
        }

        private void MazeForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Manager.Maze.IsFinished)
            {
                Manager.SetStartNode(e.X, e.Y, MazeScale);
            }
            else if (e.Button == MouseButtons.Right && Manager.Maze.IsFinished)
            {
                Manager.SetEndNode(e.X, e.Y, MazeScale);
            }
            Manager.FindPathButton.Enabled = (Manager.StartNode != null && Manager.EndNode != null);
            Invalidate();
        }

        private void BackgroundMazeCreator_DoWork(object sender, DoWorkEventArgs e)
        {
            bool invalidate = true;
            do
            {
                try
                {
                    // if (invalidate)
                        //this.Invoke((MethodInvoker)delegate { this.Invalidate(); });
                }
                catch (Exception)
                {
                    //throw;
                }
                //Thread.Sleep(50);
            } while (!Manager.Maze.CreateMazeNextStep(out invalidate));
            this.Invoke((MethodInvoker)delegate { this.Invalidate(); });
            //MessageBox.Show("Finished");
        }

        private void BackgroundMazeCreator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void MazeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Manager.CreateMazeButton.Enabled = true;
            Manager.FindPathButton.Enabled = false;
        }

    }
}
