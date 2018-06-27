using KNDBsys.Model.BaseInfo;
using KNDBsys.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Service.BaseInfoSer.BaseView
{
    public class LoginUserAuthSer
    {
        private UserInfoSer InfoSer = new UserInfoSer();
        private AuthoritySer authoritySer = new AuthoritySer();

        public UserAuthMsgVM GetUserAuthMsgByUserID(string userid)
        {
            UserInfo userInfo = InfoSer.GetUserInfobyID_claz(userid);
            List<Authority> authorities = authoritySer.GetAuthoritiesClz(userid);
            UserAuthMsgVM vM = new UserAuthMsgVM { LoginUser = userInfo, UserAuths = authorities };
            return vM;
        }


    }
}
