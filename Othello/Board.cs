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
        List<Position> blackPositions;
        List<Position> whitePositions;
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
                        whitePositions.Add(new Position(j, i));
                    }
                    else
                    {
                        board[j, i] = Colour.black;
                        blackPositions.Add(new Position(j, i));
                    }
                }
            }
        }

        /// <summary>
        /// Finds all of the positions that the player with the colour defined can place a counter
        /// </summary>
        /// <param name="colour">The Colour of the player taking their turn</param>
        /// <returns></returns>
        List<Position> FindValidMoves(Colour colour)
        {
            List<Position> enemyPieces;
            if (colour == Colour.black)
            {
                enemyPieces = whitePositions;
            }
            else if (colour == Colour.white)
            {
                enemyPieces = blackPositions;
            }
            else
            {
                throw new ArgumentException("colour cannot be none");
            }

            List<Position> possibleMoves = new List<Position>();
            Dictionary<Position, Position> MoveDirections = new Dictionary<Position, Position>();

            foreach(Position position in enemyPieces)
            {
                for (int i = -1; i < 1; i++)
                {
                    for (int j = -1; j < 1; j++)
                    {
                        if (board[position.x + j, position.y + i] == Colour.none)
                        {
                            Position possibleMove = new Position(position.x + j, position.y + i);
                            MoveDirections.Add(possibleMove, new Position(j, i));
                        }
                    }
                }
            }

            //TODO: check that the player will convert pieces if they place a counter here
            foreach ()
        }

        bool CheckValidMove()
        {

        }

        Colour Opposite(Colour colour)
        {
            switch (colour)
            {
                case Colour.white:
                    return Colour.black;
                case Colour.black:
                    return Colour.white;
                default:
                    return Colour.none;
            }
        }
    }
}
