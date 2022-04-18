using PatchedConicsLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatchedConicsPrototypeUI
{
    public partial class CanvasForm : Form
    {
        Stopwatch _timer = new Stopwatch();

        Point _lastMouseLocation;

        int _focusedBodyIndex = 0;

        public CanvasForm()
        {
            InitializeComponent();
            MouseWheel += CanvasForm_MouseWheel;
        }

        private void CanvasForm_Load(object sender, EventArgs e)
        {
            ClientSize = new Size(800, 800);
            Manager.CanvasForm = this;
            CanvasManager.CanvasForm = this;
            Manager.Start();
        }

        private void CanvasForm_Paint(object sender, PaintEventArgs e)
        {
            
            Text = Manager.ElapsedTicks.ToString();
            _timer.Restart();
            CanvasManager.DrawAll(e.Graphics);
            //MessageBox.Show(_timer.ElapsedMilliseconds.ToString());
            Manager.CanvasRepainted.Set();
        }

        private void CanvasForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                CanvasManager.FocusedBody = null;
            }
        }



        private void CanvasForm_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Math.Sign(e.Delta) < 0)
                CanvasManager.Scale *= (6.0/5);
            else
                CanvasManager.Scale *= (5.0/6);
        }

        private void CanvasForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && CanvasManager.FocusedBody == null)
            {
                int dx = e.Location.X - _lastMouseLocation.X;
                int dy = e.Location.Y - _lastMouseLocation.Y;
                CanvasManager.CenterOffset.X += dx;
                CanvasManager.CenterOffset.Y += dy;
            }
            _lastMouseLocation = e.Location;
        }

        private void CanvasForm_KeyDown(object sender, KeyEventArgs e)
        {
            int bodyCount = Space.Bodies.Count();
            switch(e.KeyCode)
            {
                case Keys.Left:
                    _focusedBodyIndex = (_focusedBodyIndex + (bodyCount - 1)) % bodyCount;
                    CanvasManager.FocusedBody = Space.Bodies[_focusedBodyIndex];
                    break;
                case Keys.Right:
                    _focusedBodyIndex = (_focusedBodyIndex + 1) % bodyCount;
                    CanvasManager.FocusedBody = Space.Bodies[_focusedBodyIndex];
                    break;
            }
        }
    }
}
