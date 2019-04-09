using System.Web.Caching;
using V2.PaidTimeOffBLL.Framework;

namespace PaidTimeOffUI
{
    public static class Globals
    {
        #region Constants

        private const string CACHE_KEY_MENU_ITEMS = "MenuItems";
        private const string CACHE_KEY_USERS = "Users";

        #endregion Constants

        #region Methods

        public static ENTMenuItemBOList GetMenuItems(Cache cache)
        {
            //Check if the menus have been cached.
            if (cache[CACHE_KEY_MENU_ITEMS] == null)
            {
                LoadMenuItems(cache);
            }

            return (ENTMenuItemBOList)cache[CACHE_KEY_MENU_ITEMS];
        }

        public static ENTUserAccountEOList GetUsers(Cache cache)
        {
            //Check for the users
            if (cache[CACHE_KEY_USERS] == null)
            {
                LoadUsers(cache);
            }

            return (ENTUserAccountEOList)cache[CACHE_KEY_USERS];
        }

        public static void LoadMenuItems(Cache cache)
        {
            ENTMenuItemBOList menuItems = new ENTMenuItemBOList();
            menuItems.Load();

            cache.Remove(CACHE_KEY_MENU_ITEMS);
            cache[CACHE_KEY_MENU_ITEMS] = menuItems;
        }

        public static void LoadUsers(Cache cache)
        {
            ENTUserAccountEOList users = new ENTUserAccountEOList();
            users.Load();

            cache.Remove(CACHE_KEY_USERS);
            cache[CACHE_KEY_USERS] = users;
        }

        #endregion Methods
    }
}