﻿using DEL.Auth.Infrastructure;
using DEL.Auth.Facade;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class BaseController : Controller
    {
        public JqGridModel jqGridModel { get; set; }
        private ILog _logger;

        private ILog Logger
        {
            get { return _logger ?? (_logger = LogManager.GetLogger(GetType())); }
        }

        protected void LogError(string message)
        {
            Logger.Error(message);
        }

        protected void LogInfo(string message)
        {
            Logger.Info(message);
        }

        protected void LogWarning(string message)
        {
            Logger.Warn(message);
        }

        protected void LogFatarError(string message)
        {
            Logger.Fatal(message);
        }

        public void ResolveJqFilterData(BaseFacade facade)
        {
            jqGridModel = System.Web.HttpContext.Current.Items["_jqgfilterdata_"] as JqGridModel;
            facade.JqGridModel = jqGridModel;
        }
    }
}