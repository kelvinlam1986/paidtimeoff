using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V2.PaidTimeOffDAL;
using V2.PaidTimeOffDAL.Framework;

namespace V2.PaidTimeOffBLL.Framework
{
    [Serializable]
    public class ENTMenuItemBO : ENTBaseBO
    {
        public string MenuItemName { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public int? ParentMenuItemId { get; set; }
        public short DisplaySequence { get; set; }
        public bool IsAlwaysEnabled { get; set; }
        public ENTMenuItemBOList ChildMenuList { get; private set; }

        public ENTMenuItemBO()
        {
            ChildMenuList = new ENTMenuItemBOList();
        }

        protected override string GetDisplayText()
        {
            throw new NotImplementedException();
        }

        public override bool Load(int id)
        {
            throw new NotImplementedException();
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            ENTMenuItem menuItem = (ENTMenuItem)entity;
            ID = menuItem.ENTMenuItemId;
            MenuItemName = menuItem.MenuItemName;
            Description = menuItem.Description;
            Url = menuItem.Url;
            ParentMenuItemId = menuItem.ParentENTMenuItemId;
            DisplaySequence = menuItem.DisplaySequence;
            IsAlwaysEnabled = menuItem.IsAlwaysEnabled;
        }
    }

    [Serializable()]
    public class ENTMenuItemBOList : ENTBaseBOList<ENTMenuItemBO>
    {
        public override void Load()
        {
            List<ENTMenuItem> menuItems = new ENTMenuItemData().Select();
            foreach (var item in menuItems)
            {
                var menuItemBO = new ENTMenuItemBO();
                menuItemBO.MapEntityToProperties(item);
                if (MenuExist(menuItemBO.ID) == false)
                {
                    if (menuItemBO.ParentMenuItemId == null)
                    {
                        this.Add(menuItemBO);
                    }
                    else
                    {
                        var parent = GetByMenuItemId(Convert.ToInt32(menuItemBO.ParentMenuItemId));
                        if (parent == null) 
                        {
                            // If it gets here then the parent isn’t in the list yet.
                            // Find the parent in the list.                            
                            ENTMenuItemBO newParentMenuItem = FindOrLoadParent(menuItems, Convert.ToInt32(menuItemBO.ParentMenuItemId));
                            // Add the current child menu item to the newly added parent
                            newParentMenuItem.ChildMenuList.Add(menuItemBO);
                        }
                        else
                        {
                            parent.ChildMenuList.Add(menuItemBO);
                        }
                    }
                }
                
            }
        }

        public bool MenuExist(int menuItemId)
        {
            return (GetByMenuItemId(menuItemId) != null);
        }

        public ENTMenuItemBO GetByMenuItemId(int menuItemId)
        {
            foreach (var menuItem in this)
            {
                if (menuItem.ID == menuItemId)
                {
                    return menuItem;
                }
                else
                {
                    if (menuItem.ChildMenuList.Count > 0)
                    {
                        var childMenuItem = menuItem.ChildMenuList.GetByMenuItemId(menuItemId);
                        if (childMenuItem != null)
                        {
                            return childMenuItem;
                        }
                    }
                }
            }

            return null;
        }

        private ENTMenuItemBO FindOrLoadParent(List<ENTMenuItem> menuItems, int parentMenuItemId)
        {
            ENTMenuItem parentMenuItem = menuItems.Single(m => m.ENTMenuItemId == parentMenuItemId);
            ENTMenuItemBO menuItemBO = new ENTMenuItemBO();
            menuItemBO.MapEntityToProperties(parentMenuItem);

            if (parentMenuItem.ParentENTMenuItemId == null)
            {
                this.Add(menuItemBO);
            }
            else 
            {
                // Since this has a parent it should be added to its parent’s children.
                // Try to find the parent in the list already.
                ENTMenuItemBO parent = GetByMenuItemId(Convert.ToInt32(parentMenuItem.ParentENTMenuItemId));
                if (parent == null)
                {
                    // This one’s parent wasn’t found.  So add it.
                    ENTMenuItemBO newParent = FindOrLoadParent(menuItems, Convert.ToInt32(parentMenuItem.ParentENTMenuItemId));
                    newParent.ChildMenuList.Add(menuItemBO);
                }
                else
                {
                    // Add this menu to the child of the parent
                    parent.ChildMenuList.Add(menuItemBO);
                }
            }

            return menuItemBO;
        }

        public ENTMenuItemBO GetTopMenuItem(string menuItemName)
        {
            //Find the menu item by it name.
            ENTMenuItemBO menuItem = GetByMenuItemName(menuItemName);
            while (menuItem.ParentMenuItemId != null)
            {
                menuItem = GetByMenuItemId(Convert.ToInt32(menuItem.ParentMenuItemId));
            }
            return menuItem;
        }

        public ENTMenuItemBO GetByMenuItemName(string menuItemName)
        {
            foreach (ENTMenuItemBO menuItem in this)
            {
                // Check if this is the item we are looking for
                if (menuItem.MenuItemName == menuItemName)
                {
                    return menuItem;
                }
                else
                {
                    // Check if this menu has children
                    if (menuItem.ChildMenuList.Count > 0)
                    {
                        // Search the children for this item.
                        ENTMenuItemBO childMenuItem =
                            menuItem.ChildMenuList.GetByMenuItemName(menuItemName);
                        // If the menu is found in the children then it won’t be null
                        if (childMenuItem != null)
                        {
                            return childMenuItem;
                        }
                    }
                }
            }
            //It wasn’t found so return null.
            return null;
        }
    }
}
