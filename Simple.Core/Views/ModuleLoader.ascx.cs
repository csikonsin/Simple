using Autofac;
using System;
using System.Web.UI;

namespace Simple.Core.Views
{
    public partial class ModuleLoader : System.Web.UI.UserControl, IModuleLoaderView
    {
        private ModuleLoaderPresenter.Factory Factory { get; set; }
        public string Identity { get; set; }

        public ControlCollection ControlCollection
        {
            get
            {
                return ph.Controls;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var presenter = Factory(this);
            presenter.Initialize();
        }
    }
}