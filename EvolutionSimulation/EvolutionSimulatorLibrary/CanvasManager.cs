using EvolutionSimulatorLibrary.Interfaces;
using EvolutionSimulatorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EvolutionSimulatorLibrary
{
    public static class CanvasManager
    {
        #region PROPERTIES
        public static bool SectorsVisible { get; set; }

        public static bool RangeVisible { get; set; }

        public static bool IgnoreSize { get; set; }

        public static bool ColorCodeSpeed { get; set; }

        public static bool ColorCodePredatoriness { get; set; } = true;

        public static Form PlaneForm { get { return Manager.PlaneForm; } }

        private static PlaneModel Plane { get { return Manager.Plane; } }

        #region BRUSHES & PENS
        private static SolidBrush _creatureBursh = new SolidBrush(Color.Black);

        private static SolidBrush _foodBrush = new SolidBrush(Color.Green);

        private static Pen _sectorPen = new Pen(Color.DarkGray, 2);

        private static Pen _rangePen = new Pen(Color.Gray, 2);

        private static Pen _lineToTargetPen = new Pen(Color.Red, 4);

        #endregion

        #endregion


        // ---
        // ---

        public static void DrawAll(Graphics G)
        {
            if (SectorsVisible)
                DrawSectors(G);
            DrawFood(G);
            DrawCreatures(G);
        }

        private static void DrawSectors(Graphics G)
        {
            for (int i = 1; i <= Plane.SizeInSectors.Width; i++)
            {
                G.DrawLine(_sectorPen, i * Plane.SectorSize, 0, i * Plane.SectorSize, Plane.Height);
            }
            for (int i = 1; i <+ Plane.SizeInSectors.Height; i++)
            {
                G.DrawLine(_sectorPen, 0, i * Plane.SectorSize, Plane.Width, i * Plane.SectorSize);
            }
        }

        public static void DrawCreatures(Graphics G)
        {
            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            foreach (var creature in Plane.Creatures)
            {
                int size = IgnoreSize ? 6 : creature.Size;
                _creatureBursh.Color = DetermineCreatureColor(creature);
                G.FillEllipse(_creatureBursh, 
                             (float)(creature.Position.X - size), 
                             (float)(creature.Position.Y - size), 
                             size * 2,
                             size * 2);
                // Draw Range Radius
                if (RangeVisible || creature == Manager.ActiveCreature)
                {
                    G.DrawEllipse(_rangePen,
                                 (float)(creature.Position.X - creature.Range),
                                 (float)(creature.Position.Y - creature.Range),
                                 (float)creature.Range * 2,
                                 (float)creature.Range * 2);
                }
                // Draw lines to creature targets
                if (creature.Target is CreatureModel || (creature == Manager.ActiveCreature && creature.Target != null))
                {
                    G.DrawLine(_lineToTargetPen,
                              (float)(creature.Position.X),
                              (float)(creature.Position.Y),
                              (float)(creature.Target.Position.X),
                              (float)(creature.Target.Position.Y));
                }
            }
            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
        }

        public static void DrawFood(Graphics G)
        {
            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            foreach (var food in Plane.Food)
            {
                G.FillEllipse(_foodBrush,
                             (float)(food.Position.X - food.Size),
                             (float)(food.Position.Y - food.Size),
                             food.Size * 2,
                             food.Size * 2);
            }
            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
        }

        public static Color DetermineCreatureColor(CreatureModel creature)
        {
            int colorVal = 0;
            if (creature == Manager.ActiveCreature)
            {
                return Color.Gold;
            }
            if (ColorCodeSpeed)
            {
                colorVal = (int)Map(creature.Speed, CreatureModel.MinSpeed, CreatureModel.MaxSpeed, 0, 255);
                return Color.FromArgb(colorVal, 0, 255 - colorVal);
            }
            if (ColorCodePredatoriness)
            {
                colorVal = (int)Map(creature.Predatoriness, CreatureModel.MinPredatoriness, CreatureModel.MaxPredatoriness, 0, 255);
                //colorVal = (int)(255 * Sigmoid(creature.Predatoriness, 0, 2.0, 5.0));
                return Color.FromArgb(colorVal, 255 - colorVal, 0);
            }
            return Color.FromArgb(0, 0, 0);
        }

        private static double Normalise(double val, double min, double max)
        {
            if (max - min == 0)
                return 0.5;
            return (val - min) / (max - min);
        }

        private static double Map(double val, double minA, double maxA, double minB, double maxB)
        {
            val = Normalise(val, minA, maxA);
            return val * (maxB - minB) + minB;
        }

        private static double Sigmoid(double val, double min, double max, double scale)
        {
            return 1 / (1 + Math.Pow(Math.E, scale * (-val + (max - min) / 2)));
        }

    }

}
