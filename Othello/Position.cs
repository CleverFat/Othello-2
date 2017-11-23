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
        /// <returns>Position containing the x and y values calculated from the sum of the positions</returns>
        public Position Add(Position position)
        {
            return new Position(this.x + position.x, this.y + position.y);
        }

        /// <summary>
        /// Adds the x and y coordinates of the position parameter to this object's x and y variables.
        /// The result is stored in this object's x and y variables.
        /// </summary>
        /// <param name="position"></param>
        public void AddToSelf(Position position)
        {
            this.x += position.x;
            this.y += position.y;
        }
    }
}
