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
    public class ENTUserAccountEOList : ENTBaseEOList<ENTUserAccountEO>
    {
        public override void Load()
        {
            LoadFromList(new ENTUserAccountData().Select());
        }

        protected void LoadFromList(List<ENTUserAccount> users)
        {
            foreach (var user in users)
	        {
		        var newUserAccountEO = new ENTUserAccountEO();
                newUserAccountEO.MapEntityToProperties(user);
                this.Add(newUserAccountEO);
	        }
            
        }
    }
}
