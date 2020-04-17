using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocean_DEMO
{
    public interface IOcean
    {
        bool SetNewPosition(Coordinate oldPosition, Coordinate newPosition);
        bool DislaceAtNewPosition(Coordinate oldPosition, Coordinate newPosition);
        bool Add(ICell c, Coordinate position);
        void Delete(ICell c);
        bool GetEmptyNeighbour(Coordinate position, out Coordinate result);
        bool GetPreyNeighbour(ICell c, out Coordinate result);
    }
}
