using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeCreatorLIB
{
    /// <summary>
    /// Represents a node in the maze.
    /// </summary>
    public class Node
    {
        //
        // PROPERTIES
        //

        /// <summary>
        /// The row in the maze that the node is in.
        /// </summary>
        public int Row { get; }

        /// <summary>
        /// The column in the maze that the node is in.
        /// </summary>
        public int Col { get; }

        /// <summary>
        /// Determines whether the node has been visited.
        /// </summary>
        public bool IsVisited { get; set; } = false;

        /// <summary>
        /// Determines whether the node is a junction.
        /// </summary>
        public bool IsJunction { get; set; } = false;

        /// <summary>
        /// Determines whether the node is a part of path.
        /// </summary>
        public bool IsParthOfPath { get; set; } = false;

        /// <summary>
        /// Determines whether the node was considered during path finding.
        /// </summary>
        public bool WasConsidered { get; set; } = false;

        /// <summary>
        /// A list of unvisited neighbour nodes.
        /// </summary>
        public List<Node> UnvisitedNeighbours { get; } = new List<Node>();

        /// <summary>
        /// A list of neighbour nodes.
        /// </summary>
        public List<Node> Neighbours { get; } = new List<Node>();

        /// <summary>
        /// A list of neighbouring junctions.
        /// </summary>
        public List<Node> NeighbourJunctions { get; } = new List<Node>();

        /// <summary>
        /// The array that this node belongs to.
        /// </summary>
        public Maze ParentMaze { get; }

        //
        // METHODS
        //

        /// <summary>
        /// Initializes an instance of a node.
        /// </summary>
        /// <param name="row">The row that the node is in.</param>
        /// <param name="col">The columns that the node is in.</param>
        /// <param name="parentMaze">The maze that this node belongs to.</param>
        public Node(int row, int col, Maze parentMaze)
        {
            Row = row;
            Col = col;
            ParentMaze = parentMaze;
        }

        /// <summary>
        /// Initializes an instance of a node.
        /// </summary>
        public Node()
        {
            Row = -1;
            Col = -1;
            ParentMaze = null;
        }

        /// <summary>
        /// Finds the neighbours of the node that are not visited.
        /// </summary>
        /// <returns>Neighbours of the node.</returns>
        public List<Node> FindUnvisitedNeighbours()
        {
            UnvisitedNeighbours.Clear();
            // Top
            if (Row > 1 && !ParentMaze[Row - 2, Col].IsVisited)
            {
                UnvisitedNeighbours.Add(ParentMaze[Row - 2, Col]);
            }
            // Left
            if (Col < ParentMaze.Cols - 2 && !ParentMaze[Row, Col + 2].IsVisited)
            {
                UnvisitedNeighbours.Add(ParentMaze[Row, Col + 2]);
            }
            // Bottom
            if (Row < ParentMaze.Rows - 2 && !ParentMaze[Row + 2, Col].IsVisited)
            {
                UnvisitedNeighbours.Add(ParentMaze[Row + 2, Col]);
            }
            // Right
            if (Col > 1 && !ParentMaze[Row, Col - 2].IsVisited)
            {
                UnvisitedNeighbours.Add(ParentMaze[Row, Col - 2]);
            }
            return UnvisitedNeighbours;
        }

        /// <summary>
        /// Finds all neighbours of the node.
        /// </summary>
        /// <returns></returns>
        public List<Node> FindNeighbours()
        {
            Neighbours.Clear();
            // Top
            if (Row > 1 && ParentMaze[Row - 1, Col].IsVisited)
            {
                Neighbours.Add(ParentMaze[Row - 2, Col]);
            }
            // Left
            if (Col < ParentMaze.Cols - 2 && ParentMaze[Row, Col + 1].IsVisited)
            {
                Neighbours.Add(ParentMaze[Row, Col + 2]);
            }
            // Bottom
            if (Row < ParentMaze.Rows - 2 && ParentMaze[Row + 1, Col].IsVisited)
            {
                Neighbours.Add(ParentMaze[Row + 2, Col]);
            }
            // Right
            if (Col > 1 && ParentMaze[Row, Col - 1].IsVisited)
            {
                Neighbours.Add(ParentMaze[Row, Col - 2]);
            }
            return Neighbours;
        }

        /// <summary>
        /// Finds the neighbouring junctions.
        /// </summary>
        /// <returns></returns>
        public List<Node> FindNeighbourJunctions()
        {
            NeighbourJunctions.Clear();
            Node j = new Node();
            // Top
            if (SearchForJunction(out j, y: -1))
            {
                NeighbourJunctions.Add(j);
            }
            // Right
            if (SearchForJunction(out j, x: 1))
            {
                NeighbourJunctions.Add(j);
            }
            // Bottom
            if (SearchForJunction(out j, y: 1))
            {
                NeighbourJunctions.Add(j);
            }
            // Left
            if (SearchForJunction(out j, x: -1))
            {
                NeighbourJunctions.Add(j);
            }
            return NeighbourJunctions;
        }

        public int DistanceTo(Node n)
        {
            return Math.Abs(Row - n.Row) + Math.Abs(Col - n.Col);
        }

        /// <summary>
        /// Searches for the closest junction in a given direction.
        /// </summary>
        /// <param name="x">The change in x direction in each step.</param>
        /// <param name="y">The change in y direction in each step.</param>
        /// <param name="junction">The found junction. Null if search failed.</param>
        /// <returns>Whether the search succeeded.</returns>
        public bool SearchForJunction(out Node junction, int x = 0, int y = 0)
        {
            junction = null;
            if (y != 0)
            {
                for (int i = Row + y; i > 0 && i < ParentMaze.Rows - 1; i+=y)
                {
                    if (ParentMaze[i, Col].IsJunction)
                    {
                        junction = ParentMaze[i, Col];
                        return true;
                    }
                    else if (!ParentMaze[i, Col].IsVisited)
                    {
                        return junction != null;
                    }
                }
            }
            else if (x != 0)
            {
                for (int i = Col + x; i > 0 && i < ParentMaze.Cols - 1; i += x)
                {
                    if (ParentMaze[Row, i].IsJunction)
                    {
                        junction = ParentMaze[Row, i];
                        return true;
                    }
                    else if (!ParentMaze[Row, i].IsVisited)
                    {
                        return junction != null;
                    }
                }
            }
            return junction != null;
        }


    }
}
