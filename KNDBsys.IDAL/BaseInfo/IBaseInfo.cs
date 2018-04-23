using KNDBsys.Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.IDAL.BaseInfo
{
    class IBaseInfo
    {
    }

    public interface IAuthorityDal : IBaseDal<Authority>
    {
    }

    public interface IUserAuthDal : IBaseDal<UserAuth>
    {
    }

    public interface ICSDicTionaryDal : IBaseDal<CSDicTionary>
    {
    }

}
