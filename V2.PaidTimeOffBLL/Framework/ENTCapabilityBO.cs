using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V2.PaidTimeOffDAL;
using V2.PaidTimeOffDAL.Framework;

namespace V2.PaidTimeOffBLL.Framework
{
    [Serializable()]
    public class ENTCapabilityBO : ENTBaseBO
    {
        public enum AccessTypeEnum
        {
            ReadOnlyEdit = 0,
            ReadOnly = 1,
            Edit = 2
        }

        public string CapabilityName { get; private set; }
        public int? MenuItemId { get; private set; }
        public AccessTypeEnum AccessType { get; private set; }

        public override bool Load(int id)
        {
            ENTCapability capability = new ENTCapabilityData().Select(id);
            if (capability != null)
            {
                MapEntityToProperties(capability);
                return true;
            }
            else
            {
                return false;
            }
           
        }

        protected override string GetDisplayText()
        {
            return CapabilityName;
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            ENTCapability capability = (ENTCapability)entity;
            ID = capability.ENTCapabilityId;
            CapabilityName = capability.CapabilityName;
            MenuItemId = capability.ENTMenuItemId;
            AccessType = (AccessTypeEnum)capability.AccessType;
        }
    }
}
