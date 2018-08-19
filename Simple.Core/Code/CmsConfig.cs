using Simple.Core.Code.ModuleParameters;
using System.Collections.Generic;

namespace Simple.Core.Code
{
    public sealed class CmsConfig
    {
        private static CmsConfig instance = null;
        private static readonly object padlock = new object();

        public CmsConfig()
        {
            InitModules();
        }

        public static CmsConfig Instance
        {
            get
            {
                if(instance == null)
                {
                    lock (padlock)
                    {
                        if(instance == null)
                        {
                            instance = new CmsConfig();
                        }
                    }
                }
                return instance;
            }
        }

        public Dictionary<int, CmsModule> CmsModules { get;  set; }

        public void InitModules()
        {
            if (CmsModules != null || CmsModules?.Count > 0) return;

#pragma warning disable IDE0028 // Initialisierung der Sammlung vereinfachen
            CmsModules = new Dictionary<int, CmsModule>();
#pragma warning restore IDE0028 // Initialisierung der Sammlung vereinfachen

            CmsModules.Add(1, new CmsModule()
            {
                ControlPath = "~/Views/Article/Article.ascx",
                EditorPath = "~/Views/Article/edit_article.aspx",
                ParameterType = typeof(ArticleParameter),
                CssClass = "article"
            });

            CmsModules.Add(2, new CmsModule()
            {
                ControlPath ="~/Views/Articles/Articles.ascx",
                EditorPath = "~/Views/Articles/edit_articles.aspx",
                ParameterType = typeof(ArticlesOverviewParameter),
                CssClass = "articles"
            });

        }
    }
}