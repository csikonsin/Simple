using Simple.Services;
using Simple.Core.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Simple.Core.ModuleParameters;

namespace Simple.Core.Presenter
{
    public interface IArticlesView
    {
        List<ArticleViewModel> Articles { set; }
        string BackgroundColorHex {  set; }
    }

    public class ArticlesOverviewParameter : BaseParameter
    {
        public string BackgroundColorHex { get; set; }

    }

    public class ArticleOverviewPresenter
    {
        public delegate ArticleOverviewPresenter Factory(IArticlesView view, Domain.Module module);

        private readonly Domain.Module module;

        private readonly ArticlesOverviewParameter parameter;

        private readonly IArticleService articleService;
        public  IArticlesView view;

        public ArticleOverviewPresenter(IArticlesView view, Domain.Module module, IArticleService articleService)
        {
            this.view = view;
            this.articleService = articleService;
            this.module = module;

            this.parameter = this.module?.Parameter.Deserialize<ArticlesOverviewParameter>();
        }

        public void LoadArticles()
        {
            var articles = articleService.GetAll();
            var vms = new List<ArticleViewModel>();
            foreach (var article in articles)
            {
                var vm = new ArticleViewModel
                {
                    Id = article.Id,
                    Heading = article.Heading,
                    Text = article.Text,
                    CreatedAt = article.CreatedAt,
                    CreatedBy = "Max Mustermann"
                };
                vms.Add(vm);
            }
            view.Articles = vms;

            if (this.parameter != null)
            {
                view.BackgroundColorHex = this.parameter.BackgroundColorHex;
            }
        }
    }

    public class ArticleViewModel
    {
        public int Id { get; set; }
        public string Heading { get; set; }
        public string Text { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}