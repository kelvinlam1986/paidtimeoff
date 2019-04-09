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
    public class ENTUserAccountEO : ENTBaseEO
    {
        public string WindowAccountName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public override bool Save(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors, int userAccountId)
        {
            if (DbAction == DBActionEnum.Save)
            {
                Validate(db, ref validationErrors);
                if (validationErrors.Count == 0)
                {
                    if (IsNewRecord())
                    {
                        ID = new ENTUserAccountData().Insert(db, WindowAccountName, FirstName, LastName, Email, IsActive, userAccountId);
                    }
                    else
                    {
                        if (!new ENTUserAccountData().Update(db, ID, WindowAccountName, FirstName, LastName, Email, IsActive, userAccountId, Version))
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

        protected override void Validate(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {
            if (string.IsNullOrEmpty(WindowAccountName) || WindowAccountName.Trim().Length == 0)
            {
                validationErrors.Add("The window account name is required.");
            }

            var entUserAccountData = new ENTUserAccountData();
            if (entUserAccountData.IsDuplicateWindowsAccountName(db, ID, WindowAccountName))
            {
                validationErrors.Add("Window account name must be unique");
            }
        }

        public override void Init()
        {
            IsActive = true;
        }

        protected override void DeleteForReal(HRPaidTimeOffDataContext db)
        {
            throw new NotImplementedException();
        }

        protected override void ValidateDelete(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors)
        {
            throw new NotImplementedException();
        }

        protected override string GetDisplayText()
        {
            return LastName + ", " + FirstName;
        }

        public override bool Load(int id)
        {
            var userAccount = new ENTUserAccountData().Select(id);
            if (userAccount != null)
            {
                MapEntityToProperties(userAccount);
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            ENTUserAccount userAccount = (ENTUserAccount)entity;
            ID = userAccount.ENTUserAccountId;
            WindowAccountName = userAccount.WindowsAccountName;
            FirstName = userAccount.FirstName;
            LastName = userAccount.LastName;
            Email = userAccount.Email;
            IsActive = userAccount.IsActive;
        }
    }
}
