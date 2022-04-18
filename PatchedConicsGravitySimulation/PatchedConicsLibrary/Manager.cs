using DataStructuresLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatchedConicsLibrary
{
    public static class Manager
    {
        public static int ElapsedTicks { get; set; } = 0;

        public static Form CanvasForm { get; set; }

        public static AutoResetEvent CanvasRepainted { get; set; } = new AutoResetEvent(false);

        private static Stopwatch _timer = new Stopwatch();

        /// <summary>
        /// Number of real time seconds that pass every tick.
        /// </summary>
        public static double DeltaTime { get; private set; } = 100;

        /// <summary>
        /// The interval of real time seconds between every prediction point.
        /// </summary>
        public static double DeltaTimePrediction { get; private set; } = 10 * DeltaTime;

        /// <summary>
        /// Max number of predictions.
        /// </summary>
        public static int MaxPredictions { get; private set; } = 200;

        public static double TimeScale { get; set; } = 1;


        public static void Start()
        {
            BodyModel Kerbin = new BodyModel(new Vector2(), new Vector2(), 600000, 5.29 * Math.Pow(10, 22), Color.CornflowerBlue);
            BodyModel Ship = new BodyModel(Kerbin, Vector2.Decompose(700000, -15, false), Vector2.Decompose(3084, 75, false), 1, 1, Color.White, Color.Blue);
            BodyModel Mun = new BodyModel(Kerbin, Vector2.Decompose(12000000, Math.PI / 2), Vector2.Decompose(543, Math.PI), 200000, 9.76 * Math.Pow(10, 20), Color.Gray, Color.LightBlue);
            BodyModel Minmus = new BodyModel(Kerbin, Vector2.Decompose(47000000, Math.PI), Vector2.Decompose(274, 1.5 * Math.PI), 
                                            60000, 2.646 * Math.Pow(10, 19), Color.CadetBlue, Color.LightBlue);
            BodyModel Sattelite = new BodyModel(Kerbin, new Vector2(2000000, 0), new Vector2(0, -1750), 1, 1, Color.White, Color.Yellow);
            BodyModel Munsite = new BodyModel(Mun, Vector2.Decompose(500000, 0, false), Vector2.Decompose(362, 90, false), 1, 1, Color.White, Color.Green);

            Space.AddBody(Kerbin);
            Space.AddBody(Ship);
            Space.AddBody(Mun);
            Space.AddBody(Minmus);
            Space.AddBody(Sattelite);
            Space.AddBody(Munsite);

            Space.CentralBody = Kerbin;
            Space.UpdateBodyList();

            new Task(() => { Thread.CurrentThread.IsBackground = true; MainLoop(); }).Start();
        }

        public static void MainLoop()
        {
            Thread.Sleep(1000);
            while (true)
            {
                _timer.Restart();
                CanvasRepainted.WaitOne();
                Space.Update(DeltaTime);
                if ((ElapsedTicks+1) % (DeltaTimePrediction / DeltaTime) == 0)
                    Space.UpdatePredictions();

                try
                {
                    CanvasForm.Invoke((MethodInvoker)delegate { CanvasForm.Invalidate(); });
                }
                catch (Exception) {}
                

                Thread.Sleep(Math.Max(0, (int)((10 / TimeScale) - _timer.ElapsedTicks / (Stopwatch.Frequency / 1000))));
                ++ElapsedTicks;
            }
        }


    }
}
