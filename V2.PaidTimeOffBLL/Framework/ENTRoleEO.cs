using System;
using V2.PaidTimeOffDAL;
using V2.PaidTimeOffDAL.Framework;

namespace V2.PaidTimeOffBLL.Framework
{
    [Serializable()]
    public class ENTRoleEO : ENTBaseEO
    {
        public ENTRoleEO()
        {
            RoleCapabilites = new ENTRoleCapabilityEOList();
            RoleUserAccounts = new ENTRoleUserAccountEOList();
        }

        public string RoleName { get; set; }
        public ENTRoleCapabilityEOList RoleCapabilites { get; set; }
        public ENTRoleUserAccountEOList RoleUserAccounts { get; set; }

        public override void Init()
        {
            // No defaults
        }

        public override bool Load(int id)
        {
            ENTRole role = new ENTRoleData().Select(id);
            if (role != null)
            {
                MapEntityToProperties(role);
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
                        ID = new ENTRoleData().Insert(db, RoleName, userAccountId);
                        foreach (var capability in RoleCapabilites)
                        {
                            capability.ENTRoleId = ID;
                        }

                        foreach (var user in RoleUserAccounts)
                        {
                            user.ENTRoleId = ID;
                        }
                    }
                    else
                    {
                        if (!new ENTRoleData().Update(db, ID, RoleName, userAccountId, Version))
                        {
                            UpdateFailed(ref validationErrors);
                            return false;
                        }
                    }

                    if (RoleCapabilites.Save(ref validationErrors, userAccountId))
                    {
                        if (RoleUserAccounts.Save(ref validationErrors, userAccountId))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new Exception("DBAction is not saved");
            }
        }

        protected override void DeleteForReal(HRPaidTimeOffDataContext db)
        {
            if (DbAction == DBActionEnum.Delete)
            {
                foreach (var capability in RoleCapabilites)
                {
                    new ENTRoleCapabilityData().Delete(db, capability.ID);
                }

                foreach (var user in RoleUserAccounts)
                {
                    new ENTRoleUserAccountData().Delete(db, user.ID);
                }

                new ENTRoleData().Delete(db, ID);
            }
            else
            {
                throw new Exception("DBAction not delete");
            }
        }

        protected override string GetDisplayText()
        {
            return RoleName;
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            ENTRole role = (ENTRole)entity;
            ID = role.ENTRoleId;
            RoleName = role.RoleName;
            RoleCapabilites.LoadByENTRoleId(ID);
            RoleUserAccounts.GetByEntRoleId(ID);
        }

        protected override void Validate(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {
            if (RoleName.Trim().Length == 0)
            {
                validationErrors.Add("The name is required");
            }

            if (new ENTRoleData().IsDuplicateRoleName(db, ID, RoleName))
            {
                validationErrors.Add("The name must be unique");
            }
        }

        protected override void ValidateDelete(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {
        }
    }
}
