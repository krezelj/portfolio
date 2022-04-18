using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckersGame2LIB
{
    public static class Manager
    {
        public static Random Rng { get; set; } = new Random();
        private static Stopwatch _timer { get; set; } = new Stopwatch();
        public static uint Ticks { get; set; } = 0;

        public static Form BoardCanvas { get; set; }
        public static Label NodesCountLabel { get; set; }
        public static Label OutcomesCountLabel { get; set; }
        public static Label BoardValueNumberLabel { get; set; }
        public static Button LauncherPlayButton { get; set; }

        public static BoardModel CurrentBoard { get; set; }
        public static bool GameClosed { get; set; } = false;
        public static int AnimationLength { get; set; } = 200;

        public static PlayerType PlayerOneType;
        public static PlayerType PlayerTwoType;
        public static int PlayerOneDifficulty = 1;
        public static int PlayerTwoDifficulty = 4;

        public static void StartNewGame()
        {
            CurrentBoard = new BoardModel();
            BoardCanvas.MouseDown += CurrentBoard.OnMouseDown;
            BoardCanvas.MouseMove += CurrentBoard.OnMouseMove;

            //Start loop on separate thread
            GameClosed = false;
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                MainLoop();
            }).Start();

        }

        public static bool CheckIfGameFinished()
        {
            return CurrentBoard.AvailableMoves.Count > 0;
        }

        public static void GenerateComputerMove(int Difficulty)
        {
            MoveModel move;
            bool becameKing = false;
            do
            {
                move = CheckerAI.MakeMove(Difficulty);
                CanvasManager.AnimateMove(move);
                becameKing = CurrentBoard.ApplyMove(move);
            } while (!becameKing && move.CaptureCell != null && CurrentBoard.CheckForBonus(move.TargetCell.Column, move.TargetCell.Row));
            CurrentBoard.TurnEnd();
        }

        public static void UpdateDiagnostics()
        {
            // Show diagnostics
            if (BoardCanvas.Visible)
            {
                NodesCountLabel.Invoke((MethodInvoker)delegate { NodesCountLabel.Text = $"{CheckerAI.NodesExplored}"; });
                OutcomesCountLabel.Invoke((MethodInvoker)delegate { OutcomesCountLabel.Text = $"{CheckerAI.OutcomesExplored}"; });
                BoardValueNumberLabel.Invoke((MethodInvoker)delegate { BoardValueNumberLabel.Text = $"{CurrentBoard.Evaluate()}"; });
            }
            //
        }

        private static void MainLoop()
        {
            while (CheckIfGameFinished() && !GameClosed)
            {
                _timer.Restart();
                ++Ticks;
                if (CurrentBoard.IsWhitePlaying && PlayerOneType == PlayerType.Computer)
                {
                    if (BoardCanvas.Visible)
                    {
                        BoardCanvas.Invoke((MethodInvoker)delegate { BoardCanvas.Invalidate(); });
                    }
                    GenerateComputerMove(PlayerOneDifficulty);
                    UpdateDiagnostics();
                    CanvasManager.InvalidationPending = true;
                }
                if (!CurrentBoard.IsWhitePlaying && PlayerTwoType == PlayerType.Computer)
                {
                    if (BoardCanvas.Visible)
                    {
                        BoardCanvas.Invoke((MethodInvoker)delegate { BoardCanvas.Invalidate(); });
                    }
                    GenerateComputerMove(PlayerTwoDifficulty);
                    UpdateDiagnostics();
                    CanvasManager.InvalidationPending = true;
                }
                if (CanvasManager.InvalidationPending)
                {
                    try{ BoardCanvas.Invoke((MethodInvoker)delegate { BoardCanvas.Invalidate(); }); }
                    catch (Exception) { }
                }
                Thread.Sleep((int)(Math.Max(15 - _timer.ElapsedMilliseconds, 0)));
            }
            if (!GameClosed)
            {
                BoardCanvas.Invoke((MethodInvoker)delegate { BoardCanvas.Invalidate(); });
                MessageBox.Show("Game finished.");
            }
            LauncherPlayButton.Invoke((MethodInvoker)delegate { LauncherPlayButton.Enabled = true; });
        }
    }

    public enum PlayerType { Human, Computer }
}
