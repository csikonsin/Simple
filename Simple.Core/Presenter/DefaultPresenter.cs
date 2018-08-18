using Simple.Core.Code;
using Simple.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Simple.Core.Presenter
{
    public interface IDefaultView
    {
        ControlCollection ControlCollection { get; }
        string CssBundlePath { get; set; }
        string PageTitle { get; set; }
    }

    public class DefaultPresenter
    {
        public delegate DefaultPresenter Factory(IDefaultView view);

        private readonly IMenuService menuService;
        private readonly IDefaultView view;
        private readonly ResourceLoader loader;
        public DefaultPresenter(IDefaultView view, ResourceLoader loader, IMenuService menuService)
        {
            this.view = view;
            this.loader = loader;
            this.menuService = menuService;
        }

        public void Initialize()
        {
            var defaultControl = loader.LoadDefaultPage();
            view.ControlCollection.Add(defaultControl);
            view.CssBundlePath = loader.GetStyleBundleVirtualPath();

            var menu = menuService.GetCurrentMenu();

            view.PageTitle = menu.Title;
        }
    }
}