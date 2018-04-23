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

    public class AuthorityService : BaseService<Authority>, IAuthorityService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = new AuthorityDal();
        }
    }

    public class UserAuthService : BaseService<UserAuth>, IUserAuthService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = new UserAuthDal();
        }
    }

    public class CSDicTionaryService : BaseService<CSDicTionary>, ICSDicTionaryService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = new CSDicTionaryDal();
        }
    }

}
