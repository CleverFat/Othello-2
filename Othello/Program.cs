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
            Program prog = new Program();
            Board board = new Board(4);
            prog.PrintBoard(board);

            Colour currentPlayer = Colour.white;
            while (!prog.GameFinished(board, currentPlayer))
            {
                List<Position> possibleMoves = board.FindValidMoves(currentPlayer);

            }

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
    }
}
