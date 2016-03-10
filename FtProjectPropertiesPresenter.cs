using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fieldtool
{
    class FtProjectPropertiesPresenter : Presenter<IFtProjectPropertiesView>
    {
        private FtProject Project { get; set; }

        public FtProjectPropertiesPresenter(IFtProjectPropertiesView view) : base(view)
        {

        }
    }
}
