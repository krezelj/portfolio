using CheckersGame2LIB;
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

namespace CheckersGame2
{
    public partial class BoardForm : Form
    {

        public Button LauncherPlayButton;

        public BoardForm()
        {
            InitializeComponent();
        }

        private void BoardForm_Load(object sender, EventArgs e)
        {
            ClientSize = new Size(800, 800);
            // TODO Move this to launcher
            
            Manager.NodesCountLabel = NodesCountLabel;
            Manager.OutcomesCountLabel = OutcomesCountLabel;
            Manager.BoardValueNumberLabel = BoardValueNumberLabel;
            Manager.StartNewGame();

            // ==========================
        }

        private void BoardForm_Paint(object sender, PaintEventArgs e)
        {
            CanvasManager.RedrawAll(e.Graphics);
        }

        private void BoardForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Manager.GameClosed = true;
        }
    }
}
