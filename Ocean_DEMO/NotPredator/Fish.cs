using Ocean_DEMO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocean_DEMO
{
    class Fish : Organism
    {
        ESpecies _kind;

        public Fish(IOcean ocean, Coordinate pos, int curTimeToReproduce, int timeToReproduce )
            : base(ocean, pos, curTimeToReproduce, timeToReproduce)
        {
            _kind = ESpecies.Fish;
        }

        public Fish(Fish p)
            : this(p._ocean, p._position, p._currentTimeToReproduce, p._timeToReproduce)
        {
            _kind = ESpecies.Fish;
        }


        public override ESpecies Kind
        {
            get
            {
                return _kind;
            }
        }

        public override char AnObject
        {
            get
            {
                return (char)ECharObject.NotPredatorFish;
            }
        }

        public override object Clone()
        {
            return new Fish(this);
        }
    }
}
