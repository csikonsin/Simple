﻿using Simple.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Simple.Core.Views
{
    public interface IModuleLoaderView
    {
        ControlCollection ControlCollection { get; }
    }

    public class ModuleLoaderPresenter
    {
        public delegate ModuleLoaderPresenter Factory(IModuleLoaderView view);

        private IModuleLoaderView view;
        private readonly IMenuService menuService;
        private readonly HttpContextBase httpContext;

        public ModuleLoaderPresenter(IModuleLoaderView view, HttpContextBase httpContext, IMenuService menuService)
        {
            this.view = view ?? throw new ArgumentException("View must be set!");
            this.menuService = menuService;
            this.httpContext = httpContext;
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
            Domain.Menu menu = menuService.GetCurrentMenu(HttpContext.Current.Request.Url.AbsolutePath);

            if (menu == null)
            {
                var lt404 = new LiteralControl() { Text = "404 Seite nicht gefunden" };
                return new List<Control> { lt404 };
            }

            int menuId = menu.Id;

            List<Domain.Module> modules;
            using (var work = new Data.UnitOfWork())
            {
                modules = work.ModuleRepository.GetAllByMenuId(menuId);
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
            }

            var controls = new List<Control>();


            foreach (var module in modules)
            {
                if (!Code.CmsConfig.CmsModules.ContainsKey(module.ModuleId))
                {
                    controls.Add(new LiteralControl() { Text = $"Module with id={module.ModuleId} was not found!" });
                    continue;
                }

                var cmsModule = Code.CmsConfig.CmsModules[module.ModuleId];

                var p = HttpContext.Current.Handler as Page;
                var cmsControl = p.LoadControl(cmsModule.ControlPath);
                var parameter =   ParameterBuilder.Deserialize(module.Parameter, cmsModule.ParameterType);
                ((IBaseModule)cmsControl).SetParameter(parameter);

                var wrapper = p.LoadControl("~/Modules/BaseModuleWrapper.ascx");
                ((BaseModuleWrapper)wrapper).CmsModule = cmsModule;
                wrapper.FindControl("ph").Controls.Add(cmsControl);
             
                controls.Add(wrapper);
            }

            return controls;
        }
    }
}