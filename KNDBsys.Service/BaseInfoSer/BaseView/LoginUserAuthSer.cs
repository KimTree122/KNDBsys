using KNDBsys.Common.TreeNode;
using KNDBsys.Model.BaseInfo;
using KNDBsys.Model.ViewModel;
using KNDBsys.Model.ViewModel.WebVM;
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
        private DictionarySer dictionarySer = new DictionarySer();

        public UserAuthMsgVM GetUserAuthMsgByUserID(string userid,string portType)
        {
            RegUserInfo userInfo = InfoSer.GetUserInfobyID_claz(userid);

            UserAuthMsgVM vM = new UserAuthMsgVM { LoginUser = userInfo };

            return vM;
        }

        public string AuthTreeNode_json(string userid,string portType)
        {
            List<Authority> authorities = authoritySer.GetAuthoritiesClz(userid, portType)
                .OrderBy(e => e.ParentID)
                .OrderBy(e => e.AOrder).ToList();
            TreeNodeHelper nodeHelper = new TreeNodeHelper();
            return nodeHelper.InitTreeNode_json(authorities, 0,false);
        }

        public string AllAuthTreeNode_json()
        {
            List<Authority> allauth = authoritySer.GetAllAuth();
            TreeNodeHelper nodeHelper = new TreeNodeHelper();
            return nodeHelper.InitTreeNode_json(allauth,0,true);
        }

        public string GetDicbytypename_json(string typenames)
        {



            return "";
        }


    }
}
