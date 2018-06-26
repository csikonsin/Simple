using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simple.Data
{
    public interface IArticleRepository : IBaseRepository<Domain.Article>
    {

    }
    public class ArticleRepository : BaseRepository<Domain.Article>, IArticleRepository
    {
        public ArticleRepository(Data.IUnitOfWork work) :base(work)
        {
        }

    }
}