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
        public SugarADO sado = new SugarADO();

        public SqlSugarClient ssc 
        {
            get { return sado.GetInstance(); }
        }

        public T GetEntity(Expression<Func<T, bool>> whereLamdda) 
        {
            return ssc.Queryable<T>().Where(whereLamdda).First();
        }
        public IList<T> GetEntities(Expression<Func<T,bool>> whereLamdda) 
        {
            return ssc.Queryable<T>().Where(whereLamdda).ToList();
        }
        public IList<T> GetPageEntityes(Expression<Func<T, bool>> whereLamdda, 
            Expression<Func<T, object>>orderbyLambda ,int pageSize, int pageIndex,bool isAsc)
        {
            OrderByType oby = isAsc ? OrderByType.Asc : OrderByType.Desc;
            return ssc.Queryable<T>().Where(whereLamdda).OrderBy(orderbyLambda, oby) 
                .Skip(pageIndex).Take(pageSize).ToList();
        }

        public T Add(T entity)
        {
            return ssc.Insertable(entity).ExecuteReturnEntity();
        }
        public bool Update(T entity) 
        {
            return ssc.Updateable(entity).ExecuteCommand() > 0;
        }
        public bool Del(T entity)
        {
            return ssc.Deleteable(entity).ExecuteCommand() > 0;
        }
    }
}
