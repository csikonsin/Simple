using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;

namespace Simple.Data
{
    public interface IMenuRepository : IBaseRepository<Domain.Menu>
    {
        Domain.Menu GetByPublicUrl(string absolutePath);
    }

    public class MenuRepository : BaseRepository<Domain.Menu>, IMenuRepository
    {
        public MenuRepository(IUnitOfWork work) : base(work) { }

        public Domain.Menu GetByPublicUrl(string absolutePath)
        {
            var p = Predicates.Field<Domain.Menu>(m => m.PublicUrl, Operator.Eq, absolutePath);
            var result = Connection.GetList<Domain.Menu>(p, null, Transaction).FirstOrDefault();
            return result;
        }
    }
}
