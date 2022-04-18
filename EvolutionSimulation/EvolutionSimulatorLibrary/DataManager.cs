using EvolutionSimulatorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace EvolutionSimulatorLibrary
{
    public static class DataManager
    {
        private static PlaneModel Plane { get { return Manager.Plane; } }

        private static IReadOnlyCollection<CreatureModel> Creatures { get { return Plane.Creatures; } }

        public static Series MeanSize { get; set; }

        //public static Series MaxSize { get; set; }

        public static Series MeanSpeed { get; set; }

        //public static Series MaxSpeed { get; set; }

        public static Series MeanRange { get; set; }

        //public static Series MaxRange { get; set; }

        public static Series MeanExplorationRange { get; set; }

        //public static Series MaxExplorationRange { get; set; }

        public static Series MeanPredatoriness { get; set; }

        public static Series MaxPredatoriness { get; set; }

        public static Series MinPredatoriness { get; set; }

        static DataManager()
        {

        }

        public static void InitializeAllSeries()
        {
            MeanSize = new Series();
            //MaxSize = new Series();
            MeanSpeed = new Series();
            //MaxSpeed = new Series();
            MeanRange = new Series();
            //MaxRange = new Series();
            MeanExplorationRange = new Series();
            //MaxExplorationRange = new Series();
            MeanPredatoriness = new Series();
            MaxPredatoriness = new Series();
            MinPredatoriness = new Series();

            InitializeSeries(MeanSpeed, "Mean Speed", true);
            //InitializeSeries(MaxSpeed, "Max Speed", true);
            InitializeSeries(MeanSize, "Mean Size", true);
            //InitializeSeries(MaxSize, "Max Size", true);
            InitializeSeries(MeanRange, "Mean Range", true);
            //InitializeSeries(MaxRange, "Max Range", true);
            InitializeSeries(MeanExplorationRange, "Mean E. Range", true);
            //InitializeSeries(MaxExplorationRange, "Max E. Range", true);
            InitializeSeries(MeanPredatoriness, "Mean Predatoriness", true);
            InitializeSeries(MaxPredatoriness, "Max Predatoriness", true);
            InitializeSeries(MinPredatoriness, "Min Predatoriness", true);
        }

        public static void ResetAllSeries()
        {
            MeanSize.Points.Clear();
            //MaxSize.Points.Clear();
            MeanSpeed.Points.Clear();
            //MaxSpeed.Points.Clear();
            MeanRange.Points.Clear();
            //MaxRange.Points.Clear();
            MeanExplorationRange.Points.Clear();
            //MaxExplorationRange.Points.Clear();
            MeanPredatoriness.Points.Clear();
            MaxPredatoriness.Points.Clear();
            MinPredatoriness.Points.Clear();
        }

        private static void InitializeSeries(Series series, string name, bool isVisibleInLegend)
        {
            series.IsVisibleInLegend = isVisibleInLegend;
            series.ChartType = SeriesChartType.Line;
            series.BorderWidth = 3;
        }

        public static void UpdateSeries()
        {
            MeanSize.Points.AddXY(Manager.TicksElapsed, GetMeanValue(c => c.Size));
            //MaxSize.Points.AddXY(Manager.TicksElapsed, GetMaxValue(c => c.Size));

            MeanSpeed.Points.AddXY(Manager.TicksElapsed, GetMeanValue(c => c.Speed));
            //MaxSpeed.Points.AddXY(Manager.TicksElapsed, GetMaxValue(c => c.Speed)); 

            MeanRange.Points.AddXY(Manager.TicksElapsed, GetMeanValue(c => c.Range));
            //MaxRange.Points.AddXY(Manager.TicksElapsed, GetMaxValue(c => c.Range));

            MeanExplorationRange.Points.AddXY(Manager.TicksElapsed, GetMeanValue(c => c.ExplorationRange));
            //MaxExplorationRange.Points.AddXY(Manager.TicksElapsed, GetMaxValue(c => c.ExplorationRange));

            MeanPredatoriness.Points.AddXY(Manager.TicksElapsed, GetMeanValue(c => c.Predatoriness));
            MaxPredatoriness.Points.AddXY(Manager.TicksElapsed, GetMaxValue(c => c.Predatoriness));
            MinPredatoriness.Points.AddXY(Manager.TicksElapsed, GetMinValue(c => c.Predatoriness));
        }

        public static double GetMedianValue(Func<CreatureModel, double> getValue)
        {
            int count = Creatures.Count;
            if (count > 0)
            {
                List<CreatureModel> orderedCreatures = Creatures.OrderBy(getValue).ToList();
                return count % 2 == 0 ? (getValue(orderedCreatures.ElementAt(count / 2 - 1)) + getValue(orderedCreatures.ElementAt(count / 2))) / 2 :
                                        getValue(orderedCreatures.ElementAt((count - 1) / 2));
            }
            return 0;
        }

        public static double GetMeanValue(Func<CreatureModel, double> getValue)
        {
            if (Creatures.Count > 0)
            {
                double sum = 0;
                foreach (var creature in Creatures)
                {
                    sum += getValue(creature);
                }
                return sum / Creatures.Count;
            }
            return 0;
        } 

        public static double GetMaxValue(Func<CreatureModel, double> getValue)
        {
            if (Creatures.Count > 0)
            {
                double maxVal = double.MinValue;
                foreach (var creature in Creatures)
                {
                    maxVal = Math.Max(maxVal, getValue(creature));
                }
                return maxVal;
            }
            return 0;
        }

        public static double GetMinValue(Func<CreatureModel, double> getValue)
        {
            if (Creatures.Count > 0)
            {
                double minVal = double.MaxValue;
                foreach (var creature in Creatures)
                {
                    minVal = Math.Min(minVal, getValue(creature));
                }
                return minVal;
            }
            return 0;
        }

    }
}
