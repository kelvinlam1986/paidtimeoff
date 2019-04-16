using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace V2.PaidTimeOffDAL.Framework
{
    public class ENTRoleUserAccountData : ENTBaseData<ENTRoleUserAccount>
    {
        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            db.ENTRoleUserAccountDelete(id);
        }

        public override List<ENTRoleUserAccount> Select()
        {
            using (var db = new HRPaidTimeOffDataContext(DBHelper.GetHRPaidTimeOffConnectionString()))
            {
                return db.ENTRoleUserAccountSelectAll().ToList();
            }
        }

        public override ENTRoleUserAccount Select(int id)
        {
            using (var db = new HRPaidTimeOffDataContext(DBHelper.GetHRPaidTimeOffConnectionString()))
            {
                return db.ENTRoleUserAccountSelectById(id).SingleOrDefault();
            }
        }

        public List<ENTRoleUserAccount> SelectByENTRoleId(int entRoleId)
        {
            using (var db = new HRPaidTimeOffDataContext(DBHelper.GetHRPaidTimeOffConnectionString()))
            {
                return db.ENTRoleUserAccountSelectByENTRoleId(entRoleId).ToList();
            }
        }

        public int Insert(HRPaidTimeOffDataContext db, int entRoleId, int entUserAccountId,
            int insertENTUserAccountId)
        {
            int? entRoleUserAccountId = 0;
            db.ENTRoleUserAccountInsert(ref entRoleUserAccountId, entRoleId, entUserAccountId, insertENTUserAccountId);
            return Convert.ToInt32(entRoleUserAccountId);
        }

        public int Insert(string connectionString, int entRoleId, int entUserAccountId,
            int insertENTUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Insert(db, entRoleId, entUserAccountId, insertENTUserAccountId);
            }
        }

        public bool Update(HRPaidTimeOffDataContext db, int entRoleUserAccountId,
            int entRoleId, int entUserAccountId, int updateENTUserAccountId,
            Binary version)
        {
            int rowEffected = db.ENTRoleUserAccountUpdate(entRoleUserAccountId,
                entRoleId, entUserAccountId, updateENTUserAccountId, version);
            return rowEffected == 1;
        }

        public bool Update(string connectionString, int entRoleUserAccountId, 
            int entRoleId, int entUserAccountId, int updateENTUserAccountId,
            Binary version)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Update(db, entRoleUserAccountId, entRoleId, entUserAccountId,
                    updateENTUserAccountId, version);
            }
        }
    }
}
