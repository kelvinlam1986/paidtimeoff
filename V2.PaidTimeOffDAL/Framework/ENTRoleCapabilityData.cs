using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace V2.PaidTimeOffDAL.Framework
{
    public class ENTRoleCapabilityData : ENTBaseData<ENTRoleCapability>
    {
        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            db.ENTRoleCapabilityDelete(id);
        }

        public override List<ENTRoleCapability> Select()
        {
            using (var db = new HRPaidTimeOffDataContext(DBHelper.GetHRPaidTimeOffConnectionString()))
            {
                return db.ENTRoleCapabilitySelectAll().ToList();
            }
        }

        public override ENTRoleCapability Select(int id)
        {
            using (var db = new HRPaidTimeOffDataContext(DBHelper.GetHRPaidTimeOffConnectionString()))
            {
                return db.ENTRoleCapabilitySelectById(id).SingleOrDefault();
            }
        }

        public int Insert(HRPaidTimeOffDataContext db, int entRoleId, int entCapabilityId,
                byte accessFlag, int insertENTUserAccountId)
        {
            int? entRoleCapabilityId = 0;
            db.ENTRoleCapabilityInsert(ref entRoleCapabilityId, entRoleId, entCapabilityId,
                accessFlag, insertENTUserAccountId);
            return Convert.ToInt32(entRoleCapabilityId);
        }

        public int Insert(string connectionString, int entRoleId, int entCapabilityId,
                byte accessFlag, int insertENTUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Insert(db, entRoleId, entCapabilityId, accessFlag, insertENTUserAccountId);
            }
        }

        public bool Update(HRPaidTimeOffDataContext db, int entRoleCapabilityId,
            int entRoleId, int entCapabilityId, byte accessFlag, int updateENTUserAccountId,
            Binary version)
        {
            int rowEffective = db.ENTRoleCapabilityUpdate(entRoleCapabilityId,
                entRoleId, entCapabilityId, accessFlag, updateENTUserAccountId, version);
            return rowEffective == 1;
        }

        public bool Update(string connectionString, int entRoleCapabilityId,
            int entRoleId, int entCapabilityId, byte accessFlag, int updateENTUserAccountId,
            Binary version)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Update(db, entRoleCapabilityId, entRoleId, entCapabilityId, accessFlag, updateENTUserAccountId, version);
            }
        }

        public List<ENTRoleCapability> SelectByENTRoleId(int entRoleId)
        {
            using (var db = new HRPaidTimeOffDataContext(DBHelper.GetHRPaidTimeOffConnectionString()))
            {
                return db.ENTRoleCapabilitySelectByENTRoleId(entRoleId).ToList();
            }
        }
    }
}
