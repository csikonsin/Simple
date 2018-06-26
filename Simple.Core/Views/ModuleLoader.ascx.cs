using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Simple.Core.Views
{
    public partial class ModuleLoader : System.Web.UI.UserControl, IModuleLoaderView
    {
        private ModuleLoaderPresenter.Factory Factory { get; set; }

        public ControlCollection ControlCollection
        {
            get
            {
                return ph.Controls;
            }
        }

        public ModuleLoader()
        {            
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var presenter = Factory(this);
            presenter.Initialize();
        }
    }
}