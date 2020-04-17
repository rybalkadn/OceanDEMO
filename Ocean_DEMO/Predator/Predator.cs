using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocean_DEMO
{
    /// <summary>
    /// Хищник являеться видом добычи
    /// </summary>
    public abstract class Predator : Organism
    {
        protected int _timeToFeed;
        protected int _currentTimeToFeed;

        public Predator(IOcean ocean, Coordinate pos, int curTimeToReproduce, int curTimeToFeed, int timeToReproduce , int timeToFeed )
            : base(ocean, pos, curTimeToReproduce, timeToReproduce)
        {
            _timeToFeed = timeToFeed;
            _currentTimeToFeed = curTimeToFeed;
        }

        public Predator(Predator p)
            : base(p)
        {
            _timeToFeed = p._timeToFeed;
            _currentTimeToFeed = p._currentTimeToFeed;
        }

        /// <summary>
        /// перемещение и размножение
        /// </summary>
        public override void Process()
        {
            if (--_currentTimeToFeed <= 0)
            {
                _ocean.Delete(this);
            }
            else
            {
                Coordinate oldPosition;

                if (Feed(out oldPosition))
                {
                    --_currentTimeToReproduce;
                    if (_currentTimeToReproduce == 0)
                    {
                        Reproduce(oldPosition);
                    }
                }
                else
                {
                    base.Process();
                }
            }
        }

        protected virtual bool Feed(out Coordinate oldPosition)
        {
            bool result = false;
            Coordinate preyPos = new Coordinate();
            oldPosition = _position;

            if (_ocean.GetPreyNeighbour(this, out preyPos))
            {
                _ocean.DislaceAtNewPosition(_position, preyPos);
                _currentTimeToFeed = _timeToFeed;
                result = true;
            }

            return result;
        }
    }
}
