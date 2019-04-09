using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2.PaidTimeOffDAL.Framework
{
    public interface IENTBaseEntity
    {
        DateTime InsertDate { get; set; }
        int InsertENTUserAccountId { get; set; }
        DateTime UpdateDate { get; set; }
        int UpdateENTUserAccountId { get; set; }
        Binary Version { get; set; }
    }
}
