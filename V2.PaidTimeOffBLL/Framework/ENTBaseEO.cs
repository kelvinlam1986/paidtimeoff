using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using V2.PaidTimeOffDAL;

namespace V2.PaidTimeOffBLL.Framework
{
    [Serializable()]
    public abstract class ENTBaseEO : ENTBaseBO
    {

        public enum DBActionEnum
        {
            Save,
            Delete
        }

        public DBActionEnum DbAction { get; set; }

        public ENTBaseEO()
            : base() 
        {
        }

        public bool Save(ref ENTValidationErrors validationErrors, int userAccountId)
        {
            if (DbAction == DBActionEnum.Save)
            {
                using (var ts = new TransactionScope())
                {
                    using (HRPaidTimeOffDataContext db = new HRPaidTimeOffDataContext())
                    {
                        if (this.Save(db, ref validationErrors, userAccountId))
                        {
                            ts.Complete();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                throw new Exception("DbAction not Save");
            }
        }
        public abstract bool Save(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors, int userAccountId);
        protected abstract void Validate(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors);
        public abstract void Init();
        protected abstract void DeleteForReal(HRPaidTimeOffDataContext db);
        protected abstract void ValidateDelete(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors);
        public bool Delete(ref ENTValidationErrors validationErrors, int userAccountId)
        {
            if (DbAction == DBActionEnum.Delete)
            {
                using (var ts = new TransactionScope())
                {
                    using (var db = new HRPaidTimeOffDataContext())
                    {
                        this.Delete(db, ref validationErrors, userAccountId);
                        if (validationErrors.Count == 0)
                        {
                            ts.Complete();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            } 
            else
            {
                throw new Exception("DBAction is not Delete");
            }
        }
        internal virtual bool Delete(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors, int userAccountId)
        {
            if (DbAction == DBActionEnum.Delete)
            {
                ValidateDelete(db, ref validationErrors);
                if (validationErrors.Count == 0)
                {
                    DeleteForReal(db);
                    return true;
                }
                else 
                {
                    return false;
                }
            }
            else 
            {
                throw new Exception("DBAction is not Delete");
            }
        }
        public bool IsNewRecord()
        {
            return ID == 0;
        }
        protected void UpdateFailed(ref ENTValidationErrors validationErrors)
        {
            validationErrors.Add("This record was updated by someone else while you were " +
                "editing it.  Your changes were not saved.");
        }

    }
}
