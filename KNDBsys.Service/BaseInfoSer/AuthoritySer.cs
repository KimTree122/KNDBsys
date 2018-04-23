using KNDBsys.BLL.BaseInfo;
using KNDBsys.IBLL.BaseInfo;
using KNDBsys.Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Service.BaseInfoSer
{
    public class AuthoritySer
    {
        private IAuthorityService ias = new AuthorityService();

        public string AddAuthority(string authority)
        {
            Authority auth = DataSwitch.JsonToObj<Authority>(authority);
            if (auth.AuthName == "") return General.reFail;
            return ias.Add(auth).id + "";
        }
    }
}
