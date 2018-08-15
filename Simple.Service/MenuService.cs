using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Domain;

namespace Simple.Service
{
    public interface IMenuService
    {
        Menu GetCurrentMenu(string absolutePath);
    }
    public class MenuService : IMenuService
    {
        public Menu GetCurrentMenu(string absolutePath)
        {
            throw new NotImplementedException();
        }
    }
}
