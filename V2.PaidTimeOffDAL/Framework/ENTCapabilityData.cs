using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2.PaidTimeOffDAL.Framework
{
    public class ENTCapabilityData : ENTBaseData<ENTCapability>
    {
        public override void Delete(HRPaidTimeOffDataContext db, int id)
        {
            throw new NotImplementedException();
        }

        public override List<ENTCapability> Select()
        {
            using (HRPaidTimeOffDataContext db = 
                new HRPaidTimeOffDataContext(DBHelper.GetHRPaidTimeOffConnectionString()))
            {
                return db.ENTCapabilitySelectAll().ToList();
            }
        }

        public override ENTCapability Select(int id)
        {
            using (HRPaidTimeOffDataContext db =
                new HRPaidTimeOffDataContext(DBHelper.GetHRPaidTimeOffConnectionString()))
            {
                return db.ENTCapabilitySelectById(id).SingleOrDefault();
            }
        }
    }
}
