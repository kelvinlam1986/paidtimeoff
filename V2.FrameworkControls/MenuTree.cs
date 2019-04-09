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
    [ToolboxData("<{0}:MenuTree runat=server></{0}:MenuTree>")]
    public class MenuTree : WebControl
    {
        [Browsable(false)]
        public ENTMenuItemBOList MenuItems { get; set; }

        [Browsable(false)]
        public string CurrentMenuItemName { get; set; }

        [Browsable(true)]
        [DefaultValue("Enter Application Root Path")]
        [Description("Enter the root path for your application.  This is used to determine the path for all items in the menu.")]
        public string RootPath { get; set; }

        protected override void CreateChildControls()
        {
            TreeView menuControl = new TreeView();
            menuControl.SelectedNodeStyle.CssClass = "selectedMenuItem";
            menuControl.ID = "tvSideMenu";
            menuControl.NodeWrap = true;

            // Find the top most menu item. This is the tab at the top.
            var topMenuItem = MenuItems.GetTopMenuItem(CurrentMenuItemName);
            CreateChildMenu(menuControl.Nodes, topMenuItem.ChildMenuList);
            Controls.Add(menuControl);
            base.CreateChildControls();
        }

        private void CreateChildMenu(TreeNodeCollection nodes, ENTMenuItemBOList menuItems)
        {
            foreach (var mi in menuItems)
            {
                TreeNode menuNode = new TreeNode(mi.MenuItemName, mi.ID.ToString(),
                    "", (string.IsNullOrEmpty(mi.Url) ? "" : RootPath + mi.Url), "");
                if (string.IsNullOrEmpty(mi.Url))
                {
                    menuNode.SelectAction = TreeNodeSelectAction.None;
                }

                // Check if this is the menu item that should be selected
                if (mi.MenuItemName == CurrentMenuItemName)
                {
                    menuNode.Selected = true;
                }

                // Check if this has children
                if (mi.ChildMenuList.Count > 0)
                {
                    // Create items for the children
                    CreateChildMenu(menuNode.ChildNodes, mi.ChildMenuList);
                }

                nodes.Add(menuNode);
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            base.RenderContents(writer);
            string html = "";
            if (MenuItems == null)
            {
                html = "<div>Tree goes here</div>";
            }

            writer.Write(html);
        }
    }
}
