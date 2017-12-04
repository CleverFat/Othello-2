using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
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
    }
}
