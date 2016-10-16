using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fieldtool.SharpmapExt.Symbolizers;
using SharpMap.Data;
using SharpMap.Rendering.Symbolizer;
using SharpMap.Rendering.Thematics;
using SharpMap.Styles;

namespace fieldtool.SharpmapExt.Themes
{
    public class VectorThemeSymbolizer : ITheme
    {
        private readonly VectorStyle _style;
        private bool _doRotation;

        public VectorThemeSymbolizer(ISymbolizer symbolizer)
        {
            _style = new VectorStyle {PointSymbolizer = (IPointSymbolizer) symbolizer};
            if (symbolizer is FtArrowPointSymbolizer)
                _doRotation = true;
        }

        public IStyle GetStyle(FeatureDataRow fdr)
        {
            //VectorStyle result = _style.Clone();
            //if(!_doRotation)
            //    return _style;
            //result.SymbolRotation = (float)((double)fdr.ItemArray[6]);

            return _style;
        }
    }
}
