using System;

namespace EvolutionSimulatorLibrary
{
    /// <summary>
    /// Represents a two-dimensional vector that supports operations on a pair of numbers
    /// and two pairs of numbers.
    /// </summary>
    public class Vector2
    {
        private double _x;
        /// <summary>
        /// X component of the vector
        /// </summary>
        public double X
        {
            get { return _x; }
            set
            {
                _x = value;
                Lenght = Math.Sqrt(X * X + Y * Y);
            }
        }

        private double _y;
        /// <summary>
        /// Y component of the vector
        /// </summary>
        public double Y
        {
            get { return _y; }
            set
            {
                _y = value;
                Lenght = Math.Sqrt(X * X + Y * Y);
            }
        }

        /// <summary>
        /// The lenght of the vector.
        /// </summary>
        public double Lenght { get; private set; }

        /// <summary>
        /// Initializes an instance of a two-dimensional vector.
        /// </summary>
        public Vector2()
        {
            X = 0;
            Y = 0;
        }

        /// <summary>
        /// Initializes an instance of a two-dimensional vector with a given pair of numbers.
        /// </summary>
        /// <param name="_x">X component of the vector.</param>
        /// <param name="_y">Y component of the vector.</param>
        public Vector2(double _x, double _y)
        {
            X = _x;
            Y = _y;
        }

        /// <summary>
        /// Limits the values of the vector to set boundries.
        /// </summary>
        /// <param name="minx">Minimal value of the X component.</param>
        /// <param name="maxx">Maximum value of the X component.</param>
        /// <param name="miny">Minimal value of the Y component.</param>
        /// <param name="maxy">Maximum value of the Y component.</param>
        public void Limit(double minX, double maxX, double minY, double maxY)
        {
            X = Math.Max(minX, X);
            X = Math.Min(maxX, X);
            Y = Math.Max(minY, Y);
            Y = Math.Min(maxY, Y);
        }

        /// <summary>
        /// Wraps values of the vector between the set boundries.
        /// </summary>
        /// <param name="minx">Minimal value of the X component.</param>
        /// <param name="maxx">Maximum value of the X component.</param>
        /// <param name="miny">Minimal value of the Y component.</param>
        /// <param name="maxy">Maximum value of the Y component.</param>
        public void Wrap(double minX, double maxX, double minY, double maxY)
        {
            double r;
            r = maxX - minX;
            X = (X - maxX) - r * Math.Floor((X - maxX) / r) + minX;
            r = maxY - minY;
            Y = (Y - maxY) - r * Math.Floor((Y - maxY) / r) + minY;
        }

        /// <summary>
        /// Maps values of the vector between given range.
        /// </summary>
        /// <param name="minA">Current minimum value.</param>
        /// <param name="maxA">Current maximum value.</param>
        /// <param name="minB">Desired minimum value.</param>
        /// <param name="maxB">Desired maximum value.</param>
        public void Map(double minA, double maxA, double minB, double maxB)
        {
            X = ((X - minA) / (maxA - minA)) * (maxB - minB) + minB;
            Y = ((Y - minA) / (maxA - minA)) * (maxB - minB) + minB;
        }

        /// <summary>
        /// Round the values of the vector to the nearest integer.
        /// </summary>
        public void Round()
        {
            X = Math.Round(X);
            Y = Math.Round(Y);
        }

        /// <summary>
        /// Calculates squared distance between two points on a plane.
        /// </summary>
        /// <param name="v1">Coordinates of the first point.</param>
        /// <param name="v2">Coordinates of the second point.</param>
        /// <returns>Squared distance</returns>
        public static double DistanceSq(Vector2 v1, Vector2 v2)
        {
            return (Math.Pow(v1.X - v2.X, 2) + Math.Pow(v1.Y - v2.Y, 2));
        }

        /// <summary>
        /// Calculates distance between two points on a plane.
        /// </summary>
        /// <param name="v1">Coordinates of the first point.</param>
        /// <param name="v2">Coordinates of the second point.</param>
        /// <returns>Distance</returns>
        public static double Distance(Vector2 v1, Vector2 v2)
        {
            return Math.Sqrt(DistanceSq(v1, v2));
        }

        /// <summary>
        /// Calculates the dot product of two specified vectors.
        /// </summary>
        /// <param name="v1">First vector.</param>
        /// <param name="v2">Second vector.</param>
        /// <returns>The dot product of two vectors.</returns>
        public static double DotProduct(Vector2 v1, Vector2 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y;
        }

        /// <summary>
        /// Adds two vectors by their components.
        /// </summary>
        /// <param name="v1">First vector.</param>
        /// <param name="v2">Second vector.</param>
        /// <returns>Sum of two vectors.</returns>
        public static Vector2 Add(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }

        /// <summary>
        /// Subtracts a vector from another one.
        /// </summary>
        /// <param name="v1">Minuend vector.</param>
        /// <param name="v2">Subtrahent vector.</param>
        /// <returns>Difference between the two vectors</returns>
        public static Vector2 Subtract(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        } 

