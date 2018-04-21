using KNDBsys.IDAL.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.IDAL
{
    public interface IDBsession
    {
        IUserInfoDal UserInfoDal { get; }
    }
}
