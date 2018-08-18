using Simple.Core.Code.ModuleParameters;
using Simple.Data;

namespace Simple.Core.Presenter
{
    public interface IArticleView
    {
        string Content { get; set; }
    }

    public class ArticlePresenter
    {
        public delegate ArticlePresenter Factory(IArticleView view, Domain.Module module);
        private readonly Domain.Module module;
        private readonly IArticleView view;
        private readonly ArticleParameter parameter;
        private readonly IUnitOfWork work;

        public ArticlePresenter(IArticleView view, Domain.Module module, IUnitOfWork work)
        {
            this.view = view;
            this.module = module;
            this.work = work;

            parameter = ParameterBuilder.Deserialize<ArticleParameter>(this.module.Parameter);
        }

        public void Initialize()
        {
            var article = work.ArticleRepository.GetById(parameter.ArticleId);

            view.Content = article.Text;
        }
    }
}