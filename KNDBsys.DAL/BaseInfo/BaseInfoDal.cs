using KNDBsys.IDAL.BaseInfo;
using KNDBsys.Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.DAL.BaseInfo
{
    class BaseInfoDal
    {
    }

    public class AuthorityDal : BaseDal<Authority>,IAuthorityDal
    {

    }

    public class UserAuthDal : BaseDal<UserAuth> , IUserAuthDal
    {

    }

    //public class SysDicDal : BaseDal<SysDic>, ISysDicDal
    //{

    //}


}
