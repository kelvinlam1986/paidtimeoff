using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using V2.PaidTimeOffBLL.Framework;

namespace V2.FrameworkControls
{
    [ToolboxData("<{0}:MenuTabs runat=server></{0}:MenuTabs>")]
    public class MenuTabs : WebControl
    {
        [Browsable(false)]
        public ENTMenuItemBOList MenuItems { get; set; }

        [Browsable(false)]
        public string CurrentMenuItemName { get; set; }

        [Browsable(true)]
        [DefaultValue("Enter Application Root Path")]
        [Description("Enter the root path for your application.  This is used to determine the path for all items in the menu.")]
        public string RootPath { get; set; }

        [Browsable(false)]
        public ENTUserAccountEO UserAccount { get; set; }

        [Browsable(false)]
        public ENTRoleEOList Roles { get; set; }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            base.RenderContents(writer);

            string html;

            // Get the parent menu item for the current menu item.  The parent will be 
            // the one with a null ParentMenuItemId
            if (MenuItems != null)
            {
                ENTMenuItemBO topMenuItem = MenuItems.GetTopMenuItem(CurrentMenuItemName);
                html = "<ul class=\"glossymenu\">";

                // Loop around the top level items
                foreach (var mi in MenuItems)
                {
                    if (mi.HasAccessToMenu(UserAccount, Roles))
                    {
                        // Check if this is the selected menu tab.
                        if (mi.MenuItemName == topMenuItem.MenuItemName)
                        {
                            html += GetActiveTab(mi);
                        }
                        else
                        {
                            html += GetInactiveTab(mi);
                        }
                    }
                }

                html += "</ul>";
            }
            else
            {
                html = "<div>Top Menu Goes Here</div>";
            }

            writer.Write(html);
        }

        private string GetInactiveTab(ENTMenuItemBO subMenu)
        {
            return "<li><a href=\"" + RootPath + subMenu.Url + "\"><b>" + subMenu.MenuItemName + "</b></a></li>";
        }

        private string GetActiveTab(ENTMenuItemBO subMenu)
        {
            return "<li class=\"current\"><a href=\"" + RootPath + subMenu.Url + "\"><b>" + subMenu.MenuItemName + "</b></a></li>";
        }
    }
}
