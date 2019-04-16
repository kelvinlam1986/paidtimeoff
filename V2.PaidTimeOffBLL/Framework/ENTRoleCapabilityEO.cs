using System;
using V2.PaidTimeOffDAL;
using V2.PaidTimeOffDAL.Framework;

namespace V2.PaidTimeOffBLL.Framework
{
    [Serializable()]
    public class ENTRoleCapabilityEO : ENTBaseEO
    {
        public enum CapabilityAccessFlagEnum
        {
            None,
            ReadOnly,
            Edit
        }

        public ENTRoleCapabilityEO()
        {
            Capability = new ENTCapabilityBO();
        }

        public int ENTRoleId { get; set; }
        public CapabilityAccessFlagEnum AccessFlag { get; set; }
        public ENTCapabilityBO Capability { get; private set; }

        public override void Init()
        {
            // No default
        }

        public override bool Load(int id)
        {
            var roleCapability = new ENTRoleCapabilityData().Select(id);
            if (roleCapability != null)
            {
                MapEntityToProperties(roleCapability);
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Save(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors, int userAccountId)
        {
            if (DbAction == DBActionEnum.Save)
            {
                Validate(db, ref validationErrors);
                if (validationErrors.Count == 0)
                {
                    if (IsNewRecord())
                    {
                        ID = new ENTRoleCapabilityData().Insert(db, ENTRoleId, Capability.ID, Convert.ToByte(AccessFlag), 
                            userAccountId);
                    }
                    else
                    {
                        if (!new ENTRoleCapabilityData().Update(db, ID, ENTRoleId, Capability.ID, Convert.ToByte(AccessFlag), 
                            userAccountId, Version))
                        {
                            UpdateFailed(ref validationErrors);
                            return false;
                        }
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new Exception("DB Action is not Save");
            }
        }

        protected override void DeleteForReal(HRPaidTimeOffDataContext db)
        {
            throw new NotImplementedException();
        }

        protected override string GetDisplayText()
        {
            throw new NotImplementedException();
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            ENTRoleCapability roleCapability = (ENTRoleCapability)entity;
            ID = roleCapability.ENTRoleCapabilityId;
            ENTRoleId = roleCapability.ENTRoleId;
            AccessFlag = (CapabilityAccessFlagEnum)roleCapability.AccessFlag;
            Capability.Load(roleCapability.ENTCapabilityId);
        }

        protected override void Validate(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {
            // No validate
        }

        protected override void ValidateDelete(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {
            // No validate
        }
    }
}
