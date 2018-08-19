using Simple.Core.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Simple.Core.Views
{
    public partial class ArticleView : BaseUserControlModule, IArticleView
    {
        public string Content
        {
            get
            {
                return cont.Text;
            }
            set
            {
                cont.Text = value;
            }
        }

        public ArticlePresenter.Factory Factory { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var presenter = Factory(this, Module);

            presenter.Initialize();
        }
    }
}