using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Simple.Data;

namespace Simple.Services
{
    public interface IArticleService
    {
        List<Domain.Article> GetAll();
    }
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork work;

        public ArticleService(IUnitOfWork work)
        {
            this.work = work;
        }

        public List<Domain.Article> GetAll()
        {
            return work.ArticleRepository.GetList();
        }
    }
}