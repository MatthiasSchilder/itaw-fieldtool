using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoAPI.Geometries;
using SharpMap.Rendering.Symbolizer;

namespace fieldtool.Data.Movebank
{
    public class FtTagVisulization
    {
        public Color VisulizationColor;
        public ISymbolizer Symbolizer;
        public bool SymbolizerWithLabel;
    }
}
