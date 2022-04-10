using DEL.Auth.DTO;
using DEL.Auth.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WebUI.Areas.Auth.Models
{
    public class RequestHelper
    {
        public static Dictionary<string, string> GetFromQueryString()
        {
            var request = HttpContext.Current.Request;
            return request.QueryString.AllKeys.ToDictionary(key => key, key => request[key]);
        }
    }

    public class MenuWebModel
    {
        private readonly MenuFacade menuFacade;

        public MenuWebModel()
        {
            menuFacade = new MenuFacade();
        }

        public static string GetMenuJsonDataForFlatGrid(List<MenuDto> menuDtos)
        {
            var bldr = new StringBuilder("<?xml version='1.0' encoding='utf-8'?><rows>");
            foreach (var m in menuDtos)
            {
                bldr.Append(
                    string.Format(
                        "<row>" +
                        "<cell>{0}</cell>" +
                        "<cell>{1}</cell>" +
                        "<cell>/{2}</cell>" +
                        "<cell>/{3}</cell>" +
                        "<cell>/{4}</cell>" +
                        "<cell>/{5}</cell>" +
                        "<cell>/{6}</cell>" +
                        "<cell>/{7}</cell>" +
                        "</row>"
                        , m.Id, m.DisplayName, m.Url, 0, m.Sl, m.Sl, true, true));
            }
            bldr.Append("</rows>");
            return bldr.ToString();
        }
        public static string GetMenuXmlStringForTreeGrid(List<MenuDto> menuDtos)
        {
            var bldr = new StringBuilder("<?xml version='1.0' encoding='utf-8'?><rows>");
            foreach (var m in menuDtos)
            {
                bldr.Append(
                    string.Format(
                        "<row>" +
                        "<cell>{0}</cell>" +
                        "<cell>{1}</cell>" +
                        "<cell>/{2}</cell>" +
                        "<cell>/{3}</cell>" +
                        "<cell>/{4}</cell>" +
                        "<cell>/{5}</cell>" +
                        "<cell>/{6}</cell>" +
                        "<cell>/{7}</cell>" +
                        "</row>"
                        , m.Id, m.DisplayName, m.Url, 0, m.Sl, m.Sl, true, true));
            }
            bldr.Append("</rows>");
            return bldr.ToString();
        }
        public List<MenuStructure> GetModuleAndSubModules(string appName = "")
        {
            List<long?> AppList = new List<long?>();
            AppList.Add((long)SessionHelper.UserProfile.SelectedApplicationId);
            //get database menus according to appname

            var moduleList = SessionHelper.UserProfile.Modules;//menuFacade.GetModules(AppList);
            var subModuleList = SessionHelper.UserProfile.SubModules;//menuFacade.GetSubModules();
            var retVal = moduleList.Where(m => !string.IsNullOrEmpty(m.DisplayName)).Select(m => new MenuStructure
            {
                Id = m.Id,
                Name = m.Name,
                DisplayName = m.Name,
                Description = m.Description,
                Sl = m.Sl,
                ChildMenus = subModuleList.Where(x => x.ModuleId == m.Id).ToList().Select(s => new MenuStructure
                {
                    Id = s.Id,
                    ParentId = s.ModuleId,
                    Name = s.Name,
                    DisplayName = s.DisplayName,
                    Description = s.Description,
                    Sl = s.Sl
                }).ToList()
            }).ToList();

            return retVal;
        }
        public List<MenuStructure> GetModuleAndSubModulesAndMenu(string appName = "")
        {
            List<long?> AppList = new List<long?>();
            AppList.Add((long)SessionHelper.UserProfile.SelectedApplicationId);
            //get database menus according to appname

            var moduleList = SessionHelper.UserProfile.Modules;//menuFacade.GetModules(AppList);
            var subModuleList = SessionHelper.UserProfile.SubModules;//menuFacade.GetSubModules();
            var menuList = SessionHelper.UserProfile.Menus;//menuFacade.GetSubModules();
            var retVal = moduleList.Where(m => !string.IsNullOrEmpty(m.DisplayName)).Select(m => new MenuStructure
            {
                Id = m.Id,
                Name = m.Name,
                DisplayName = m.Name,
                Description = m.Description,
                Sl = m.Sl,
                ChildMenus = subModuleList.Where(x => x.ModuleId == m.Id).ToList().Select(s => new MenuStructure
                {
                    Id = s.Id,
                    ParentId = s.ModuleId,
                    Name = s.Name,
                    DisplayName = s.DisplayName,
                    Description = s.Description,
                    Sl = s.Sl,
                    ChildMenusM = menuList.Where(y => y.SubModuleId == s.Id).ToList().Select(t => new MenuStructure
                    {
                        Id = t.Id,
                        ParentId = t.SubModuleId,
                        Name = t.Name,
                        DisplayName = t.DisplayName,
                        Description = t.Description,
                        Sl = t.Sl,
                        Url = t.Url
                    }).ToList()
                }).ToList()
            }).ToList();

            return retVal;
        }

        public List<MenuStructure> GetOnlyMenus(string appName = "")
        {
            List<long?> AppList = new List<long?>();
            AppList.Add((long)SessionHelper.UserProfile.SelectedApplicationId);
            //get database menus according to appname

            var moduleList = SessionHelper.UserProfile.Modules;//menuFacade.GetModules(AppList);
            var subModuleList = SessionHelper.UserProfile.SubModules;//menuFacade.GetSubModules();
            var menuList = SessionHelper.UserProfile.Menus;//menuFacade.GetSubModules();
                                                           //var retVal =  List<null>;

            var retVal = menuList.Where(y => y.SubModuleId == 4).ToList().Select(t => new MenuStructure
            {
                Id = t.Id,
                ParentId = t.SubModuleId,
                Name = t.Name,
                DisplayName = t.DisplayName,
                Description = t.Description,
                Sl = t.Sl,
                Url = t.Url
                ////ChildMenus = subModuleList.Where(x => x.ModuleId == m.Id).ToList().Select(s => new MenuStructure
                ////{
                ////    Id = s.Id,
                ////    ParentId = s.ModuleId,
                ////    Name = s.Name,
                ////    DisplayName = s.DisplayName,
                ////    Description = s.Description,
                ////    Sl = s.Sl,
                //    ChildMenusM = menuList.Where(y => y.SubModuleId == 10054).ToList().Select(t => new MenuStructure
                //    {
                //        Id = t.Id,
                //        ParentId = t.SubModuleId,
                //        Name = t.Name,
                //        DisplayName = t.DisplayName,
                //        Description = t.Description,
                //        Sl = t.Sl,
                //        Url = t.Url
                //    }).ToList()
                //}).ToList()
            }).ToList();
            var retValAdmin = menuList.ToList().Select(t => new MenuStructure
            {
                Id = t.Id,
                ParentId = t.SubModuleId,
                Name = t.Name,
                DisplayName = t.DisplayName,
                Description = t.Description,
                Sl = t.Sl,
                Url = t.Url
                ////ChildMenus = subModuleList.Where(x => x.ModuleId == m.Id).ToList().Select(s => new MenuStructure
                ////{
                ////    Id = s.Id,
                ////    ParentId = s.ModuleId,
                ////    Name = s.Name,
                ////    DisplayName = s.DisplayName,
                ////    Description = s.Description,
                ////    Sl = s.Sl,
                //    ChildMenusM = menuList.Where(y => y.SubModuleId == 10054).ToList().Select(t => new MenuStructure
                //    {
                //        Id = t.Id,
                //        ParentId = t.SubModuleId,
                //        Name = t.Name,
                //        DisplayName = t.DisplayName,
                //        Description = t.Description,
                //        Sl = t.Sl,
                //        Url = t.Url
                //    }).ToList()
                //}).ToList()
            }).ToList();
            if (SessionHelper.UserProfile.UserName.ToLower() == "admin")
            {

            }
            return (SessionHelper.UserProfile.UserName.ToLower() != "admin" ? retVal : retValAdmin);
        }
    }

    public class MenuStructure
    {
        public MenuStructure()
        {
            ChildMenus = new List<MenuStructure>();
            ChildMenusM = new List<MenuStructure>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public int Sl { get; set; }
        public long ParentId { get; set; }
        public string Url { get; set; }
        public List<MenuStructure> ChildMenus { get; set; }
        public List<MenuStructure> ChildMenusM { get; set; }
    }
}