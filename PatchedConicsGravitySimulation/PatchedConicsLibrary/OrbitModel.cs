using DataStructuresLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchedConicsLibrary
{
    public class OrbitModel
    {
        // TODO - Add all orbital elements, reorganize and generalize for 3D

        /// <summary>
        /// Body at one of the foci.
        /// </summary>
        public BodyModel ParentBody { get; set; }

        /// <summary>
        /// Current radius.
        /// </summary>
        public Vector2 Radius { get; set; }

        /// <summary>
        /// Velocity of the body in orbit.
        /// </summary>
        public Vector2 Velocity { get; set; }

        /// <summary>
        /// Determines whether the orbit is in retrograde (180 degrees of inclination).
        /// </summary>
        public bool Retrograde { get; set; }

        /// <summary>
        /// Predicitons of body's position in the future.
        /// </summary>
        public Queue<Vector2> Predictions { get; set; }

        /// <summary>
        /// Color of the orbit.
        /// </summary>
        public Color OrbitColor { get; set; }

        #region ORBITAL ELEMENTS
        /// <summary>
        /// Semi-major axis.
        /// </summary>
        public double A { get; set; } 

        /// <summary>
        /// Eccentricity of the orbit.
        /// </summary>
        public double E { get; set; }

        /// <summary>
        /// Current true anomaly.
        /// </summary>
        public double TrueAnomaly { get; set; }

        // Inclination

        // Longitude of AN

        // Arugment of periapsis

        #endregion

        #region AUXILIARY PROPERTIES

        /// <summary>
        /// Period of the orbit.
        /// </summary>
        public double Period { get; set; }

        /// <summary>
        /// Periapsis of the orbit.
        /// </summary>
        public double Periapsis { get; set; }

        /// <summary>
        /// Apoapsis of the orbit.
        /// </summary>
        public double Apoapsis { get; set; }

        /// <summary>
        /// Current flight-path angle.
        /// </summary>
        public double FlightPathAngle { get; set; }

        /// <summary>
        /// Eccentricity vector pointing towards periapsis and with magnitude of eccentricty.
        /// </summary>
        public Vector2 EccentricityVector { get; set; }

        /// <summary>
        /// Current mean anomaly.
        /// </summary>
        public double MeanAnomaly { get; set; }

        /// <summary>
        /// Current eccentric anomaly.
        /// </summary>
        public double EccentricAnomaly { get; set; }

        /// <summary>
        /// Mean motion of the orbit.
        /// </summary>
        public double MeanMotion { get; set; }

        /// <summary>
        /// Specific oribtal energy of the orbit.
        /// </summary>
        public double SpecificME { get; set; }

        /// <summary>
        /// Specific angular momentum of the orbit.
        /// </summary>
        public double SpecificAM { get; set; }

        /// <summary>
        /// True anomaly at epoch.
        /// </summary>
        public double TrueAnomalyAtEpoch { get; set; }

        #endregion

        public OrbitModel(BodyModel parentBody, Vector2 radius, Vector2 velocity, Color orbitColor)
        {
            ParentBody = parentBody;
            Radius = radius;
            Velocity = velocity;
            Retrograde = Vector2.CrossProduct(Radius, Velocity) < 0;

            // Determine flight-path angle
            FlightPathAngle = (Math.PI / 2) - Vector2.GetAngle(Velocity, Radius);

            // Determine specific orbital energy
            SpecificME = (Math.Pow(Velocity.Lenght, 2) / 2) - (ParentBody.GParameter / Radius.Lenght);

            // Determine specific angular momentum
            SpecificAM = Radius.Lenght * Velocity.Lenght * Math.Cos(FlightPathAngle);

            // Determine semi-major axis
            A = -ParentBody.GParameter / (2 * SpecificME);

            // Determine eccentricity
            E = Math.Sqrt(1 + (2 * SpecificME * Math.Pow(SpecificAM, 2)) / Math.Pow(ParentBody.GParameter,2));

            // Determine true anomaly at epoch
            if (E != 0)
            {
                double trueAnomalyCosine = Math.Round((A * (1 - Math.Pow(E, 2)) - Radius.Lenght) / (E * Radius.Lenght), 10);
                TrueAnomalyAtEpoch = Math.Acos(trueAnomalyCosine);
                if (Vector2.DotProduct(Radius, Velocity) < 0)
                    TrueAnomalyAtEpoch = 2 * Math.PI - TrueAnomalyAtEpoch;
            }
            if (Retrograde)
            {
                TrueAnomalyAtEpoch = 2 * Math.PI - TrueAnomalyAtEpoch;
            }
            TrueAnomaly = TrueAnomalyAtEpoch;

            // Determine eccentric vector
            EccentricityVector = Vector2.Decompose(E, Radius.Angle - TrueAnomalyAtEpoch);

            // Determine eccentric anomaly
            if (E < 1)
            {
                double eccentricAnomalyCosine = Math.Round((A * E + Radius.Lenght * Math.Cos(TrueAnomaly)) / A, 10);
                EccentricAnomaly = Math.Acos(eccentricAnomalyCosine);
                if (FlightPathAngle < 0)
                {
                    EccentricAnomaly = 2 * Math.PI - EccentricAnomaly;
                }
            }
            else
            {
                double eccentricAnomalyCosh = Math.Round((Math.Cos(TrueAnomaly) + E) / (1 + E * Math.Cos(TrueAnomaly)), 10);
                EccentricAnomaly = Math.Log(eccentricAnomalyCosh + Math.Sqrt(Math.Pow(eccentricAnomalyCosh, 2) - 1));
                if (FlightPathAngle < 0)
                {
                    EccentricAnomaly = -EccentricAnomaly;
                }
            }

            // Determine mean anomaly
            if (E < 1)
            {
                MeanAnomaly = EccentricAnomaly - E * Math.Sin(EccentricAnomaly);
            }
            else
            {
                MeanAnomaly = E * Math.Sinh(EccentricAnomaly) - EccentricAnomaly; 
            }

            // Determine mean motion
            MeanMotion = Math.Sqrt(ParentBody.GParameter / Math.Pow(Math.Abs(A), 3)) ;

            // Determine orbital period
            if (E < 1)
            {
                Period = 2 * Math.PI * Math.Sqrt(Math.Pow(A, 3) / ParentBody.GParameter);
            }
            else
            {
                Period = 2 * (1.0 / MeanMotion) * -(E * Math.Sinh(EccentricAnomaly) - EccentricAnomaly);
            }

            // Determine apoapsis and periapsis
            Apoapsis = A * (1 + E);
            Periapsis = A * (1 - E);

            // Set orbit color
            OrbitColor = orbitColor;

            // Create the first set of predictions
            CreatePredictions();
        }

        private double GetMeanAnomaly(double deltaTime)
        {
            double meanAnomaly = (MeanAnomaly + MeanMotion * deltaTime);
            if (E < 1)
                meanAnomaly %= (2 * Math.PI);
            return meanAnomaly;
        }

        private double GetEccentricAnomaly(double meanAnomaly, int maxIterations = 20, double maxError = 0.001)
        {
            // Applying Newton-Raphson method x1 = x0 - f(x0)/f'(x0)
            double x = Math.PI * Math.Sign(meanAnomaly);
            double dx;
            Func<double, double> function;
            Func<double, double> derivate;
            if (E < 1)
            {
                function = (input) => (input - E * Math.Sin(input) - meanAnomaly);
                derivate = (input) => (1 - E * Math.Cos(input));
            }
            else
            {
                function = (input) => (E * Math.Sinh(input) - input - meanAnomaly);
                derivate = (input) => (E * Math.Cosh(input) - 1);
            }
            for (int i = 0; i < maxIterations; i++)
            {
                dx = function(x) / derivate(x);
                x = x - dx;
                if (Math.Abs(dx) < maxError)
                    break;
            }

            return x;
        }

        private double GetTrueAnomaly(double eccentricAnomaly, double meanAnomaly)
        {
            double trueAnomaly;
            if (E < 1)
            {
                double trueAnomalyCosine = (Math.Cos(eccentricAnomaly) - E) / (1 - E * Math.Cos(eccentricAnomaly));
                trueAnomaly = Math.Acos(trueAnomalyCosine);
            }
            else
            {
                double trueAnomalyTangent = Math.Tanh(eccentricAnomaly / 2) / (Math.Sqrt((E - 1) / (E + 1)));
                trueAnomaly = 2 * Math.Atan(trueAnomalyTangent);
            }
            if (E < 1 && Math.Abs(meanAnomaly) > Math.PI)
            {
                trueAnomaly = 2 * Math.PI - Math.Abs(trueAnomaly);
            }
            return trueAnomaly;
        }

        private Vector2 GetRadius(double trueAnomaly)
        {
            Vector2 radius;

            double radiusAngle = (EccentricityVector.Angle + trueAnomaly) % (2 * Math.PI);
            double radiusMagnitude = (A * (1 - Math.Pow(E, 2))) / (1 + E * Math.Cos(trueAnomaly));
            radius = Vector2.Decompose(radiusMagnitude, radiusAngle);

            if (Retrograde)
            {
                radius = radius.Rotate(-EccentricityVector.Angle);
                radius.Y = -radius.Y;
                radius = radius.Rotate(EccentricityVector.Angle);
            }
            return radius;
        }

        private Vector2 GetVelocity(double trueAnomaly, Vector2 radius)
        {
            // TODO - Figure out better way of determining velocity cause current one is shit.
            Vector2 velocity;

            double a = 2 * A - radius.Lenght;
            double b = radius.Lenght;
            double c = 2 * E * A;
            // Angle from focus 1 to body to focus 2.
            double alpha = Math.Acos((c * c - a * a - b * b) / (-2 * a * b));

            double velocityAngle = (trueAnomaly + (Math.PI - alpha) / 2 + EccentricityVector.Angle);
            if (trueAnomaly > Math.PI || trueAnomaly < 0)
                velocityAngle += alpha;
            double velocityMagnitude = Math.Sqrt(ParentBody.GParameter * (2.0 / radius.Lenght - 1.0 / A));
            velocity = Vector2.Decompose(velocityMagnitude, velocityAngle);

            if (Retrograde)
            {
                velocity = velocity.Rotate(-EccentricityVector.Angle);
                velocity.Y = -velocity.Y;
                velocity = velocity.Rotate(EccentricityVector.Angle);
            }

            return velocity;
        }

        public void PredictPosition(double deltaTime, out double meanAnomaly, out double eccentricAnomaly, out double trueAnomaly,
                            out Vector2 radius, out Vector2 velocity, out double flightPathAngle)
        {
            // Calculate mean anomaly
            meanAnomaly = GetMeanAnomaly(deltaTime);
            // Calculate eccentric anomaly
            eccentricAnomaly = GetEccentricAnomaly(meanAnomaly);
            // Calculate true anomaly
            trueAnomaly = GetTrueAnomaly(eccentricAnomaly, meanAnomaly);
            // Calculate radius
            radius = GetRadius(trueAnomaly);
            // Calculate velocity
            velocity = GetVelocity(trueAnomaly, radius);
            // Calculate flight path angle
            flightPathAngle = (Math.PI / 2) - Vector2.GetAngle(velocity, radius);
        }

        public void Update(double deltaTime)
        {
            PredictPosition(deltaTime, out double meanAnomaly, out double eccentricAnomaly, out double trueAnomaly,
                   out Vector2 radius, out Vector2 velocity, out double flightPathAngle);
            MeanAnomaly = meanAnomaly;
            EccentricAnomaly = eccentricAnomaly;
            TrueAnomaly = trueAnomaly;
            Radius = radius;
            Velocity = velocity;
            FlightPathAngle = flightPathAngle;
        }

        private void CreatePredictions()
        {
            Predictions = new Queue<Vector2>();
            int numberOfPredictions = Math.Min((int)Math.Floor(Period / Manager.DeltaTimePrediction), Manager.MaxPredictions);
            numberOfPredictions = Math.Max(numberOfPredictions, 2);
            for (int i = 0; i < numberOfPredictions; i++)
            {
                PredictPosition(Manager.DeltaTimePrediction * i, out _, out _, out _, out Vector2 prediction, out _, out _);
                Predictions.Enqueue(prediction);
            }
        }

        public void UpdatePredictions()
        {
            Predictions.Dequeue();
            PredictPosition(Manager.DeltaTimePrediction * Predictions.Count(), out _, out _, out _, out Vector2 prediction, out _, out _);
            Predictions.Enqueue(prediction);
        }

    }
}
