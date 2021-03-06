﻿using System;
using System.Configuration;
using System.Web.UI;
using V2.PaidTimeOffBLL.Framework;

namespace PaidTimeOffUI
{
    public partial class PaidTimeOff : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // ENTUserAccountEO currentUser = ((BasePage)Page).CurrentUser;

            //Set the top menu properties
            MenuTabs1.MenuItems = Globals.GetMenuItems(this.Cache);
            MenuTabs1.RootPath = BasePage.RootPath(Context);
            MenuTabs1.CurrentMenuItemName = ((BasePage)Page).MenuItemName();
            var currentUser = ((BasePage)Page).CurrentUser;
            MenuTabs1.UserAccount = currentUser;
            MenuTabs1.Roles = Globals.GetRoles(this.Cache);

            //Set the side menu properties
            MenuTree1.MenuItems = Globals.GetMenuItems(this.Cache);
            MenuTree1.RootPath = BasePage.RootPath(Context);
            MenuTree1.CurrentMenuItemName = ((BasePage)Page).MenuItemName();
            MenuTree1.UserAccount = currentUser;
            MenuTree1.Roles = Globals.GetRoles(this.Cache);

            lblCurrentUser.Text = Page.User.Identity.Name;
            lblCurrentDateTime.Text = DateTime.Now.ToString();

            //Set the version
            lblVersion.Text = ConfigurationManager.AppSettings["version"].ToString();
        }
    }
}