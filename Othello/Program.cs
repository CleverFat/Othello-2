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

            board.board = new Colour[4, 4];
            for (int i = 0; i < board.board.GetLength(0); i++)
            {
                for (int j = 0; j < board.board.GetLength(1); j++)
                {
                    board.board[j, i] = Colour.none;
                }
            }

            board.board[0, 1] = Colour.white;
            board.board[2, 1] = Colour.white;
            board.board[1, 1] = Colour.black;

            Console.WriteLine("POSSIBLE MOVES");
            foreach (Position position in board.FindValidMoves(Colour.black))
            {
                for (int i = 0; i < board.board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.board.GetLength(1); j++)
                    {
                        if (j == position.x && i == position.y)
                        {
                            Console.Write("x");
                        }
                        else
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
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
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
    }
}
