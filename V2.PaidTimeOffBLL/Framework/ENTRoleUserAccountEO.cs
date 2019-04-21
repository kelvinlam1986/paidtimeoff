using System;
using V2.PaidTimeOffDAL;
using V2.PaidTimeOffDAL.Framework;

namespace V2.PaidTimeOffBLL.Framework
{
    [Serializable()]
    public class ENTRoleUserAccountEO : ENTBaseEO
    {
        public ENTRoleUserAccountEO()
        {
        }

        public int ENTRoleId { get; set; }
        public int ENTUserAccountId { get; private set; }

        public override void Init()
        {
            // No default
        }

        public override bool Load(int id)
        {
            var roleUserAccount = new ENTRoleUserAccountData().Select(id);
            if (roleUserAccount != null)
            {
                MapEntityToProperties(roleUserAccount);
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
                        ID = new ENTRoleUserAccountData().Insert(db, ENTRoleId, ENTUserAccountId, userAccountId);
                    }
                    else
                    {
                        if (!new ENTRoleUserAccountData().Update(db, ID, ENTRoleId, ENTUserAccountId, userAccountId, Version))
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
            ENTRoleUserAccount entRoleUserAccount = (ENTRoleUserAccount)entity;
            ID = entRoleUserAccount.ENTRoleUserAccountId;
            ENTRoleId = entRoleUserAccount.ENTRoleId;
            ENTUserAccountId = entRoleUserAccount.ENTUserAccountId;
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
