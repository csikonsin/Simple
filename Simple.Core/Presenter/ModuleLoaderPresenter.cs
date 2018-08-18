using Autofac;
using Autofac.Integration.Web;
using Simple.Core.Code.ModuleParameters;
using Simple.Core.Views;
using Simple.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Simple.Core.Presenter
{
    public interface IModuleLoaderView
    {
        ControlCollection ControlCollection { get; }
        string Identity { get; }
    }

    public class ModuleLoaderPresenter
    {
        public delegate ModuleLoaderPresenter Factory(IModuleLoaderView view);

        private IModuleLoaderView view;
        private readonly IMenuService menuService;
        private readonly HttpContextBase httpContext;
        private readonly Data.IUnitOfWork work;

        public ModuleLoaderPresenter(IModuleLoaderView view, HttpContextBase httpContext, IMenuService menuService, Data.IUnitOfWork work)
        {
            this.view = view ?? throw new ArgumentException("View must be set!");
            this.menuService = menuService;
            this.httpContext = httpContext;
            this.work = work;
        }

        public void Initialize()
        {
            LoadModules();
        }

        private void LoadModules()
        {
            var controls = GetControls();
            foreach (var cnt in controls)
            {
                view.ControlCollection.Add(cnt);
            }
        }

        private List<Control> GetControls()
        {
            var menuId = Convert.ToInt32(httpContext.Request.QueryString["menuId"]);

            var menu = work.MenuRepository.GetById(menuId);

            if (menu == null)
            {
                var lt404 = new LiteralControl() { Text = "404 Seite nicht gefunden" };
                return new List<Control> { lt404 };
            }

            var modules = work.ModuleRepository.GetAllByMenuId(menu.Id);
            modules = modules.OrderBy((x) =>
            {
                if (x.Position.HasValue)
                {
                    return x.Position.Value;
                }
                else
                {
                    return Int32.MaxValue;
                }
            }).ToList();

            var controls = new List<Control>();
            foreach (var module in modules)
            {
                if (!Code.CmsConfig.Instance.CmsModules.ContainsKey(module.ModuleId))
                {
                    controls.Add(new LiteralControl() { Text = $"Module with id={module.ModuleId} was not found!" });
                    continue;
                }
                var cmsModule = Code.CmsConfig.Instance.CmsModules[module.ModuleId];

                var p = HttpContext.Current.Handler as Page;
                var cmsControl = p.LoadControl(cmsModule.ControlPath);

                ((IContainerProviderAccessor)HttpContext.Current.ApplicationInstance)
                    .ContainerProvider
                    .RequestLifetime
                    .InjectProperties(cmsControl)
                ;

                var parameter = ParameterBuilder.Deserialize(module.Parameter, cmsModule.ParameterType);
                ((IBaseModule)cmsControl).Module = module;

                var wrapper = p.LoadControl("~/Views/BaseModuleWrapper.ascx");
                ((BaseModuleWrapper)wrapper).CmsModule = cmsModule;
                wrapper.FindControl("ph").Controls.Add(cmsControl);

                controls.Add(wrapper);
            }
            return controls;
        }
    }
}