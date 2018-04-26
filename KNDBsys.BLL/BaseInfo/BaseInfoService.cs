using KNDBsys.DAL.BaseInfo;
using KNDBsys.IBLL.BaseInfo;
using KNDBsys.Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.BLL.BaseInfo
{
    class BaseInfoService
    {

    }

    public class AuthorityService : BaseServiceBLL<Authority>, IAuthorityService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = new AuthorityDal();
        }
    }

    public class UserAuthService : BaseServiceBLL<UserAuth>, IUserAuthService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = new UserAuthDal();
        }
    }

}
