using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Data
{
    public interface IMenuRepository : IBaseRepository<Domain.Menu>
    {

    }

    public class MenuRepository : BaseRepository<Domain.Menu>, IMenuRepository
    {
        public MenuRepository(IUnitOfWork work) : base(work) { }

    }
}
