using KNDBsys.DAL.BaseInfo;
using KNDBsys.IBLL.BaseInfo;
using KNDBsys.IDAL.BaseInfo;
using KNDBsys.Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.BLL.BaseInfo
{
    public class UserInfoService : BaseService<UserInfo>,IUserInfoService
    {
        public override void SetCurrentDal()
        {
            //IUserInfoDal d = new UserInfoDal();
            CurrentDal = new UserInfoDal();
        }
    }
}
