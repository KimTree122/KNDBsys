using KNDBsys.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace KNDBsys.DAL
{
    public class BaseDal<T> where T:class,new ()
    {
        private SugarADO sado = new SugarADO();

        public SqlSugarClient Ssc  
        {
            get { return sado.GetInstance(); }
        }

        public T GetEntity(Expression<Func<T, bool>> whereLamdda) 
        {
            return Ssc.Queryable<T>().Where(whereLamdda).First();
        }

        public List<T> GetEntities(Expression<Func<T, bool>> whereLamdda) 
        {
            return Ssc.Queryable<T>().Where(whereLamdda).ToList();
        }

        public List<T> GetPageEntityes(Expression<Func<T, bool>> whereLamdda, 
            Expression<Func<T, object>>orderbyLambda ,int pageSize, int pageIndex,bool isAsc)
        {
            OrderByType oby = isAsc ? OrderByType.Asc : OrderByType.Desc;
            return Ssc.Queryable<T>().Where(whereLamdda).OrderBy(orderbyLambda, oby) 
                .Skip(pageIndex).Take(pageSize).ToList();
        }

        public T Add(T entity)
        {
            return Ssc.Insertable(entity).ExecuteReturnEntity();
        }

        public bool Update(T entity) 
        {
            return Ssc.Updateable(entity).ExecuteCommand() > 0;
        }

        public bool Del(T entity)
        {
            return Ssc.Deleteable(entity).ExecuteCommand() > 0;
        }
    }
}
