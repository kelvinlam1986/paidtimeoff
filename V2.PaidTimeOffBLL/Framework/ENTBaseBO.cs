using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using V2.PaidTimeOffDAL.Framework;

namespace V2.PaidTimeOffBLL.Framework
{
    [Serializable()]
    public abstract class ENTBaseBO
    {
        public int ID { get; set; }
        public DateTime InsertDate { get; private set; }
        public int InsertENTUserAccountId { get; private set; }
        public DateTime UpdateDate { get; private set; }
        public int UpdateENTUserAccountId { get; private set; }
        public Binary Version { get; set; }

        public string DisplayText
        {
            get { return GetDisplayText(); }
        }

        protected abstract string GetDisplayText();

        public abstract bool Load(int id);

        protected abstract void MapEntityToCustomProperties(IENTBaseEntity entity);

        public void MapEntityToProperties(IENTBaseEntity entity)
        {
            if (entity != null)
            {
                InsertDate = entity.InsertDate;
                InsertENTUserAccountId = entity.InsertENTUserAccountId;
                UpdateDate = entity.UpdateDate;
                UpdateENTUserAccountId = entity.UpdateENTUserAccountId;
                Version = entity.Version;
                this.MapEntityToCustomProperties(entity);
            }
        }
       
    }
}
