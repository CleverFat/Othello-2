using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    class Program
    {
        static void Main(string[] args)
        {
            const int BOARD_SIZE = 8;
            bool gameFinished = false;
            Colour currentPlayer = Colour.white;
            Program prog = new Program();
            Board board = new Board(BOARD_SIZE);
            Random rnd = new Random();

            prog.PrintBoard(board);
            Console.ReadLine();

            while (!gameFinished)
            {
                List<Position> possibleMoves = board.FindValidMoves(currentPlayer);
                if (possibleMoves.Count == 0)
                {
                    gameFinished = true;
                }
                else
                {
                    Position selectedPosition = possibleMoves[rnd.Next(possibleMoves.Count)];
                    board.MakeMove(selectedPosition, currentPlayer);
                    prog.PrintBoard(board);
                    Console.ReadLine();

                    currentPlayer = board.Opposite(currentPlayer);
                }
            }
            Console.WriteLine("GAME FINISHED");

            switch (prog.CheckWinner(board))
            {
                case Colour.white:
                    Console.WriteLine("White won");
                    break;
                case Colour.black:
                    Console.WriteLine("Black won");
                    break;
                default:
                    Console.WriteLine("Draw");
                    break;
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Prints board to console
        /// </summary>
        /// <param name="board"></param>
        void PrintBoard(Board board)
        {
            for (int i = 0; i < board.board.GetLength(0); i++)
            {
                for (int j = 0; j < board.board.GetLength(1); j++)
                {
                    switch (board.board[j, i])
                    {
                        case Colour.white:
                            Console.Write("w");
                            break;
                        case Colour.black:
                            Console.Write("b");
                            break;
                        default:
                            Console.Write("-");
                            break;
                    }
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Checks if there are no moves left for the current player to take
        /// </summary>
        /// <param name="board"></param>
        /// <param name="currentPlayer"></param>
        /// <returns></returns>
        bool GameFinished(Board board, Colour currentPlayer)
        {
            if (board.FindValidMoves(currentPlayer).Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Counts the number of pieces of each colour on the board to determine who won
        /// </summary>
        /// <param name="board"></param>
        /// <returns>Returns Colour.white if white won, Colour.black if black won and Colour.none if the game resulted in a draw</returns>
        Colour CheckWinner(Board board)
        {
            int whiteCount = 0;
            int blackCount = 0;
            for (int i = 0; i < board.board.GetLength(0); i++)
            {
                for (int j = 0; j < board.board.GetLength(1); j++)
                {
                    switch (board.board[j, i])
                    {
                        case Colour.white:
                            whiteCount++;
                            break;
                        case Colour.black:
                            blackCount++;
                            break;
                        default:
                            break;
                    }
                }
            }
            if (whiteCount > blackCount)
            {
                return Colour.white;
            }
            else if (blackCount > whiteCount)
            {
                return Colour.black;
            }
            else
            {
                return Colour.none;
            }
        }
    }
}
