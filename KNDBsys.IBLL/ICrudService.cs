using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.IBLL
{
    public interface ICrudService<T>
    {
        string AddEntity(string json);
        int AddEntity(T Entity);
        string UpdateEntity(string json);
        bool UpdateEntity(T Entity);
        string DeleteEntity(string json);
        bool DeleteEntity(T Entity);
    }
}
