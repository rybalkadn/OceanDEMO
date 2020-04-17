using Ocean_DEMO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocean_DEMO
{
    /// <summary>
    /// Акула
    /// </summary>
    public class Shark : Predator
    {

        private ESpecies _kind;

        public Shark(IOcean ocean, Coordinate pos, int curTimeToReproduce, int curTimeToFeed, int timeToReproduce , int timeToFeed )
            : base(ocean, pos, curTimeToReproduce, curTimeToFeed, timeToReproduce, timeToFeed)
        {
            _kind = ESpecies.Shark;
        }

        public Shark(Shark sh)
            : base(sh)
        {
            _kind = ESpecies.Shark;
        }

        public override char AnObject
        {
            get
            {
                return (char)ECharObject.PredatorShark;
            }
        }

        public override ESpecies Kind
        {
            get
            {
                return _kind;
            }
        }

        public override object Clone()
        {
            return new Shark(this);
        }

        /// <summary>
        /// животные не поедают других животных
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public override bool CanDisplace(ICell c)
        {
            bool IsFood = false;
            Organism biomass = c as Organism;

            if (biomass == null)
            {
                IsFood = false; 
            }
            else
            {
                switch (biomass.Kind)
                {
                    case ESpecies.Fish: 
                        IsFood = true;
                        break;
                    default:
                        IsFood = false;
                        break;
                }
            }

            return IsFood;
        }
    }
}
