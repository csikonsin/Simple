﻿using Autofac;
using Simple.Core.Code.ModuleParameters;
using Simple.Core.Presenter;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;

namespace Simple.Core.Views
{
    public partial class Module : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            var modules = new List<Domain.Module>();
            int b = 5;
            b++;

            modules.Add(new Domain.Module()
            {
                Id = 1,
                Parameter = ParameterBuilder.Serialize(new ArticlesOverviewParameter()
                {
                    BackgroundColorHex = "#cc0000"
                }),
                ModuleId = 1,
                CreatedAt = DateTime.Now,
                CreatedById = 1
            });


            foreach (var module in modules)
            {
                var c = (BaseControl)((UserControl)LoadControl("~/Views/articles.ascx"));
                c.Module = module;

                //Manually Inject Properties
                var cpa = (Autofac.Integration.Web.IContainerProviderAccessor)HttpContext.Current.ApplicationInstance;
                var cp = cpa.ContainerProvider;
                cp.RequestLifetime.InjectProperties(c);

                ph.Controls.Add(c);
            }
        }
    }
}