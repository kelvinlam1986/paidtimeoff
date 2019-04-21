using System.Linq;
using System.Collections.Generic;
using V2.PaidTimeOffDAL;
using V2.PaidTimeOffDAL.Framework;
using System;

namespace V2.PaidTimeOffBLL.Framework
{
    [Serializable()]
    public class ENTRoleCapabilityEOList : ENTBaseEOList<ENTRoleCapabilityEO>
    {
        public override void Load()
        {
            this.LoadFromList(new ENTRoleCapabilityData().Select());
        }

        internal IEnumerable<ENTRoleCapabilityEO> GetByMenuItemId(int menuItemId)
        {
            return from rc in this
                   where rc.Capability.MenuItemId == menuItemId
                   select rc;
        }

        public ENTRoleCapabilityEO GetByCapabilityID(int capabilityId)
        {
            return this.SingleOrDefault(x => x.Capability.ID == capabilityId);
        }
        
        internal void LoadByENTRoleId(int entRoleId)
        {
            LoadFromList(new ENTRoleCapabilityData().SelectByENTRoleId(entRoleId));
        }

        protected void LoadFromList(List<ENTRoleCapability> roleCapabilities)
        {
            foreach (var entRoleCapability in roleCapabilities)
            {
                var newRoleCapabilityEO = new ENTRoleCapabilityEO();
                newRoleCapabilityEO.MapEntityToProperties(entRoleCapability);
                this.Add(newRoleCapabilityEO);
            }
        }
    }
}
