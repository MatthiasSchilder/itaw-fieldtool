using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMap.Rendering.Symbolizer;

namespace fieldtool.SharpmapExt.Symbolizers
{
    public abstract class FtBaseSymbolizer : BaseSymbolizer
    {
        public bool Labeled { get; set; }
    }
}
