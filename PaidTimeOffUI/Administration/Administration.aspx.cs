using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using V2.PaidTimeOffBLL.Framework;

namespace PaidTimeOffUI.Administration
{
    public partial class Administration : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            IgnoreCapabilityCheck = true;
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var administrationMenuItem = Globals.GetMenuItems(Cache).GetByMenuItemName("Administration");
                CreateChildNodes(tvMenuDescriptions.Nodes, administrationMenuItem.ChildMenuList);
            }
        }

        private void CreateChildNodes(TreeNodeCollection treeNodeCollection, ENTMenuItemBOList childMenuItems)
        {
            foreach (var menuItem in childMenuItems)
            {
                var menuNode = new TreeNode(menuItem.MenuItemName +
                    (menuItem.Description == null ? "" : ": " + menuItem.Description));
                menuNode.SelectAction = TreeNodeSelectAction.None;
                if (menuItem.ChildMenuList.Count > 0)
                {
                    CreateChildNodes(menuNode.ChildNodes, menuItem.ChildMenuList);
                }

                treeNodeCollection.Add(menuNode);
            }
        }

        public override string MenuItemName()
        {
            return "Administration";
        }

        public override string[] CapabilityNames()
        {
            throw new NotImplementedException();
        }
    }
}