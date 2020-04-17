using Ocean_DEMO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocean_DEMO
{
    public class Tuna : Predator
    {

        private ESpecies _kind;

        public Tuna(IOcean ocean, Coordinate pos, int curTimeToReproduce, int curTimeToFeed, int timeToReproduce , int timeToFeed )
            : base(ocean, pos, curTimeToReproduce, curTimeToFeed, timeToReproduce, timeToFeed)
        {
            _kind = ESpecies.Tuna;
        }

        public Tuna(Tuna sh)
            : base(sh)
        {
            _kind = ESpecies.Tuna;
        }

        public override char AnObject
        {
            get
            {
                return (char)ECharObject.PredatorTuna;
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
            return new Tuna(this);
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
