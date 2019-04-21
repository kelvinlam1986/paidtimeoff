using System;
using System.Collections.Generic;
using System.Linq;
using V2.PaidTimeOffDAL;
using V2.PaidTimeOffDAL.Framework;

namespace V2.PaidTimeOffBLL.Framework
{
    public class ENTRoleUserAccountEOList : ENTBaseEOList<ENTRoleUserAccountEO>
    {
        public override void Load()
        {
            LoadFromList(new ENTRoleUserAccountData().Select());
        }

        public bool IsUserInRole(int userAccountId)
        {
            return (GetByUserAccountId(userAccountId) != null);
        }

        public ENTRoleUserAccountEO GetByUserAccountId(int userAccountId)
        {
            return this.SingleOrDefault(x => x.ENTUserAccountId == userAccountId);
        }

        internal void GetByEntRoleId(int entRoleId)
        {
            LoadFromList(new ENTRoleUserAccountData().SelectByENTRoleId(entRoleId));
        }

        protected void LoadFromList(List<ENTRoleUserAccount> roleUserAccounts)
        {
            foreach (var roleUserAccount in roleUserAccounts)
            {
                var newRoleUserAccountEO = new ENTRoleUserAccountEO();
                newRoleUserAccountEO.MapEntityToProperties(roleUserAccount);
                this.Add(newRoleUserAccountEO);
            }
        }
    }
}
