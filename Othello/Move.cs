using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    struct Line
    {
        Position startPoint;
        Position endPoint;
        Position direction;
    }

    class Move
    {
        Position position;
        List<Position> endCounters;
        List<Line> lines;
        public float q;
        public Move(Position position, List<Position> endCounters, float reward)
        {
            this.position = position;
            this.endCounters = endCounters;
            this.q = reward;
        }
    }
}
