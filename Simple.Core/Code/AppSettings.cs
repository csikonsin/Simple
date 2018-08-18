﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Simple.Core.Code
{
    public interface IAppSettings
    {
        int WebsiteId { get; }
    }

    public class AppSettings : IAppSettings
    {
        public AppSettings()
        {
        }

        public int WebsiteId => Convert.ToInt32(ConfigurationManager.AppSettings["WebsiteId"]);
    }
}