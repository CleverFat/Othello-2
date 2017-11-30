using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    enum Colour
    {
        none,
        black,
        white
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
                    }
                    else
                    {
                        board[j, i] = Colour.black;
                    }
                }
            }
        }

        /// <summary>
        /// Returns a list of Positions which the player can move to 
        /// </summary>
        /// <param name="playerColour"></param>
        /// <returns></returns>
        public List<Move> FindValidMoves(Colour playerColour)
        {
            if (playerColour == Colour.none)
            {
                throw new ArgumentException("colour must not be none");
            }

            List<Position> playerCounters = new List<Position>();
            
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[j, i] == playerColour)
                    {
                        playerCounters.Add(new Position(j, i));
                    }
                }
            }

            List<Move> validMoves = new List<Move>();

            foreach (Position playerCounter in playerCounters)
            {
                List<Move> currentCounterMoves = CheckSurroundingSquares(playerCounter, playerColour, Opposite(playerColour));
                validMoves = AddToValidMoves(validMoves, currentCounterMoves);                
            }
            return validMoves;
        }

        /// <summary>
        /// Goes through newMoves. If the position is already in validMoves, the function only adds the flippedPositions 
        /// to the move with the same movePosition. Otherwise, it will add the move to the list, and return it.
        /// </summary>
        /// <param name="validMoves"></param>
        /// <param name="newMoves"></param>
        /// <returns></returns>
        List<Move> AddToValidMoves(List<Move> validMoves, List<Move> newMoves)
        {
            foreach (Move move in newMoves)
            {
                bool inValidMoves = false;
                for (int i = 0; i < validMoves.Count; i++)
                {
                    if (validMoves[i].movePosition.Equals(move.movePosition))
                    {
                        inValidMoves = true;
                        foreach (Position flippedCounter in move.flippedPieces)
                        {
                            validMoves[i].flippedPieces.Add(flippedCounter);
                        }
                    }
                }
                if (!inValidMoves)
                {
                    validMoves.Add(move);
                }
            }
            return validMoves;
        }

        /// <summary>
        /// Checks if the list parameter contains an object with equivalent variables to the Position parameter.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        bool ListContains(List<Position> positions, Position position)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                if (position.Equals(positions[i]))
                {
                    return true;
                }
            }
            return false;
        }

        bool ListContains(List<Move> moves, Move move)
        {
            for (int i = 0; i < moves.Count; i++)
            {
                if (moves[i].movePosition.Equals(move.movePosition))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Loops through the squares surrounding the Position of the counter that is passed in and checks if there is a line of the 
        /// enemy's colour with an empty space at the end.
        /// </summary>
        /// <param name="counterPosition"></param>
        /// <param name="playerColour"></param>
        /// <param name="enemyColour"></param>
        /// <returns></returns>
        List<Move> CheckSurroundingSquares(Position counterPosition, Colour playerColour, Colour enemyColour)
        {
            List<Move> possibleMoves = new List<Move>();
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    Move possibleMove = CheckMove(counterPosition, new Position(j, i), playerColour, enemyColour);
                    if (possibleMove != null)
                    {
                        possibleMoves.Add(possibleMove);
                    }
                }
            }
            return possibleMoves;
        }

        /// <summary>
        /// Checks that there is at least one enemy counter before a counter of the player's colour before calling FindLineRecursively().
        /// </summary>
        /// <param name="counterPosition">Position of the player's counter to find whether or not there are valid moves from this counter</param>
        /// <param name="direction">Position with maximum size of 1 which sets the direction to convert</param>
        /// <param name="playerColour"></param>
        /// <returns>True if there is a line that the player can convert, otherwise false.</returns>
        Move CheckMove(Position counterPosition, Position direction, Colour playerColour, Colour enemyColour)
        {
            Move move = new Move(new List<Position>());
            Position nextPosition = counterPosition.Add(direction);
            if (!OnBoard(nextPosition))
            {
                return null;
            }
            else if (board[nextPosition.x, nextPosition.y] == enemyColour)
            {
                move.flippedPieces.Add(nextPosition);
                return FindLineRecursively(nextPosition, direction, playerColour, enemyColour, move);
            }
            else
            {
                return null;
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
        /// <param name="move">The move parameter that will be returned. Each enemy position is added to the flippedCounter list until the
        /// method reaches the end of the line of enemy counters</param>
        /// <returns></returns>
        Move FindLineRecursively(Position currentPosition, Position direction, Colour playerColour, Colour enemyColour, Move move)
        {
            Position nextPosition = currentPosition.Add(direction);
            if (!OnBoard(nextPosition))                                     //cannot convert line - hit the edge of the board
            {
                return null;
            }
            else if (board[nextPosition.x, nextPosition.y] == enemyColour)  //There are still pieces left in the line to check
            {
                //flippedPieces.Add(nextPosition);
                move.flippedPieces.Add(nextPosition);
                return FindLineRecursively(nextPosition, direction, playerColour, enemyColour, move);
            }
            else if (board[nextPosition.x, nextPosition.y] == Colour.none)  //reached the end of the line
            {
                move.movePosition = nextPosition;
                return move;
                //return nextPosition;
            }
            else
            {
                return null;
            }
        }

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
        public Colour Opposite(Colour colour)
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

        /// <summary>
        /// Places a counter in the position of the move.movePosition and flips all of the pieces in the move.flippedPieces to 
        /// the player's colour.
        /// </summary>
        /// <param name="move"></param>
        /// <param name="playerColour"></param>
        public void MakeMove(Move move, Colour playerColour)
        {
            foreach (Position counter in move.flippedPieces)
            {
                board[counter.x, counter.y] = playerColour;
            }
            board[move.movePosition.x, move.movePosition.y] = playerColour;
        }
    }
}
