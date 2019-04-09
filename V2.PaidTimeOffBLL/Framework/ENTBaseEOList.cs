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
    public abstract class ENTBaseEOList<T> : ENTBaseBOList<T>
        where T: ENTBaseEO, new()
    {
        public bool Save(ref ENTValidationErrors validationErrors, int userAccountId)
        {
            if (this.Count > 0)
            {
                using (var ts = new TransactionScope())
                {
                    using (var db = new HRPaidTimeOffDataContext())
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
                return true;
            }
        }

        public bool Save(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors, int userAccountId)
        {
            foreach (ENTBaseEO genericEO in this)
            {
                if (genericEO.DbAction == ENTBaseEO.DBActionEnum.Save)
                {
                    if (!genericEO.Save(db, ref validationErrors, userAccountId))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

                if (genericEO.DbAction == ENTBaseEO.DBActionEnum.Delete)
                {
                    if (!genericEO.Delete(db, ref validationErrors, userAccountId))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            return true;
        }
    }
}
