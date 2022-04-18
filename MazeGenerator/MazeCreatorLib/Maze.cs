using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeCreatorLIB
{
    /// <summary>
    /// Represents a maze and its properties.
    /// </summary>
    public class Maze
    {
        //
        // PROPERTIES
        //
        
        /// <summary>
        /// Random number generator.
        /// </summary>
        Random rng;

        /// <summary>
        /// An array of nodes in the maze.
        /// </summary>
        Node[,] nodes;

        public Node this[int row, int column]
        {
            get { return nodes[row, column]; }
        }

        /// <summary>
        /// A queue of visited nodes.
        /// </summary>
        Queue<Node> queue = new Queue<Node>();

        /// <summary>
        /// The node that is currently checked.
        /// </summary>
        public Node CurrentNode;

        /// <summary>
        /// Number of rows in the maze.
        /// </summary>
        public int Rows { get { return nodes.GetLength(0); } }

        /// <summary>
        /// Number of columns in the maze.
        /// </summary>
        public int Cols { get { return nodes.GetLength(1); } }

        /// <summary>
        /// Determines whether the maze is finished.
        /// </summary>
        private bool isFinished = false;

        /// <summary>
        /// Determines whether the maze is finished.
        /// </summary>
        public bool IsFinished
        {
            get { return isFinished; }
        }


        // 
        // METHODS
        //

        /// <summary>
        /// Initializes an instance of a maze.
        /// </summary>
        /// <param name="noRows">Number of rows in the maze.</param>
        /// <param name="noCols">Number of columns in the maze.</param>
        public Maze(int noRows, int noCols, int seed = -1)
        {
            // Create new maze array
            nodes = new Node[noRows, noCols];
            // Seed the RNG if necessary (if (seed >= 0) seed; else default;)
            rng = seed >= 0 ? new Random(seed) : new Random();
            // Populate the array with nodes.
            for (int i = 0; i < noRows; i++)
            {
                for (int j = 0; j < noCols; j++)
                {
                    nodes[i, j] = new Node(i, j, this);
                }
            }
            // Prepare maze creation.
            CurrentNode = nodes[1, 1];
            queue.Enqueue(CurrentNode);
            CurrentNode.IsVisited = true;
        }

        /// <summary>
        /// Perfroms the next step of maze creation.
        /// </summary>
        /// <returns>Whether the maze creation is done.</returns>
        public bool CreateMazeNextStep(out bool invalidate)
        {
            invalidate = true;
            Node nextNode;
            List<Node> neighbours = CurrentNode.FindUnvisitedNeighbours();
            if (neighbours.Count > 0)
            {
                nextNode = neighbours[rng.Next(0, neighbours.Count())];
                nextNode.IsVisited = true;
                nodes[(CurrentNode.Row + nextNode.Row) / 2, (CurrentNode.Col + nextNode.Col) / 2].IsVisited = true;
                CurrentNode = nextNode;
                queue.Enqueue(CurrentNode);
            }
            else
            {
                CurrentNode = queue.Dequeue();
                invalidate = false;
            }
            isFinished = !(queue.Count() > 0);
            return isFinished;
        }
    }
}
