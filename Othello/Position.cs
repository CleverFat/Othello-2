using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    class Position
    {
        public int x;
        public int y;
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Adds the x and y coordinates of the position parameter to this Position.
        /// </summary>
        /// <param name="position"></param>
        public Position Add(Position position)
        {
            return new Position(this.x + position.x, this.y + position.y);
        }

        public void AddToSelf(Position position)
        {
            this.x += position.x;
            this.y += position.y;
        }
    }
}