        /// <summary>
        /// Calculates the distance between two points on a map as a vector.
        /// </summary>
        /// <param name="v1">Tail of new vector.</param>
        /// <param name="v2">Tip of new vector.</param>
        /// <returns>Distance as a vector.</returns>
        public static Vector2 FindVector(Vector2 v1, Vector2 v2)
        {
            double distance = Math.Sqrt(DistanceSq(v1, v2));
            return Decompose(distance, Angle(v1, v2));
        }

        /// <summary>
        /// Decomposes a vector into its components.
        /// </summary>
        /// <param name="value">Vector lenght.</param>
        /// <param name="angle">Vector angle.</param>
        /// <returns>Components of a vector.</returns>
        public static Vector2 Decompose(double value, double angle)
        {
            return new Vector2(value * Math.Cos(angle), value * Math.Sin(angle));
        }

        /// <summary>
        /// Calculates the angle between a segment connecting two points and the X axis going through the origin point.
        /// </summary>
        /// <param name="origin">The origin point.</param>
        /// <param name="target">Second point.</param>
        /// <returns>Angle between a segment and X axis.</returns>
        public static double Angle(Vector2 origin, Vector2 target)
        {
            double distance = Math.Sqrt(DistanceSq(origin, target));
            if (distance != 0)
            {
                double dx = target.X - origin.X;
                double dy = target.Y - origin.Y;
                double angle;
                angle = Math.Acos(dx / distance);
                if (dy < 0)
                {
                    angle = 2 * Math.PI - angle;
                }
                return angle;
            }
            else return 0;
        }

        /// <summary>
        /// Converts a vector to a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"[{X},{Y}]";
        }

        #region OPERATOR OVERLOADS

        /// <summary>
        /// Adds two vectors by their components.
        /// </summary>
        /// <param name="v1">First vector.</param>
        /// <param name="v2">Second vector.</param>
        /// <returns>Sum of two vectors.</returns>
        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return Add(v1, v2);
        }

        /// <summary>
        /// Subtracts a vector from another one.
        /// </summary>
        /// <param name="v1">Minued vector.</param>
        /// <param name="v2">Subtrahend vector.</param>
        /// <returns>Difference between the two vectors.</returns>
        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return Subtract(v1, v2);
        }

        public static Vector2 operator*(Vector2 v1, double val)
        {
            return new Vector2(v1.X * val, v1.Y * val);
        }

        public static Vector2 operator*(double val, Vector2 v1)
        {
            return new Vector2(v1.X * val, v1.Y * val);
        }

        public static Vector2 operator/(Vector2 v1, double val)
        {
            return new Vector2(v1.X / val, v1.Y / val);
        }

        public static Vector2 operator /(double val, Vector2 v1)
        {
            return new Vector2(v1.X / val, v1.Y / val);
        }

        /// <summary>
        /// Compares the lenght of the two vectors.
        /// </summary>
        /// <param name="v1">First vector.</param>
        /// <param name="v2">Second vector.</param>
        /// <returns>True if the second vector is shorter; otherwise false.</returns>
        public static bool operator >(Vector2 v1, Vector2 v2)
        {
            return (v1.Lenght > v2.Lenght);
        }

        /// <summary>
        /// Compares the lenght of the two vectors.
        /// </summary>
        /// <param name="v1">First vector.</param>
        /// <param name="v2">Second vector.</param>
        /// <returns>False if the second vector is shorter; otherwise false.</returns>
        public static bool operator <(Vector2 v1, Vector2 v2)
        {
            return (v1.Lenght < v2.Lenght);
        }

        /// <summary>
        /// Compares the lenght of the two vectors.
        /// </summary>
        /// <param name="v1">First vector.</param>
        /// <param name="v2">Second vector.</param>
        /// <returns>False if the second vector is longer; otherwise false.</returns>
        public static bool operator >=(Vector2 v1, Vector2 v2)
        {
            return (v1.Lenght >= v2.Lenght);
        }

        /// <summary>
        /// Compares the lenght of the two vectors.
        /// </summary>
        /// <param name="v1">First vector.</param>
        /// <param name="v2">Second vector.</param>
        /// <returns>True if the second vector is longer; otherwise false.</returns>
        public static bool operator <=(Vector2 v1, Vector2 v2)
        {
            return (v1.Lenght <= v2.Lenght);
        }

        // /// <summary>
        // /// Compares the lenght of the two vectors.
        // /// </summary>
        // /// <param name="v1">First vector.</param>
        // /// <param name="v2">Second vector.</param>
        // /// <returns>True if the lenght is equal; otherwise false.</returns>
        // public static bool operator ==(Vector2 v1, Vector2 v2)
        // {
        //     return (v1.Lenght == v2.Lenght);
        // }
        // 
        // /// <summary>
        // /// Compares the lenght of the two vectors.
        // /// </summary>
        // /// <param name="v1">First vector.</param>
        // /// <param name="v2">Second vector.</param>
        // /// <returns>False if the lenght is equal; otherwise true.</returns>
        // public static bool operator !=(Vector2 v1, Vector2 v2)
        // {
        //     return (v1.Lenght != v2.Lenght);
        // }
        #endregion

    }
}

