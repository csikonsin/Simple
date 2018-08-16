﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Simple.Core.Views
{



    public partial class BaseModuleWrapper : System.Web.UI.UserControl
    {
        public Simple.Core.Code.CmsModule CmsModule { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            var classes = new List<string>() { "module", CmsModule.CssClass };

            pnModule.CssClass = String.Join(" ", classes);

            pnModule.ID = "";
        }
    }
}