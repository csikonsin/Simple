using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Data;
using Simple.Domain;

namespace Simple.Service
{
    public interface IMenuService
    {
        Menu GetCurrentMenu(string absolutePath);
    }
    public class MenuService : IMenuService
    {
        private readonly IUnitOfWork work;
        public MenuService(IUnitOfWork work)
        {
            this.work = work;
        }

        public Menu GetCurrentMenu(string absolutePath)
        {
            var menu = work.MenuRepository.GetByPublicUrl(absolutePath);
            return menu;
        }
    }
}
