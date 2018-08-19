using Simple.Service;
using System;
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
        public Domain.Module Module { get; set; }
        public IUserService UserService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var classes = new List<string>() { "module", CmsModule.CssClass };

            pnModule.CssClass = String.Join(" ", classes);

            pnModule.ID = "";

            if (UserService.IsAdmin())
            {
                admin.Visible = true;

                addModule.HRef = "javascript:void(0);";
                addModule.Attributes.Add("onclick", $"popupwindow('{CmsModule.EditorPath.Replace("~", "")}?moduleId={Module.Id}','Module settings', {CmsModule.EditorWidth},{CmsModule.EditorHeight})");

            }            
        }
    }
}