using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeCreatorLIB
{
    /// <summary>
    /// Represents the global data container.
    /// </summary>
    public static class Manager
    {
        //
        // PROPERTIES
        //

        /// <summary>
        /// Current maze.
        /// </summary>
        public static Maze Maze { get; set; }

        /// <summary>
        /// Number of rows in the maze specified by the user.
        /// </summary>
        public static int Rows;

        /// <summary>
        /// Number of cols in the maze specified by the user.
        /// </summary>
        public static int Cols;

        /// <summary>
        /// The RNG seed specified by the user.
        /// </summary>
        public static int Seed;

        /// <summary>
        /// The maze form.
        /// </summary>
        public static Form MazeForm { get; set; }

        /// <summary>
        /// The create maze button on the launcher form.
        /// </summary>
        public static Button CreateMazeButton { get; set; }
        
        /// <summary>
        /// The find path button on the launcher form.
        /// </summary>
        public static Button FindPathButton { get; set; }

        /// <summary>
        /// The starting point of the maze.
        /// </summary>
        public static Node StartNode { get; set; } = null;

        /// <summary>
        /// The ending point of the maze.
        /// </summary>
        public static Node EndNode { get; set; } = null;

        //
        // METHODS
        //

        /// <summary>
        /// Sets the start node of the maze.
        /// </summary>
        /// <param name="x">The X position of the mouse.</param>
        /// <param name="y">The Y position of the mouse.</param>
        /// <param name="scale">Scale of the form.</param>
        public static void SetStartNode(int x, int y, int scale)
        {
            if (Maze[y / scale, x / scale].IsVisited)
            {
                StartNode = Maze[y / scale, x / scale] == StartNode ? null : Maze[y / scale, x / scale];
            }
                
        }

        /// <summary>
        /// Sets the end node of the maze.
        /// </summary>
        /// <param name="x">The X position of the mouse.</param>
        /// <param name="y">The Y position of the mouse.</param>
        /// <param name="scale">Scale of the form.</param>
        public static void SetEndNode(int x, int y, int scale)
        {
            if (Maze[y / scale, x / scale].IsVisited)
            {
                EndNode = Maze[y / scale, x / scale] == EndNode ? null : Maze[y / scale, x / scale];
            }
        }

        /// <summary>
        /// Validates the input.
        /// </summary>
        /// <param name="rows">Number of rows.</param>
        /// <param name="cols">Number of columns.</param>
        /// <param name="seed">RNG seed.</param>
        /// <returns>Whether the input is valid.</returns>
        public static bool ValidateInput(string rows, string cols, string seed, out string validationMessage)
        {
            bool isValid = true;
            validationMessage = "";
            // Validate seed
            if (!int.TryParse(seed, out Seed))
            {
                if (seed != "Seed (random)")
                {
                    isValid = false;
                    validationMessage = "The seed must be in <-2^31; 2^31-1> range.";
                }
                else
                {
                    Seed = new Random().Next();
                }
            }
            else
            {
                if (Seed < 0)
                {
                    isValid = false;
                    validationMessage = "The seed must be in <-2^31; 2^31-1> range.";
                }
            }
            // Validate columns
            if (!int.TryParse(cols, out Cols))
            {
                isValid = false;
                validationMessage = "Number of columns must be a number.";
            }
            else
            {
                if (Cols < 11 || Cols > 265 || Cols % 2 == 0)
                {
                    isValid = false;
                    validationMessage = "Number of columns must be odd and in <11; 265> range";
                }
            }
            // Validate rows
            if (!int.TryParse(rows, out Rows))
            {
                isValid = false;
                validationMessage = "Number of rows must be a number.";
            }
            else
            {
                if (Rows < 11 || Rows > 265 || Rows % 2 == 0)
                {
                    isValid = false;
                    validationMessage = "Number of rows must be odd and in <11; 265> range.";
                }
            }

            return isValid;
        }
    }
}
