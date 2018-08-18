using Autofac;
using Autofac.Builder;
using Autofac.Integration.Web;
using Simple.Core.Code;
using Simple.Core.Presenter;
using Simple.Data;
using Simple.Service;
using Simple.Services;
using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

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
       
            // Build up your application container and register your dependencies.
            var builder = new ContainerBuilder();

            builder.Register(c => c.Resolve<HttpContextBase>().Request)
                 .As<HttpRequestBase>()
                 .InstancePerRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                 .As<HttpResponseBase>()
                 .InstancePerRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                 .As<HttpServerUtilityBase>()
                 .InstancePerRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                 .As<HttpSessionStateBase>()
                 .InstancePerRequest();
            builder.Register(c => new HttpContextWrapper(HttpContext.Current))
                .As<HttpContextBase>()
                .InstancePerRequest();

            builder.RegisterType<WebsiteService>().As<IWebsiteService>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<ArticleService>().As<IArticleService>().InstancePerRequest();
            builder.RegisterType<MenuService>().As<IMenuService>().InstancePerRequest();

            builder.RegisterType<AppSettings>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ResourceLoader>().AsSelf();
            builder.RegisterType<ArticleOverviewPresenter>().AsSelf();
            builder.RegisterGeneratedFactory<ArticleOverviewPresenter.Factory>();
            builder.RegisterType<DefaultPresenter>().AsSelf();
            builder.RegisterGeneratedFactory<DefaultPresenter.Factory>();
            builder.RegisterGeneratedFactory<ModuleLoaderPresenter.Factory>();
            builder.RegisterType<ModuleLoaderPresenter>().AsSelf();

            builder.RegisterGeneratedFactory<ArticlePresenter.Factory>();
            builder.RegisterType<ArticlePresenter>().AsSelf();

            builder.RegisterType<BundleConfig>().AsSelf().InstancePerLifetimeScope();

            // Once you're done registering things, set the container
            // provider up with your registrations.
            _containerProvider = new ContainerProvider(builder.Build());

            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);            
            ContainerProvider.RequestLifetime.Resolve<BundleConfig>().RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var requestUri = HttpContext.Current.Request.Url;

            if (requestUri.AbsolutePath.Contains("browserLink")) return;

            var menuService = ContainerProvider.RequestLifetime.Resolve<IMenuService>();
            var menu = menuService.GetMenu(requestUri.AbsolutePath);
            if (menu == null) return;

            Context.RewritePath($"Default.aspx?menuid={menu.Id}");
        }
    }
}