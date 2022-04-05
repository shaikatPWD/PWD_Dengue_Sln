﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using DEL.Auth.Infrastructure;
using DEL.Auth.Service;
using System.Data.SqlClient;
using System.Web;
using System.IO;
using DEL.Auth.DTO;

namespace DEL.Auth.Facade
{
    public abstract class BaseFacade
    {
        private readonly GenService genService;
        public JqGridModel JqGridModel { get; set; }
        protected BaseFacade()
        {
            genService = new GenService();
        }

        public GenService GenService
        {
            get { return genService; }
        }

        #region Apply jqgFilterOnDTO
        protected IEnumerable<T> ApplyJqFilter<T>(IEnumerable<T> data, Dictionary<string, string> filterColumnModelNames = null) where T : class
        {
            if (JqGridModel == null)
                return data;

            if (filterColumnModelNames != null && filterColumnModelNames.Any() && JqGridModel.filters != null && JqGridModel.filters.rules.Any())
            {
                var rules = JqGridModel.filters.rules;
                foreach (var f in filterColumnModelNames)
                {
                    for (var i = 0; i < rules.Count; i++)
                    {
                        if (rules[i].field == f.Key)
                        {
                            rules[i].field = f.Value;
                            break;
                        }
                    }
                }
            }
            var t = typeof(T);
            var pInfoList = t.GetProperties().ToList();

            data = ExecuteJqFilterRules(data, pInfoList, t);
            data = ExecuteJqSort(data, pInfoList, t);
            data = ExecuteJqPaging(data);
            return data;
        }
        private IEnumerable<T> ExecuteJqFilterRules<T>(IEnumerable<T> data, List<PropertyInfo> pInfoList, Type entityType) where T : class
        {

            if (!JqGridModel._search || JqGridModel.filters == null || JqGridModel.filters.rules.Count <= 0)
                return data;
            var qData = data.AsQueryable();
            var expressionList = new List<Expression>();
            //ref: http://stackoverflow.com/questions/7246715/use-reflection-to-get-lambda-expression-from-property-name
            foreach (var r in JqGridModel.filters.rules)
            {
                var p = pInfoList.FirstOrDefault(x => x.Name == r.field) ?? pInfoList.Single(x => x.Name == "Id");
                var parameter = Expression.Parameter(entityType, "x");
                var property = Expression.Property(parameter, p.Name);
                var target = Expression.Constant(r.data);
                switch (r.op)
                {
                    case "cn":
                        var containsMethod = Expression.Call(property, "Contains", null, target);
                        expressionList.Add(Expression.Lambda<Func<T, bool>>(containsMethod, parameter));
                        break;

                    case "eq":
                        var equalsMethod = Expression.Call(property, "Equals", null, target);
                        //http://stackoverflow.com/questions/15722880/build-expression-equals-on-string    
                        //MethodInfo equalsMethod = typeof(string).GetMethod("Equals", new[] { typeof(string) });
                        expressionList.Add(Expression.Lambda<Func<T, bool>>(equalsMethod, parameter));
                        break;

                    default:
                        break;
                }
            }
            if (JqGridModel.filters.groupOp.ToLower() == "and")
            {
                qData = expressionList.Aggregate(qData, (current, e) =>
                    current.Where((Expression<Func<T, bool>>)e));
            }
            else
            {

            }
            return qData.ToList();
        }
        private IEnumerable<T> ExecuteJqSort<T>(IEnumerable<T> data, List<PropertyInfo> pInfoList, Type entityType) where T : class
        {

            //ref:http://stackoverflow.com/questions/307512/how-do-i-apply-orderby-on-an-iqueryable-using-a-string-column-name-within-a-gene

            if (string.IsNullOrWhiteSpace(JqGridModel.sidx) || string.IsNullOrWhiteSpace(JqGridModel.sord))
            {
                //data = data.OrderBy(x => x.Id);
                return data;
            }

            var qData = data.AsQueryable();

            //var property = pInfoList.Single(x => x.Name == JqGridModel.sidx);
            var property = pInfoList.FirstOrDefault(x => x.Name == JqGridModel.sidx) ??
                           pInfoList.Single(x => x.Name == "Id");  // for temporary solution; it should traverse nav prop.

            var parameter = Expression.Parameter(entityType, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            var sDir = JqGridModel.sord.ToLower() == "asc" ? "OrderBy" : "OrderByDescending";
            
            var resultExp = Expression.Call(typeof(Queryable), sDir,
                new Type[] { entityType, property.PropertyType }, qData.Expression,
                Expression.Quote(orderByExp));
            return qData.Provider.CreateQuery<T>(resultExp).ToList();
        }
        private IEnumerable<T> ExecuteJqPaging<T>(IEnumerable<T> data) where T : class 
        {
            var row2Skip = (JqGridModel.pages) * JqGridModel.rows;
            row2Skip = row2Skip <= 0 ? 0 : row2Skip;
            var row2Take = JqGridModel.rows;
            return data.Skip(row2Skip).Take(row2Take);
        }

        #endregion
        #region Apply jqgFilterOnDB
        /*
        protected IQueryable<T> ApplyJqFilter<T>(IQueryable<T> data, Dictionary<string, string> filterColumnModelNames = null) where T : Entity
        {
            if (JqGridModel == null)
                return data;

            if (filterColumnModelNames != null && filterColumnModelNames.Any() && JqGridModel.filters != null && JqGridModel.filters.rules.Any())
            {
                var rules = JqGridModel.filters.rules;
                foreach (var f in filterColumnModelNames)
                {
                    for (var i = 0; i < rules.Count; i++)
                    {
                        if (rules[i].field == f.Key)
                        {
                            rules[i].field = f.Value;
                            break;
                        }
                    }
                }
            }
            var t = typeof(T);
            var pInfoList = t.GetProperties().ToList();

            data = ExecuteJqFilterRules(data, pInfoList, t);
            data = ExecuteJqSort(data, pInfoList, t);
            data = ExecuteJqPaging(data, pInfoList, t);
            return data;
        }
        private IQueryable<T> ExecuteJqSort<T>(IQueryable<T> data, List<PropertyInfo> pInfoList, Type entityType) where T : Entity
        {

            //ref:http://stackoverflow.com/questions/307512/how-do-i-apply-orderby-on-an-iqueryable-using-a-string-column-name-within-a-gene

            if (string.IsNullOrWhiteSpace(JqGridModel.sidx) || string.IsNullOrWhiteSpace(JqGridModel.sord))
            {
                data = data.OrderBy(x => x.Id);
                return data;
            }
            //var property = pInfoList.Single(x => x.Name == JqGridModel.sidx);
            var property = pInfoList.FirstOrDefault(x => x.Name == JqGridModel.sidx) ??
                           pInfoList.Single(x => x.Name == "Id");  // for temporary solution; it should traverse nav prop.

            var parameter = Expression.Parameter(entityType, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            var sDir = JqGridModel.sord.ToLower() == "asc" ? "OrderBy" : "OrderByDescending";
            var resultExp = Expression.Call(typeof(Queryable), sDir,
                new Type[] { entityType, property.PropertyType }, data.Expression,
                Expression.Quote(orderByExp));
            return data.Provider.CreateQuery<T>(resultExp);
        }
        private IQueryable<T> ExecuteJqPaging<T>(IQueryable<T> data, List<PropertyInfo> pInfoList, Type entityType) where T : Entity
        {
            var row2Skip = (JqGridModel.pages) * JqGridModel.rows;
            row2Skip = row2Skip <= 0 ? 0 : row2Skip;
            var row2Take = JqGridModel.rows;
            return data.Skip(row2Skip).Take(row2Take);
        }
        private IQueryable<T> ExecuteJqFilterRules<T>(IQueryable<T> data, List<PropertyInfo> pInfoList, Type entityType) where T : Entity
        {

            if (!JqGridModel._search || JqGridModel.filters == null || JqGridModel.filters.rules.Count <= 0)
                return data;

            var expressionList = new List<Expression>();
            //ref: http://stackoverflow.com/questions/7246715/use-reflection-to-get-lambda-expression-from-property-name
            foreach (var r in JqGridModel.filters.rules)
            {
                var p = pInfoList.FirstOrDefault(x => x.Name == r.field) ?? pInfoList.Single(x => x.Name == "Id");
                var parameter = Expression.Parameter(entityType, "x");
                var property = Expression.Property(parameter, p.Name);
                var target = Expression.Constant(r.data);
                switch (r.op)
                {
                    case "cn":
                        var containsMethod = Expression.Call(property, "Contains", null, target);
                        expressionList.Add(Expression.Lambda<Func<T, bool>>(containsMethod, parameter));
                        break;

                    case "eq":
                        var equalsMethod = Expression.Call(property, "Equals", null, target);
                        //http://stackoverflow.com/questions/15722880/build-expression-equals-on-string    
                        //MethodInfo equalsMethod = typeof(string).GetMethod("Equals", new[] { typeof(string) });
                        expressionList.Add(Expression.Lambda<Func<T, bool>>(equalsMethod, parameter));
                        break;

                    default:
                        break;
                }
            }
            if (JqGridModel.filters.groupOp.ToLower() == "and")
            {
                data = expressionList.Aggregate(data, (current, e) =>
                    current.Where((Expression<Func<T, bool>>)e));
            }
            else
            {

            }
            return data;
        }
        */
        #endregion

        public bool DBBackupAuth()
        {
            string conString = System.Configuration.ConfigurationManager.ConnectionStrings["DELAuthContext"].ToString();
            SqlConnection conn = new SqlConnection(conString);
            string filePath = "~/DBBackup/";
            string path = HttpContext.Current.Server.MapPath(filePath);
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = Path.Combine(path, "AuthDB_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".bak");
                string query = "backup database [DEL.Southern.Auth.Suite] to disk='" + path + "'";
                //SqlDataAdapter da = new SqlDataAdapter
                conn.Open();
                SqlCommand comm = new SqlCommand(query, conn);
                comm.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
            }


            return false;
        }
        public ResponseDto CreateDBBackup()
        {
            var response = new ResponseDto();
            response.Success = true;
            response.Message = "";
            var successList = new List<string>();
            var failList = new List<string>();
            var configs = genService.GetAll<DbBackupConfig>().Where(d=>d.Status == EntityStatus.Active);
            foreach (var config in configs)
            {
                string conString = System.Configuration.ConfigurationManager.ConnectionStrings[config.DBContextName].ToString();
                SqlConnection conn = new SqlConnection(conString);
                string filePath = "~/" + config.FileDirectory;
                string path = HttpContext.Current.Server.MapPath(filePath);
                try
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    path = Path.Combine(path, config.FilePrefix + "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".bak");
                    string query = "backup database [" + config.DBName + "] to disk='" + path + "'";
                    conn.Open();
                    SqlCommand comm = new SqlCommand(query, conn);
                    comm.ExecuteNonQuery();
                    conn.Close();
                    successList.Add(config.DBName);
                }
                catch (Exception ex)
                {
                    failList.Add(config.DBName);
                }                
            }
            
            if (successList.Count == 1)
                response.Message = "Database backup has been created for " + successList.First() + ".";
            else if(successList.Count > 1)
            {
                response.Message = "Database backup has been created for ";
                var i = 0;
                foreach(var successful in successList)
                {
                    if(i++ > 0)
                        response.Message += ", ";

                    response.Message += successful;
                }
                response.Message += ". ";
            }
            if (failList.Count == 1)
                response.Message = "Database backup failed for " + failList.First() + ".";
            else if (failList.Count > 1)
            {
                response.Message = "Database backup failed for ";
                var i = 0;
                foreach (var fail in failList)
                {
                    if (i++ > 0)
                        response.Message += ", ";

                    response.Message += fail;
                }
                response.Message += ". ";
            }

            return response;
        }
    }
}
