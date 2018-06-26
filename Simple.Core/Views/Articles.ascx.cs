using Simple.Core.Presenter;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Simple.Core.Views
{
    public class BaseControl : UserControl
    {
        public Domain.Module Module { get; set; }
    }

    public partial class ArticlesView : BaseControl, IArticlesView
    {
        public ArticleOverviewPresenter.Factory PresenterFactory { get; set; }

        public List<ArticleViewModel> Articles
        {
            set
            {
                repArticles.DataSource = value;
                repArticles.DataBind();
            }
        }

        public string BackgroundColorHex
        {
            set
            {
                pnArticles.Style.Add(HtmlTextWriterStyle.BackgroundColor, value);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var presenter = PresenterFactory(this, Module);
            presenter.LoadArticles();            
        }

        protected void Save(object sender, EventArgs e)
        {
        }
    }
}