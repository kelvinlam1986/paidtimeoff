using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2.PaidTimeOffDAL.Framework
{
    public class ENTUserAccountData : ENTBaseData<ENTUserAccount>
    {
        public override List<ENTUserAccount> Select()
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTUserAccountSelectAll().ToList();
            }
        }

        public override ENTUserAccount Select(int id)
        {
            using (var db = new HRPaidTimeOffDataContext())
            {
                return db.ENTUserAccountSelectById(id).SingleOrDefault();
            }
        }

        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            db.ENTUserAccountDelete(id);
        }

        public int Insert(string connectionString, string windowAccountName,
            string firstName, string lastName, string email, bool isActive,
            int insertUserAccountId)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString))
            {
                return Insert(db, windowAccountName, firstName, lastName, email, isActive, insertUserAccountId);
            }
        }

        public int Insert(HRPaidTimeOffDataContext db, string windowAccountName,
            string firstName, string lastName, string email, bool isActive,
            int insertUserAccountId)
        {
            int? entUserAccountId = 0;
            db.ENTUserAccountInsert(ref entUserAccountId, windowAccountName, firstName, lastName, email, isActive, insertUserAccountId);
            return Convert.ToInt32(entUserAccountId);
        }

        public bool Update(string connectionString, int userAccountId,  string windowAccountName,
            string firstName, string lastName, string email, bool isActive,
            int updateUserAccountId, Binary version)
        {
            using (var db = new HRPaidTimeOffDataContext(connectionString)) 
            {
                return Update(db, userAccountId, windowAccountName, firstName, lastName, email, isActive, updateUserAccountId, version);
            }
        }

        public bool Update(HRPaidTimeOffDataContext db, int userAccountId,  string windowAccountName,
            string firstName, string lastName, string email, bool isActive,
            int updateUserAccountId, Binary version)
        {
            int rowsEffected = db.ENTUserAccountUpdate(userAccountId, windowAccountName, firstName, lastName, email, isActive, updateUserAccountId, version);
            return rowsEffected == 1;
        }

        public bool IsDuplicateWindowsAccountName(HRPaidTimeOffDataContext db, int userAccountId, string windowsAccountnName)
        {
            return IsDuplicate(db, "ENTUserAccount", "WindowsAccountName", "ENTUserAccountId", windowsAccountnName, userAccountId);
        }
    }
}
