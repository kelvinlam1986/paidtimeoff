using System;
using System.Collections.Generic;
using System.Linq;
using V2.PaidTimeOffDAL;
using V2.PaidTimeOffDAL.Framework;

namespace V2.PaidTimeOffBLL.Framework
{
    [Serializable()]
    public class ENTUserAccountEOList : ENTBaseEOList<ENTUserAccountEO>
    {
        public override void Load()
        {
            LoadFromList(new ENTUserAccountData().Select());
        }

        public void LoadWithRoles()
        {
            Load();
            foreach (var user in this)
            {
                user.Roles.LoadByENTUserAccountId(user.ID);
            }
        }

        public ENTUserAccountEO GetByWindowAccountName(string windowAccountName)
        {
            return this.SingleOrDefault(u => u.WindowAccountName.ToUpper() == windowAccountName.ToUpper());
        }

        protected void LoadFromList(List<ENTUserAccount> users)
        {
            foreach (var user in users)
	        {
		        var newUserAccountEO = new ENTUserAccountEO();
                newUserAccountEO.MapEntityToProperties(user);
                this.Add(newUserAccountEO);
	        }
            
        }
    }
}
