using System;
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

        public Dictionary<int, CmsModule> CmsModules { get; protected set; }

        public void InitModules()
        {
            if (CmsModules != null || CmsModules?.Count > 0) return;

            CmsModules = new Dictionary<int, CmsModule>();

            CmsModules.Add(1, new CmsModule()
            {
                ControlPath = "~/Views/Article.ascx",
                EditorPath = "~/Modules/edit_article.aspx",
                ParameterType = typeof(Presenter.ArticleParameter),
                CssClass = "article"
            });

        }
    }

    public class CmsModule
    {
        public int ModuleId { get; set; }
        public string ControlPath { get; set; }
        public string EditorPath { get; set; }
        public Type ParameterType { get; set; }
        public string CssClass { get; set; }
    }
}