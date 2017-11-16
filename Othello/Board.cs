using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    enum Colour
    {
        black,
        white,
        none
    }

    class Board
    {
        public int size;
        public Colour[,] board;
        public List<Move> possibleMoves;
        
        /// <summary>
        /// Board containing 2-dimensional array of counters
        /// </summary>
        /// <param name="size">Must be even and >= 4</param>
        public Board(int size)
        {
            if (size % 2 == 1 || size < 4)
            {
                throw new Exception("size must be an even integer >= 4");
            }
            this.size = size;
            board = new Colour[size, size];
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[j, i] = Colour.none;
                }
            }

            for (int i = (size / 2) - 1; i <= size / 2; i++)
            {
                for (int j = (size / 2) - 1; i <= size / 2; i++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        board[j, i] = Colour.white;
                    }
                    else
                    {
                        board[j, i] = Colour.black;
                    }
                }
            }
        }
    }
}
