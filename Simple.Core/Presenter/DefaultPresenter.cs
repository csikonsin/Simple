using Simple.Core.Code;
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
    }

    public class DefaultPresenter
    {
        public delegate DefaultPresenter Factory(IDefaultView view);

        private readonly IDefaultView view;
        private readonly ResourceLoader loader;
        public DefaultPresenter(IDefaultView view, ResourceLoader loader)
        {
            this.view = view;
            this.loader = loader;
        }

        public void Initialize()
        {
            var defaultControl = loader.LoadDefaultPage();
        }
    }
}