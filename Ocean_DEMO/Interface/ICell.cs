using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocean_DEMO
{
    /// <summary>
    /// для создания полной копии.
    /// </summary>
    public interface ICell : ICloneable
    {
        bool IsPassive { get; }
        char AnObject { get; }    //объект
        Coordinate Position { get; set; }
        bool CanDisplace(ICell c);
        void Process();
    }
}
