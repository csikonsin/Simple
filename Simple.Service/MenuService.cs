using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Simple.Data;
using Simple.Domain;

namespace Simple.Service
{
    public interface IMenuService
    {
        Menu GetCurrentMenu();
        Menu GetMenu(string absolutePath);
    }
    public class MenuService : IMenuService
    {
        private readonly IUnitOfWork work;
        private readonly HttpContextBase httpContextBase;

        public MenuService(IUnitOfWork work, HttpContextBase httpContext)
        {
            this.work = work;
            this.httpContextBase = httpContext;
        }

        public Menu GetCurrentMenu()
        {
            var absolutePath = httpContextBase.Request.Url.AbsolutePath;
            if(absolutePath == "/Default.aspx")
            {
                absolutePath = "/";
            }

            var menu = work.MenuRepository.GetByPublicUrl(absolutePath);
            return menu;
        }

        public Menu GetMenu(string absolutePath)
        {
            var menu = work.MenuRepository.GetByPublicUrl(absolutePath);
            return menu;
        }
    }
}
