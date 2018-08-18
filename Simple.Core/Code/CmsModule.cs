using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simple.Core.Code
{
    public class CmsModule
    {
        public int ModuleId { get; set; }
        public string ControlPath { get; set; }
        public string EditorPath { get; set; }
        public Type ParameterType { get; set; }
        public string CssClass { get; set; }
    }
}