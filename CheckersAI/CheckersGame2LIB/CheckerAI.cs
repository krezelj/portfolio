using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckersGame2LIB
{
    public static class CheckerAI
    {
        public static BoardModel Board { get { return Manager.CurrentBoard; } }

        public static int NodesExplored = 0;

        public static int OutcomesExplored = 0;

        public static MoveModel MakeMove(int Difficulty)
        {
            int recursionDepth = 0;
            NodesExplored = 0;
            OutcomesExplored = 0;
            switch (Difficulty)
            {
                case 0:
                    return FirstMove();
                case 1:
                    return RandomMove();
                case 2:
                    recursionDepth = 2;
                    break;
                case 3:
                    recursionDepth = 6;
                    break;
                case 4:
                    recursionDepth = 9;
                    break;
                case 5:
                    recursionDepth = 12;
                    break;
            }
            return MinMaxMove(recursionDepth);
        }

        private static void AssignMoveValue(BoardModel board, MoveModel move)
        {
            int score = 0;
            if (move.CaptureCell != null)
            {
                if (board.Checkers[(int)move.CaptureCell?.Column, (int)move.CaptureCell?.Row].IsKing)
                {
                    score += 4;
                }
                else
                {
                    score += 1;
                }
            }
            if (!board.Checkers[move.OriginCell.Column, move.OriginCell.Row].IsKing)
            {
                if ((board.Checkers[move.OriginCell.Column, move.OriginCell.Row].IsWhite && move.TargetCell.Row == 0) ||
                    (!board.Checkers[move.OriginCell.Column, move.OriginCell.Row].IsWhite && move.TargetCell.Row == 7))
                {
                    score += 3;
                }
            }
            if ((board.IsWhitePlaying && move.OriginCell.Row == 7) || (!board.IsWhitePlaying && move.OriginCell.Row == 0))
            {
                score -= 2;
            }
            move.MoveValue = score;
        }

        private static MoveModel MinMaxMove(int recursionDepth)
        {
            // Find all moves in current Board.
            List<MoveModel> rawMoves = Board.GetRawAvailableMoves();
            // If only one forced move - make it.
            if (rawMoves.Count == 1)
            {
                return rawMoves.First();
            }
            // Initialize values.
            MoveModel moveToMake = null;
            BoardModel consideredBoard;
            int moveValue = Board.IsWhitePlaying ? int.MinValue : int.MaxValue;
            int eval;
            int alpha = int.MinValue;
            int beta = int.MaxValue;
            // Sort moves based on heuristic.
            foreach (MoveModel m in rawMoves)
            {
                AssignMoveValue(Board, m);
            }
            rawMoves.Sort((x, y) => y.MoveValue.CompareTo(x.MoveValue));
            // Consider all moves.
            foreach (MoveModel move in rawMoves)
            {
                NodesExplored++;
                // Create child board based on considered move.
                consideredBoard = new BoardModel(Board, move);
                // If no bonus found, find all moves.
                if (consideredBoard.GetRawAvailableMoves().Count <= 0)
                {
                    consideredBoard.FindAllAvailableMoves();
                }
                // Evaluate board using MinMax.
                eval = MinMax(consideredBoard, recursionDepth, alpha, beta);
                // If white is playing 
                if (Board.IsWhitePlaying)
                {
                    // Check if current considered board is better than the previous one.
                    if (eval > moveValue)
                    {
                        moveValue = eval;
                        moveToMake = move;
                        alpha = Math.Max(alpha, eval);
                        if (beta <= alpha)
                            break;
                    }
                }
                // If black is playing
                else
                {
                    // Check if current considered board is better than the previous one.
                    if (eval < moveValue)
                    {
                        moveValue = eval;
                        moveToMake = move;
                        beta = Math.Min(beta, eval);
                        if (beta <= alpha)
                            break;
                    }
                }
            }
            // Return best found move.
            return moveToMake;
        }

        private static int MinMax(BoardModel boardToEvaluate, int recursionDepth, int alpha, int beta)
        {
            // If last node return static evaluation.
            if (recursionDepth <= 0)
            {
                OutcomesExplored++;
                return boardToEvaluate.Evaluate();
            }
            // Find available moves for this board.
            List<MoveModel> rawMoves = boardToEvaluate.GetRawAvailableMoves();
            // If no moves return static evaluation.
            if (rawMoves.Count <= 0)
            {
                OutcomesExplored++;
                return boardToEvaluate.Evaluate();
            }
            // Initialize values.
            int eval;
            int moveValue = boardToEvaluate.IsWhitePlaying ? int.MinValue : int.MaxValue;
            BoardModel childBoard;
            // Sort moves based on heuristic.
            foreach (MoveModel m in rawMoves)
            {
                AssignMoveValue(boardToEvaluate, m);
            }
            rawMoves.Sort((x, y) => y.MoveValue.CompareTo(x.MoveValue));
            // Consider all moves.
            foreach (MoveModel m in boardToEvaluate.GetRawAvailableMoves())
            {
                NodesExplored++;
                // Create child board based on considered move.
                childBoard = new BoardModel(boardToEvaluate, m);
                // If no bonus found, find all moves.
                if (childBoard.GetRawAvailableMoves().Count <= 0)
                {
                    childBoard.FindAllAvailableMoves();
                }
                // Evaluate board using MinMax.
                eval = MinMax(childBoard, recursionDepth - 1, alpha, beta);
                // Check if move had better value then the last one.
                if (boardToEvaluate.IsWhitePlaying)
                {
                    moveValue = Math.Max(moveValue, eval);
                    alpha = Math.Max(alpha, eval);
                    if (beta <= alpha)
                        break;
                }
                else
                {
                    moveValue = Math.Min(moveValue, eval);
                    beta = Math.Min(beta, eval);
                    if (beta <= alpha)
                        break;
                }
            }
            // Return best move value.
            return moveValue;
        }

        private static MoveModel RandomMove()
        {
            List<MoveModel> rawMoves = Manager.CurrentBoard.GetRawAvailableMoves();
            if (rawMoves.Count > 0)
            {
                MoveModel move = rawMoves[Manager.Rng.Next(0, rawMoves.Count)];
                return move;
            }
            return null;
        }

        private static MoveModel FirstMove()
        {
            List<MoveModel> rawMoves = Manager.CurrentBoard.GetRawAvailableMoves();
            MoveModel move = rawMoves.First();
            return move;
        }

    }
}
