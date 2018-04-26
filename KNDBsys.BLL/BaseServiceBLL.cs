using KNDBsys.IDAL;
using KNDBsys.IDAL.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;


namespace KNDBsys.BLL
{
    public abstract class BaseServiceBLL<T> where T: class , new ()
    {
        public IBaseDal<T> CurrentDal { get; set; }

        public BaseServiceBLL() 
        {
            SetCurrentDal();
        }

        public abstract void SetCurrentDal();

        public T GetEntity(Expression<Func<T, bool>> whereLamdda)
        {
            return CurrentDal.GetEntity(whereLamdda);
        }


        public List<T> GetEntities(Expression<Func<T, bool>> whereLamdda) 
        {
            return CurrentDal.GetEntities(whereLamdda);
        }

        public List<T> GetPageEntityes<S>(Expression<Func<T, bool>> whereLamdda,
            Expression<Func<T, object>> orderbyLambda, int pageSize, int pageIndex, bool isAsc)
        {
            return CurrentDal.GetPageEntityes(whereLamdda, orderbyLambda, pageSize, pageIndex, isAsc);
        }

        public T Add(T entity) 
        {
            return CurrentDal.Add(entity);
        }

        public bool Update(T entity)
        {
            return CurrentDal.Update(entity);
        }

        public bool Del(T entity)
        {
            return CurrentDal.Del(entity);
        }

    }
}
