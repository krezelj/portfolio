using EvolutionSimulatorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EvolutionSimulatorLibrary
{
    public static class Manager
    {
        public static Random Rng { get; private set; }

        public static Stopwatch Timer { get; private set; }

        private static AutoResetEvent _tickCanContinue { get; } = new AutoResetEvent(false);

        public static AutoResetEvent CanvasRepainted { get; } = new AutoResetEvent(false);


        public static event Action TickEnded;

        public static event Action ActiveCreatureChanged;

        public static Form PlaneForm { get; set; }

        public static PlaneModel Plane { get; set; }

        public static CreatureModel ActiveCreature { get; set; }

        private static bool _formClosed;


        public static int TicksElapsed { get; set; }

        public static long MilisecondsPerTick { get; private set; }

        public static int FoodSpawnInterval { get; set; }

        public static int FoodSpawnAmount { get; set; }

        public static int TicksPerFrame { get; set; }

        public static bool SimulationPaused { get; set; }

        

        static Manager()
        {
            Rng = new Random(1);
            Timer = new Stopwatch();
            FoodSpawnInterval = 3;
            FoodSpawnAmount = 2;
            TicksPerFrame = 1;
            SimulationPaused = true;
        }

        public static void Start(Form planeForm, System.Drawing.Size planeSize)
        {
            // Prepare the simulation
            Plane = new PlaneModel(planeSize);
            _formClosed = false;
            PlaneForm = planeForm;
            PlaneForm.FormClosed += OnFormClosed;
            PlaneForm.MouseDown += OnMouseDown;

            TicksElapsed = 0;
            Plane.SpawnCreatures(50);
            Plane.SpawnFood(1200);
            Task.Run(() => MainLoop());
        }

        private static void RunClock()
        {
            while (true)
            {
                Thread.Sleep(10);
                if (!SimulationPaused)
                    _tickCanContinue.Set();
            }
        }

        private static void MainLoop()
        {
            // Start the clock
            Task.Run(() => RunClock());
            do
            {
                Timer.Restart();
                // Check if canvas is not being repainted.
                CanvasRepainted.WaitOne();
                // Spawn new food
                if (TicksElapsed % FoodSpawnInterval == 0)
                {
                    Plane.SpawnFood(FoodSpawnAmount);
                }
                // Spawn new creatures
                Plane.SpawnCreatures();
                // Perform actions
                foreach (var creature in Plane.Creatures)
                {
                    creature.Update();
                }
                // Kill creatures
                Plane.KillCreatures();
                // Raise TickEnded event.
                TickEnded?.Invoke();
                // Wait until minimum tick length reached.
                _tickCanContinue.WaitOne();
                // Finish the tick.
                MilisecondsPerTick = Timer.ElapsedMilliseconds;
                ++TicksElapsed;
            } while (Plane.Creatures.Count > 0 && !_formClosed);
        }

        private static void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ActiveCreature = Plane.GetClosestCreature(e.X, e.Y, 20);
                ActiveCreatureChanged?.Invoke();
            }
            else if (e.Button == MouseButtons.Right)
            {
                ActiveCreature = null;
                ActiveCreatureChanged?.Invoke();
            }
        }

        private static void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            PlaneForm.FormClosed -= OnFormClosed;
            PlaneForm = null;
            _formClosed = true;
            CanvasRepainted.Set();
        }

    }
}
