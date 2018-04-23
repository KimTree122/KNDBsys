using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace KNDBsys.IBLL
{
    public interface IBaseServiceBLL <T> where T: class,new()
    {
        T GetEntity(Expression<Func<T, bool>> whereLamdda);

        List<T> GetEntities(Expression<Func<T, bool>> whereLamdda);

        List<T> GetPageEntityes<S>(Expression<Func<T, bool>> whereLamdda,
            Expression<Func<T, object>> orderbyLambda, int pageSize, int pageIndex, bool isAsc);

        T Add(T entity);

        bool Update(T entity);

        bool Del(T entity);
    }
}
