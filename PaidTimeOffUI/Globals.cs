﻿using System.Web.Caching;
using V2.PaidTimeOffBLL.Framework;

namespace PaidTimeOffUI
{
    public static class Globals
    {
        #region Constants

        private const string CACHE_KEY_MENU_ITEMS = "MenuItems";
        private const string CACHE_KEY_USERS = "Users";
        private const string CACHE_KEY_ROLES = "Roles";
        private const string CACHE_KEY_CAPABILITIES = "Capabilities";

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

        public static ENTRoleEOList GetRoles(Cache cache)
        {
            if (cache[CACHE_KEY_ROLES] == null)
            {
                LoadRoles(cache);
            }

            return (ENTRoleEOList)cache[CACHE_KEY_ROLES];
        }

        public static ENTCapabilityBOList GetCapabilities(Cache cache)
        {
            if (cache[CACHE_KEY_CAPABILITIES] == null)
            {
                LoadCapabilities(cache);
            }

            return (ENTCapabilityBOList)cache[CACHE_KEY_CAPABILITIES];
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
            users.LoadWithRoles();

            cache.Remove(CACHE_KEY_USERS);
            cache[CACHE_KEY_USERS] = users;
        }

        public static void LoadRoles(Cache cache)
        {
            ENTRoleEOList roles = new ENTRoleEOList();
            roles.Load();

            cache.Remove(CACHE_KEY_ROLES);
            cache[CACHE_KEY_ROLES] = roles;
        }

        public static void LoadCapabilities(Cache cache)
        {
            ENTCapabilityBOList capabilities = new ENTCapabilityBOList();
            capabilities.Load();

            cache.Remove(CACHE_KEY_CAPABILITIES);
            cache[CACHE_KEY_CAPABILITIES] = capabilities;
        }

        #endregion Methods
    }
}