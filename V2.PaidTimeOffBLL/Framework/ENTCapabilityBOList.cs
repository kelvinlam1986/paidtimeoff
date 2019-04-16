using System;
using System.Collections.Generic;
using System.Linq;
using V2.PaidTimeOffDAL;
using V2.PaidTimeOffDAL.Framework;

namespace V2.PaidTimeOffBLL.Framework
{
    [Serializable()]
    public class ENTCapabilityBOList : ENTBaseBOList<ENTCapabilityBO>
    {
        public override void Load()
        {
            LoadFromList(new ENTCapabilityData().Select());
        }

        protected void LoadFromList(List<ENTCapability> capabilities)
        {
            foreach (var capability in capabilities)
            {
                var newCapabilityBO = new ENTCapabilityBO();
                newCapabilityBO.MapEntityToProperties(capability);
                this.Add(newCapabilityBO);
            }
        }

        public ENTCapabilityBO GetByName(string capabilityName)
        {
            return this.SingleOrDefault(x => x.CapabilityName == capabilityName);
        }

        public IEnumerable<ENTCapabilityBO> GetByMenuItemId(int entMenuItemId)
        {
            return from c in this
                   where c.MenuItemId == entMenuItemId
                   orderby c.CapabilityName
                   select c;
        }
    }
}
