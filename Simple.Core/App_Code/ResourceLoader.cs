using Autofac;
using Autofac.Integration.Web;
using Simple.Data;
using System.Web;
using System.Web.UI;

namespace Simple.Core.Code
{
    public class ResourceLoader
    {
        private readonly Service.IWebsiteService websiteService;
        private readonly AppSettings appSettings;
        private readonly IUnitOfWork work;

        public ResourceLoader(AppSettings appSettings, Service.IWebsiteService websiteService, IUnitOfWork work)
        {
            this.websiteService = websiteService;
            this.appSettings = appSettings;
            this.work = work;
        }

        public string GetTheme()
        {
            var website = work.WebsiteRepository.GetById(appSettings.WebsiteId);
            if (website == null) throw new System.Exception("No website data foundd!");
            return website.Theme;
        }

        public string GetStyleBundleVirtualPath()
        {
            var theme = GetTheme();
            var path = $"~/Content/css/{theme}/bundle";
            return path;
        }


        public Control LoadDefaultPage()
        {
            var theme = GetTheme();
            if (!(HttpContext.Current.Handler is Page page)) return null;

            var defaultControl = page.LoadControl($"~/Content/{theme}/Default.ascx");
            var cp = ((IContainerProviderAccessor)HttpContext.Current.ApplicationInstance).ContainerProvider;
            foreach (var c in defaultControl.Controls)
            {
                cp.RequestLifetime.InjectProperties(c);
            }

            return defaultControl;
        }
    }
}