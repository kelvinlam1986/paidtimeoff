using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using V2.PaidTimeOffBLL.Framework;

namespace PaidTimeOffUI
{
    public abstract class BasePage : System.Web.UI.Page
    {
        public BasePage()
        {
        }

        #region Properities
      

        #endregion Properties

        public abstract string MenuItemName();

        public static string RootPath(HttpContext context)
        {
            string urlSuffix = context.Request.Url.Authority +
                context.Request.ApplicationPath;
            return context.Request.Url.Scheme + @"://" + urlSuffix + "/";
        }

        public static NameValueCollection DecryptQueryString(string queryString)
        {
            return StringHelpers.DecryptQueryString(queryString);
        }
        public static string EncryptQueryString(NameValueCollection queryString)
        {
            return StringHelpers.EncryptQueryString(queryString);
        }
        public static string EncryptQueryString(string queryString)
        {
            return StringHelpers.EncryptQueryString(queryString);
        }
    }
}