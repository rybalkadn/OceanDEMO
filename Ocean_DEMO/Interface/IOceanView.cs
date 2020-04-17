using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Ocean_DEMO
{
    interface IOceanView : IEnumerable
    {
        int Width { get; }
        int Height { get; }
    }
}
