using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Data
{
    public interface IWebsiteRepository : IBaseRepository<Domain.Website>
    {

    }

    public class WebsiteRepository : BaseRepository<Domain.Website>, IWebsiteRepository
    {
        public WebsiteRepository(IUnitOfWork work) : base(work) { }


    }
}
