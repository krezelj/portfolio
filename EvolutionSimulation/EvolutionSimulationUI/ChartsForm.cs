using EvolutionSimulatorLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace EvolutionSimulationUI
{
    public partial class ChartsForm : Form
    {
        private int _tickInterval = 100;

        private int _timeSpan = 200000;


        public ChartsForm()
        {
            InitializeComponent();
        }

        private void ChartsForm_Load(object sender, EventArgs e)
        {
            Size = new Size(636, 1047);
            Location = new Point(1280, 0);
            WindowState = FormWindowState.Minimized;

            InitializeCharts();
            Manager.TickEnded += OnTickEnded;
        }

        private void OnTickEnded()
        {
            try
            {
                if (Manager.TicksElapsed % _tickInterval == 0)
                {
                    Invoke((MethodInvoker)delegate { UpdateCharts(); });
                }
            }
            catch (Exception) { }
        }

        public void ResetCharts()
        {
            DataManager.ResetAllSeries();
        }

        private void InitializeCharts()
        {
            InitializeChart(ChartOne);
            ChartOne.Series.Add(DataManager.MeanSize);

            InitializeChart(ChartTwo);
            ChartTwo.Series.Add(DataManager.MeanSpeed);

            InitializeChart(ChartThree);
            ChartThree.Series.Add(DataManager.MeanRange);

            InitializeChart(ChartFour);
            ChartFour.Series.Add(DataManager.MeanPredatoriness);
            ChartFour.Series.Add(DataManager.MaxPredatoriness);
            ChartFour.Series.Add(DataManager.MinPredatoriness);
        }

        private void InitializeChart(Chart chart)
        {
            chart.ChartAreas[0].AxisX.Minimum = Math.Max(Manager.TicksElapsed - _timeSpan, 0);
            chart.ChartAreas[0].AxisX.Maximum = Math.Max(Manager.TicksElapsed, 2000);
            chart.Series.Clear();
        }

        private void UpdateCharts()
        {
            DataManager.UpdateSeries();
            UpdateChart(ChartOne);
            UpdateChart(ChartTwo);
            UpdateChart(ChartThree);
            UpdateChart(ChartFour);
        }

        private void UpdateChart(Chart chart)
        {
            chart.ChartAreas[0].AxisX.Minimum = Math.Max(Manager.TicksElapsed - _timeSpan, 0);
            chart.ChartAreas[0].AxisX.Maximum = Math.Max(Manager.TicksElapsed, 2000);
        }

        private void ChartsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Manager.TickEnded -= OnTickEnded;
        }
    }
}
