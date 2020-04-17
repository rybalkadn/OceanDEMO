using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocean_DEMO
{
    public abstract class Organism : ICell
    {
        abstract public ESpecies Kind { get; }
        protected Coordinate _position;
        protected readonly IOcean _ocean;
        protected readonly int _timeToReproduce;
        protected int _currentTimeToReproduce;

        public Organism(IOcean ocean, Coordinate pos, int curTimeToReproduce, int timeToReproduce)
        {
            _ocean = ocean;
            _position = pos;
            _timeToReproduce = timeToReproduce;
            _currentTimeToReproduce = _timeToReproduce;
        }

        public Organism(Organism obj)
            : this(obj._ocean, obj._position, obj._currentTimeToReproduce, obj._timeToReproduce)
        {

        }

        public bool IsPassive
        {
            get
            {
                return false;
            }
        }

        public Coordinate Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }

        }

        public abstract char AnObject
        {
            get;
        }

        public virtual void Process()
        {
            Coordinate oldPosition;

            --_currentTimeToReproduce;

            if (Move(out oldPosition) && _currentTimeToReproduce == 0)
            {
                Reproduce(oldPosition);
            }
        }

        protected void Reproduce(Coordinate position)
        {
            _currentTimeToReproduce = _timeToReproduce;
            Organism c = (Organism)this.Clone();
            _ocean.Add(c, position);
        }

        protected bool Move(out Coordinate oldPosition)
        {
            bool result = false;
            Coordinate newPos = new Coordinate();
            oldPosition = _position;

            if (_ocean.GetEmptyNeighbour(_position, out newPos))
            {
                _ocean.SetNewPosition(_position, newPos);
                result = true;
            }

            return result;
        }

        /// <summary>
        /// животные не поедают других животных
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public virtual bool CanDisplace(ICell c)
        {
            return false; 
        }

        public abstract object Clone();
    }
}
