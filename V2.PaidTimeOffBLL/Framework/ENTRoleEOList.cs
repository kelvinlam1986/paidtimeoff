using System.Collections.Generic;
using System.Linq;
using V2.PaidTimeOffDAL;
using V2.PaidTimeOffDAL.Framework;

namespace V2.PaidTimeOffBLL.Framework
{
    public class ENTRoleEOList : ENTBaseEOList<ENTRoleEO>
    {
        public override void Load()
        {
            LoadFromList(new ENTRoleData().Select());
        }

        private void LoadFromList(List<ENTRole> roles)
        {
            if (roles.Count > 0)
            {
                foreach (var role in roles)
                {
                    ENTRoleEO newRole = new ENTRoleEO();
                    newRole.MapEntityToProperties(role);
                    this.Add(newRole);
                }
            }
        }

        internal ENTRoleEO GetByRoleId(int roleId)
        {
            return this.SingleOrDefault(r => r.ID == roleId);
        }

        internal void LoadByENTUserAccountId(int entUserAccountId)
        {
            LoadFromList(new ENTRoleData().SelectByENTUserAccountId(entUserAccountId));
        }
    }
}
