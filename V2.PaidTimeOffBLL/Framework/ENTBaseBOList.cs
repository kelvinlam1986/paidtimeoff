using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace V2.PaidTimeOffBLL.Framework
{
    [Serializable()]
    public abstract class ENTBaseBOList<T> : List<T> where T: ENTBaseBO, new ()
    {
        public abstract void Load();

        public List<T> SortByPropertyName(string propertyName, bool ascending)
        {
            //Create a Lambda expression to dynamically sort the data.
            var param = Expression.Parameter(typeof(T), "N");

            var sortExpresseion = Expression.Lambda<Func<T, object>>
                (Expression.Convert(Expression.Property(param, propertyName), typeof(object)), param);

            if (ascending)
            {
                return this.AsQueryable<T>().OrderBy<T, object>(sortExpresseion).ToList<T>();
            }
            else
            {
                return this.AsQueryable<T>().OrderByDescending<T, object>(sortExpresseion).ToList<T>();
            }
        }
    }
}
