using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    struct Line
    {
        public Position startPoint;
        public Position endPoint;
        public int flipped;
    }

    class Move
    {
        public Position movePosition;
        public List<Position> flippedPieces;

        public Move(Position movePosition, List<Position> flippedPieces)
        {
            this.movePosition = movePosition;
            this.flippedPieces = flippedPieces;
        }

        public Move(List<Position> flippedPieces)
        {
            this.flippedPieces = flippedPieces;
        }

        public Move AddFlippedPieces(Move move)
        {
            List<Position> combinedPositions = new List<Position>();
            foreach (Position position in this.flippedPieces)
            {
                combinedPositions.Add(position);
            }
            foreach (Position position in move.flippedPieces)
            {
                combinedPositions.Add(position);
            }
            return new Move(this.movePosition, combinedPositions);
        }
    }


    /*class Move
    {
        Position position;
        List<Position> flippedCounters;     //Enemy's counters which are to be flipped in this move.
        List<Line> lines;
        public float q;

        public Move(Position position, List<Position> endCounters, float reward)
        {
            this.position = position;
            foreach (Position endCounter in endCounters)
            {
                Line tempLine = new Line();
                tempLine.startPoint = this.position;
                tempLine.endPoint = endCounter;
                int xDiff = Math.Abs(endCounter.x - this.position.x);
                int yDiff = Math.Abs(endCounter.y - this.position.y);

                if (xDiff > 1 && xDiff == yDiff ||
                    xDiff > 1 && yDiff == 0)
                {
                    tempLine.flipped = xDiff;
                }
                else if (yDiff > 1 && xDiff == 0)
                {
                    tempLine.flipped = yDiff;
                }
                else
                {
                    throw new Exception("Line does not meet specification");
                }

                lines.Add(new Line());
            }
            this.q = reward;
        }
    }*/
}
