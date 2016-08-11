using fieldtool.View;

namespace fieldtool.Presenter
{
    class FtProjectPropertiesPresenter : Presenter<IFtProjectPropertiesView>
    {
        private FtProject Project { get; set; }

        public FtProjectPropertiesPresenter(IFtProjectPropertiesView view) : base(view)
        {

        }
    }
}
