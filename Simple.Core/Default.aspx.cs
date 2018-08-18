﻿using Simple.Core.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Simple.Core
{
    public partial class _Default : Page, IDefaultView
    {
        public ControlCollection ControlCollection
        {
            get
            {
                return ph.Controls;
            }
        }

        public DefaultPresenter.Factory Factory { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var presenter = Factory(this);
            presenter.Initialize();
        }
    }
}