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
        //List<Position> blackPositions;
        //List<Position> whitePositions;
        public List<Move> possibleMoves;
        
        /// <summary>
        /// Board containing 2-dimensional array of counters
        /// </summary>
        /// <param name="size">Must be even and >= 4</param>
        public Board(int size)
        {
            //blackPositions = new List<Position>();
            //whitePositions = new List<Position>();
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
                for (int j = (size / 2) - 1; j <= size / 2; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        board[j, i] = Colour.white;
                        //whitePositions.Add(new Position(j, i));
                    }
                    else
                    {
                        board[j, i] = Colour.black;
                        //blackPositions.Add(new Position(j, i));
                    }
                }
            }
        }

        /// <summary>
        /// Returns a list of Positions which the player can move to 
        /// </summary>
        /// <param name="playerColour"></param>
        /// <returns></returns>
        public List<Position> FindValidMoves(Colour playerColour)
        {
            if (playerColour == Colour.none)
            {
                throw new ArgumentException("colour must not be none");
            }

            List<Position> validMoves = new List<Position>();

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    // TODO: update this algorithm. Currently returns the positions of the player's counter already on the board, rather than the
                    // position of the empty square that the player needs to place a counter in.
                    Position currentPosition = new Position(j, i);
                    if (CheckSurroundingSquares(currentPosition, playerColour, Opposite(playerColour)))
                    {
                        validMoves.Add(currentPosition);
                    }
                }
            }
            return validMoves;
        }

        // TODO: This should return the empty square at the end of the recursive call instead of a boolean value;
        bool CheckSurroundingSquares(Position counterPosition, Colour playerColour, Colour enemyColour)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (CheckMove(counterPosition, new Position(j, i), playerColour, enemyColour))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Checks that there is at least one enemy counter before a counter of the player's colour before calling FindLineRecursively().
        /// </summary>
        /// <param name="counterPosition">Position of the player's counter to find whether or not there are valid moves from this counter</param>
        /// <param name="direction">Position with maximum size of 1 which sets the direction to convert</param>
        /// <param name="playerColour"></param>
        /// <returns>True if there is a line that the player can convert, otherwise false.</returns>
        bool CheckMove(Position counterPosition, Position direction, Colour playerColour, Colour enemyColour)
        {
            Position nextPosition = counterPosition.Add(direction);
            if (!OnBoard(nextPosition))
            {
                return false;
            }
            else if (board[nextPosition.x, nextPosition.y] == enemyColour)
            {
                return FindLineRecursively(nextPosition, direction, playerColour, enemyColour);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Recursively calls itself while the next counter is of the enemy's colour. Will return true if it reaches a 
        /// counter of the player's colour and will retun false if it reaches an empty square or the edge of the board.
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <param name="direction"></param>
        /// <param name="playerColour"></param>
        /// <param name="enemyColour"></param>
        /// <returns></returns>
        bool FindLineRecursively(Position currentPosition, Position direction, Colour playerColour, Colour enemyColour)
        {
            Position nextPosition = currentPosition.Add(direction);
            if (!OnBoard(nextPosition))
            {
                return false;
            }
            else if (board[nextPosition.x, nextPosition.y] == enemyColour)
            {
                return FindLineRecursively(nextPosition, direction, playerColour, enemyColour);
            }
            else if (board[nextPosition.x, nextPosition.y] == Colour.none)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Finds all of the positions that the player with the colour defined can place a counter
        /// </summary>
        /// <param name="colour">The Colour of the player taking their turn. Must not be Colour.none</param>
        /// <returns></returns>
        /*public List<Position> FindValidMoves(Colour colour)
        {
            if (colour == Colour.none)
            {
                throw new ArgumentException("colour must not be none");
            }
            List<Position> enemyPieces = new List<Position>();
            Colour enemyColour = Opposite(colour);

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[j, i] == enemyColour)
                    {
                        enemyPieces.Add(new Position(j, i));
                    }
                }
            }

            List<Position> possibleMoves = new List<Position>();
            Dictionary<Position, Position> MoveDirections = new Dictionary<Position, Position>();

            foreach(Position position in enemyPieces)
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (board[position.x + j, position.y + i] == Colour.none)
                        {
                            Position possibleMove = new Position(position.x + j, position.y + i);
                            MoveDirections.Add(possibleMove, new Position(-j, -i));
                        }
                    }
                }
            }

            //Key is the position of the counter to be placed.
            //Value is the direction in which the enemy counters are that will be converted
            foreach (KeyValuePair<Position, Position> moveDirection in MoveDirections)
            {
                if (CanConvertLine(moveDirection.Key.Add(moveDirection.Value), moveDirection.Value, colour))
                {
                    possibleMoves.Add(moveDirection.Key);
                    //change so that possible moves contains class of moves
                }
            }

            return possibleMoves;
        }*/

        /// <summary>
        /// Recursively calls to find whether or not a line of counters is present to be flipped
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="direction">Position with x and y between -1 and 1</param>
        /// <param name="colour">Colour of the counters of the person taking their turn. Must be either Colour.black or Colour.white</param>
        /// <returns></returns>
        bool CanConvertLine(Position startPosition, Position direction, Colour colour)
        {
            Position newPosition = startPosition.Add(direction);
            if (OnBoard(newPosition))
            {
                if (board[newPosition.x, newPosition.y] == colour)
                {
                    return true;
                }
                else if (board[newPosition.x, newPosition.y] == Opposite(colour))
                {
                    return CanConvertLine(startPosition.Add(direction), direction, colour);
                }
                else //board[newPosition.x, newPosition.y] == Colour.empty
                {
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the specified position is on the board.
        /// </summary>
        /// <param name="position"></param>
        /// <returns>True if position is on the board, else false</returns>
        bool OnBoard(Position position)
        {
            if (position.x >= 0 &&
                position.x < size &&
                position.y >= 0 &&
                position.y < size)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the enemy's colour given the current player's colour.
        /// </summary>
        /// <param name="colour"></param>
        /// <returns></returns>
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
