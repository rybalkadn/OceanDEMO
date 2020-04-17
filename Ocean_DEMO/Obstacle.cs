using Ocean_DEMO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocean_DEMO
{
    /// <summary>
    /// Переграда
    /// </summary>
    public class Obstacle : ICell
    {
        private Coordinate _position;

        public Obstacle(Coordinate pos)
        {
            _position = pos;
        }

        public char AnObject
        {
            get
            {
                return (char)ECharObject.AnObject;
            }
        }

        public bool IsPassive
        {
            get
            {
                return true;
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

        /// <summary>
        /// животные не поедают других животных
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool CanDisplace(ICell c)
        {
            return false;
        }

        public object Clone()
        {
            return new Obstacle(_position);
        }

        public void Process()
        {
            return;
        }
    }
}
