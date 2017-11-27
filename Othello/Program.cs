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
            const int BOARD_SIZE = 64;
            Program prog = new Program();
            Board board = new Board(BOARD_SIZE);
            prog.PrintBoard(board);
            Random rnd = new Random();
            Colour currentPlayer = Colour.white;

            while (!prog.GameFinished(board, currentPlayer))
            {
                List<Position> possibleMoves = board.FindValidMoves(currentPlayer);
                Position selectedPosition = possibleMoves[rnd.Next(possibleMoves.Count)];
                board.MakeMove(selectedPosition, currentPlayer);
                prog.PrintBoard(board);
                Console.ReadLine();

                currentPlayer = board.Opposite(currentPlayer);
            }
            Console.WriteLine("GAME FINISHED");

            Console.ReadLine();
        }

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

        bool GameFinished(Board board, Colour currentPlayer)
        {
            bool boardFull = true;
            for (int i = 0; i < board.board.GetLength(0); i++)
            {
                for (int j = 0; j < board.board.GetLength(1); j++)
                {
                    if (board.board[j, i] == Colour.none)
                    {
                        boardFull = false;
                    }
                }
            }
            if (boardFull)
            {
                return true;
            }

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
