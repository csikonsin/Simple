using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;
using Simple.Domain;

namespace Simple.Data
{
    public interface IModuleRepository : IBaseRepository<Domain.Module>
    {
        List<Domain.Module> GetAllByMenuId(int menuId);
    }

    public class ModuleRepository : BaseRepository<Domain.Module>, IModuleRepository
    {
        public ModuleRepository(IUnitOfWork work) : base(work) { }

        public List<Module> GetAllByMenuId(int menuId)
        {
            var p = Predicates.Field<Domain.Module>(x => x.MenuId, Operator.Eq, menuId);
            var result = Connection.GetList<Domain.Module>(p, null, Transaction).ToList();
            return result;
        }
    }
}
