using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace V2.PaidTimeOffDAL.Framework
{
    public class ENTRoleData : ENTBaseData<ENTRole>
    {
        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            db.ENTRoleDelete(id);
        }

        public override List<ENTRole> Select()
        {
            using (var db = 
                new HRPaidTimeOffDataContext(DBHelper.GetHRPaidTimeOffConnectionString()))
            {
                return db.ENTRoleSelectAll().ToList();
            }
        }

        public override ENTRole Select(int id)
        {
            using (var db =
                new HRPaidTimeOffDataContext(DBHelper.GetHRPaidTimeOffConnectionString()))
            {
                return db.ENTRoleSelectById(id).SingleOrDefault();
            }
        }

        #region Insert

        public int Insert(HRPaidTimeOffDataContext db, string roleName, int insertENTUserAccountId)
        {
            int? entRoleId = 0;
            db.ENTRoleInsert(ref entRoleId, roleName, insertENTUserAccountId);
            return Convert.ToInt32(entRoleId);
        }

        public int Insert(string connectionString, string roleName, int insertENTUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Insert(db, roleName, insertENTUserAccountId);
            }
        }

        #endregion

        #region Update

        public bool Update(HRPaidTimeOffDataContext db, int entRoleId, string roleName,
         int updateENTUserAccountId, Binary version)
        {
            int rowEffected = db.ENTRoleUpdate(entRoleId, roleName, updateENTUserAccountId, version);
            return rowEffected == 1;
        }

        public bool Update(string connectionString, int entRoleId, string roleName,
            int updateENTUserAccountId, Binary version)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Update(db, entRoleId, roleName, updateENTUserAccountId, version);
            }
        }

        #endregion

        #region Utility

        public bool IsDuplicateRoleName(HRPaidTimeOffDataContext db, int entRoleId, string roleName)
        {
            return IsDuplicate(db, "ENTRole", "RoleName", "ENTRoleId", roleName, entRoleId);
        }

        public List<ENTRole> SelectByENTUserAccountId(int entUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext(DBHelper.GetHRPaidTimeOffConnectionString()))
            {
                return db.ENTRoleSelectByENTUserAccountId(entUserAccountId).ToList();
            }
        }

        #endregion
    }
}
