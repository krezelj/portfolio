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

namespace EvolutionSimulationUI
{
    public partial class ControlsForm : Form
    {
        PlaneForm _planeForm;

        ChartsForm _chartsForm;

        public ControlsForm()
        {
            InitializeComponent();
        }

        private void ControlsForm_Load(object sender, EventArgs e)
        {
            Size = new Size(250, 539);
            Location = new Point(0, 0);
            WindowState = FormWindowState.Minimized;

            Manager.TickEnded += OnTickEnded;
            Manager.ActiveCreatureChanged += OnActiveCreatureChanged;

            OpenChartsDialog();
            OpenPlaneDialog();
        }

        private void OnTickEnded()
        {
            try
            {
                // Invoke((MethodInvoker)delegate { Text = Manager.TicksElapsed.ToString(); });
                Invoke((MethodInvoker)delegate { UpdateGeneralInformationLabels(); });
            }
            catch (Exception) {}
            
        }

        private void UpdateGeneralInformationLabels()
        {
            CreatureCountLabel.Text = $"Creature Count: {Manager.Plane.Creatures.Count.ToString()}";
            FoodCountLabel.Text = $"Food Count: {Manager.Plane.Food.Count.ToString()}";
            TickLenghtLabel.Text = $"Tick Lenght: {Manager.MilisecondsPerTick.ToString()}ms";
        }

        private void OnActiveCreatureChanged()
        {
            try
            {
                Invoke((MethodInvoker)delegate { UpdateCreatureInformationLabels(); });
            }
            catch (Exception) {}
        }

        private void UpdateCreatureInformationLabels()
        {
            if (Manager.ActiveCreature != null)
            {
                GenerationLabel.Text = $"Generation: {Manager.ActiveCreature.Generation}";
                EnergyLabel.Text = $"Energy: {Manager.ActiveCreature.Energy}";
                SizeLabel.Text = $"Size: {Manager.ActiveCreature.Size}";
                SpeedLabel.Text = $"Speed: {Manager.ActiveCreature.Speed}";
                RangeLabel.Text = $"Range: {Manager.ActiveCreature.Range}";
                PredatorinessLabel.Text = $"Predatoriness: {Manager.ActiveCreature.Predatoriness}";
            }
            else
            {
                GenerationLabel.Text = "Generation:";
                EnergyLabel.Text = "Energy:";
                SizeLabel.Text = "Size:";
                SpeedLabel.Text = "Speed:";
                RangeLabel.Text = "Range:";
                PredatorinessLabel.Text = "Predatoriness:";
            }
        }


        private void OpenPlaneDialog()
        {
            if (_planeForm == null)
            {
                _planeForm = new PlaneForm();
                _planeForm.FormClosed += OnPlaneDialogClosed;
                Task.Run(() => _planeForm.ShowDialog());
            }
        }

        private void OnPlaneDialogClosed(object sender, FormClosedEventArgs e)
        {
            _planeForm.FormClosed -= OnPlaneDialogClosed;
            _planeForm = null;
            _chartsForm?.Invoke((MethodInvoker)delegate { _chartsForm.ResetCharts(); });
        }

        private void OpenChartsDialog()
        {
            if (_chartsForm == null)
            {
                _chartsForm = new ChartsForm();
                _chartsForm.FormClosed += OnChartsDialogClosed;
                Task.Run(() => _chartsForm.ShowDialog());
            }
        }

        private void OnChartsDialogClosed(object sender, FormClosedEventArgs e)
        {
            _chartsForm.FormClosed -= OnChartsDialogClosed;
            _chartsForm = null;
        }

        private void OpenPlaneButton_Click(object sender, EventArgs e)
        {
            OpenPlaneDialog();
            ActiveControl = null;
        }

        private void OpenChartsButton_Click(object sender, EventArgs e)
        {
            OpenChartsDialog();
            ActiveControl = null;
        }

        private void ControlsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Manager.TickEnded -= OnTickEnded;
        }

        private void DrawSectorsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CanvasManager.SectorsVisible = DrawSectorsCheckBox.Checked;
        }

        private void DrawRangeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CanvasManager.RangeVisible = DrawRangeCheckBox.Checked;
        }

        private void ColorCodeSpeedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CanvasManager.ColorCodeSpeed = ColorCodeSpeedCheckBox.Checked;
        }

        private void ColorCodePredatorinessCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CanvasManager.ColorCodePredatoriness = ColorCodePredatorinessCheckBox.Checked;
        }

        private void IgnoreSizeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CanvasManager.IgnoreSize = IgnoreSizeCheckBox.Checked;
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            Manager.SimulationPaused = !Manager.SimulationPaused;
            PauseButton.Text = Manager.SimulationPaused ? "Resume" : "Pause";
        }
    }
}
