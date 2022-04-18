using DataStructuresLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatchedConicsLibrary
{
    public static class CanvasManager
    {

        public static Point CenterOffset = new Point(400, 400);

        public static BodyModel FocusedBody { get; set; } = null;

        private static double _scale = 40000;

        public static double Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        public static bool DrawRelative = true;


        #region BRUSHES & PENS

        private static SolidBrush _bodyBrush = new SolidBrush(Color.White);

        private static Pen _orbitPen = new Pen(Color.LightBlue, 2);

        private static Pen _soiPen = new Pen(Color.Red, 1);

        private static Pen _vectorPen = new Pen(Color.Purple, 2);

        #endregion


        public static Form CanvasForm { get; set; }

        public static void DrawAll(Graphics G)
        {
            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (FocusedBody != null)
            {
                CenterOffset.X = (int)(400 - FocusedBody.PositionGlobal.X / Scale);
                CenterOffset.Y = (int)(400 + FocusedBody.PositionGlobal.Y / Scale);
            }
            DrawPredictions(G);
            DrawSOI(G);
            DrawBodies(G);
            DrawVelocityVectors(G);
            DrawPeriapsis(G);
        }


        private static void DrawBodies(Graphics G)
        {
            foreach (var body in Space.Bodies)
            {
                _bodyBrush.Color = body.BodyColor;
                float radius = Math.Max(2, (float)(body.Radius / Scale));
                G.FillEllipse(_bodyBrush, 
                             (float)(body.PositionGlobal.X / Scale - radius + CenterOffset.X), 
                             (float)(-body.PositionGlobal.Y / Scale - radius + CenterOffset.Y), 
                             2 * radius,
                             2 * radius);
            }

        }

        private static void DrawPredictions(Graphics G)
        {
            foreach (var body in Space.Bodies)
            {
                if (body.Orbit != null)
                {
                    _orbitPen.Color = body.Orbit.OrbitColor;

                    BodyModel parentBody = body.Orbit.ParentBody;
                    Queue<Vector2> predictions = body.Orbit.Predictions;
                    Point A;
                    Point B;

                    if (body.Orbit.ParentBody.Orbit == null || DrawRelative)
                    {
                        // A = new Point((int)(body.PositionGlobal.X / Scale + CenterOffset.X),
                        //                 (int)(-body.PositionGlobal.Y / Scale + CenterOffset.Y));
                        // B = new Point((int)((predictions.ElementAt(0).X + parentBody.PositionGlobal.X) / Scale + CenterOffset.X),
                        //                     (int)(-(predictions.ElementAt(0).Y + parentBody.PositionGlobal.Y) / Scale + CenterOffset.Y));
                        // G.DrawLine(_orbitPen, A, B);
                        int iMax = predictions.Count();
                        for (int i = 0; i < iMax - 1; i++)
                        {
                            A = new Point((int)((predictions.ElementAt(i).X + parentBody.PositionGlobal.X) / Scale + CenterOffset.X),
                                                (int)(-(predictions.ElementAt(i).Y + parentBody.PositionGlobal.Y) / Scale + CenterOffset.Y));
                            B = new Point((int)((predictions.ElementAt(i + 1).X + parentBody.PositionGlobal.X) / Scale + CenterOffset.X),
                                                (int)(-(predictions.ElementAt(i + 1).Y + parentBody.PositionGlobal.Y) / Scale + CenterOffset.Y));
                            G.DrawLine(_orbitPen, A, B);
                        }
                    }
                    else
                    {
                        Queue<Vector2> parentPredictions = body.Orbit.ParentBody.Orbit.Predictions;

                        // A = new Point((int)(body.PositionGlobal.X / Scale + CenterOffset.X),
                        //                 (int)(-body.PositionGlobal.Y / Scale + CenterOffset.Y));
                        // B = new Point((int)((predictions.ElementAt(0).X + parentPredictions.ElementAt(0).X) / Scale + CenterOffset.X),
                        //                         (int)(-(predictions.ElementAt(0).Y + parentPredictions.ElementAt(0).Y) / Scale + CenterOffset.Y));
                        // G.DrawLine(_orbitPen, A, B);
                        int iMax = Math.Min(predictions.Count(), parentPredictions.Count());
                        for (int i = 0; i < iMax - 1; i++)
                        {
                            A = new Point((int)((predictions.ElementAt(i).X + parentPredictions.ElementAt(i).X) / Scale + CenterOffset.X),
                                                (int)(-(predictions.ElementAt(i).Y + parentPredictions.ElementAt(i).Y) / Scale + CenterOffset.Y));
                            B = new Point((int)((predictions.ElementAt(i + 1).X + parentPredictions.ElementAt(i + 1).X) / Scale + CenterOffset.X),
                                                (int)(-(predictions.ElementAt(i + 1).Y + parentPredictions.ElementAt(i + 1).Y) / Scale + CenterOffset.Y));
                            G.DrawLine(_orbitPen, A, B);
                        }
                    }

                    // int iMax = predictions.Count();
                    // Point[] points = new Point[predictions.Count()];
                    // for (int i = 0; i < iMax; i++)
                    // {
                    //     points[i] = new Point((int)((predictions.ElementAt(i).X + parentBody.PositionGlobal.X) / Scale + _centerOffset.X),
                    //                          (int)(-(predictions.ElementAt(i).Y + parentBody.PositionGlobal.Y) / Scale + _centerOffset.Y));
                    // }
                    // G.DrawCurve(_orbitPen, points);
                }
            }
        }

        private static void DrawSOI(Graphics G)
        {
            foreach (var body in Space.Bodies)
            {
                if (body.SOIRadius > Scale && body.SOIRadius != double.MaxValue)
                {
                    float radius = (float)(body.SOIRadius / Scale);
                    G.DrawEllipse(_soiPen,
                                 (float)(body.PositionGlobal.X / Scale - radius + CenterOffset.X),
                                 (float)(-body.PositionGlobal.Y / Scale - radius + CenterOffset.Y),
                                 2 * radius,
                                 2 * radius);
                }
            }
        }

        private static void DrawVelocityVectors(Graphics G)
        {
            foreach (var body in Space.Bodies)
            {
                if (body.Orbit != null)
                {
                    //Vector2 velocityVector = body.VelocityLocal.Normalise() * body.VelocityLocal.Lenght * 5000 / Scale;
                    Vector2 velocityVector = body.VelocityGlobal.Normalise() * body.VelocityGlobal.Lenght * 5000 / Scale;
                    Point A = new Point((int)(body.PositionGlobal.X / Scale + CenterOffset.X), (int)(-body.PositionGlobal.Y / Scale + CenterOffset.Y));
                    Point B = new Point((int)(A.X + velocityVector.X), (int)(A.Y - velocityVector.Y));
                    G.DrawLine(_vectorPen, A, B);
                }
            }
        }

        private static void DrawPeriapsis(Graphics G)
        {
            foreach (var body in Space.Bodies)
            {
                if (body.Orbit != null)
                {
                    float radius = 5;
                    Vector2 periapsis = body.Orbit.EccentricityVector.Normalise() * body.Orbit.Periapsis;
                    G.DrawEllipse(_vectorPen,
                                 (float)((body.Orbit.ParentBody.PositionGlobal.X + periapsis.X) / Scale - radius + CenterOffset.X),
                                 (float)(-(body.Orbit.ParentBody.PositionGlobal.Y + periapsis.Y) / Scale - radius + CenterOffset.Y),
                                 2 * radius,
                                 2 * radius);
                }
            }
        }

    }
}
