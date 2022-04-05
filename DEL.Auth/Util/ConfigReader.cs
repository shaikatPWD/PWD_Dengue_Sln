﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.Util
{
    public class ConfigReader
    {
        public static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key] ?? "";
        }
    }
}
