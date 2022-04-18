using EvolutionSimulatorLibrary;
using EvolutionSimulatorLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EvolutionSimulationUI
{
    public partial class PlaneForm : Form
    {

        private AutoResetEvent _canPaint = new AutoResetEvent(true);

        public PlaneForm()
        {
            InitializeComponent();
        }

        private void PlaneForm_Load(object sender, EventArgs e)
        {
            ClientSize = new Size(1800, 960);
            Location = new Point(60, 0);
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            Manager.TickEnded += OnTickEnded;
            Manager.Start(this, new Size(ClientSize.Width, ClientSize.Height));
        }

        private void OnTickEnded()
        {
            try
            {
                _canPaint.Set();
                Invoke((MethodInvoker)delegate { Text = Manager.MilisecondsPerTick.ToString(); });
                if (Manager.TicksElapsed % Manager.TicksPerFrame == 0)
                    Invoke((MethodInvoker)delegate { Invalidate(); });
                else
                    Manager.CanvasRepainted.Set();
            }
            catch (Exception) { }
        }

        private void PlaneForm_Paint(object sender, PaintEventArgs e)
        {
            _canPaint.WaitOne(10);
            CanvasManager.DrawAll(e.Graphics); ;
            Manager.CanvasRepainted.Set();
        }

        private void PlaneForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Manager.TickEnded -= OnTickEnded;
        }
    }
}
