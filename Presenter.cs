using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fieldtool
{
    public class Presenter<TView> where TView : class, IView
    {
        public TView View { get; private set; }

        public Presenter(TView view)
        {
            if (view == null)
                throw new ArgumentNullException("view");

            View = view;
            View.Initialize += OnViewInitialize;
            View.Load += OnViewLoad;
        }

        protected virtual void OnViewInitialize(object sender, EventArgs e) { }

        protected virtual void OnViewLoad(object sender, EventArgs e) { }
    }
}
