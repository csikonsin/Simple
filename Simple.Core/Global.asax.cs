using Autofac;
using Autofac.Builder;
using Autofac.Integration.Web;
using Simple.Core.Presenter;
using Simple.Data;
using Simple.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Simple.Core
{

    public class Global : HttpApplication, IContainerProviderAccessor
    {
        static IContainerProvider _containerProvider;
        public IContainerProvider ContainerProvider
        {
            get { return _containerProvider; }
        }


        void Application_Start(object sender, EventArgs e)
        {
            Data.Configuration.Initialize();

            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Build up your application container and register your dependencies.
            var builder = new ContainerBuilder();

            //builder.RegisterType<Views.Presenter>();
            //builder.RegisterGeneratedFactory<PresenterFactory>();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<ArticleService>().As<IArticleService>();
            builder.RegisterType<ArticleOverviewPresenter>().AsSelf();
            builder.RegisterGeneratedFactory<ArticleOverviewPresenter.Factory>();

            // Once you're done registering things, set the container
            // provider up with your registrations.
            _containerProvider = new ContainerProvider(builder.Build());
        }
    }
}