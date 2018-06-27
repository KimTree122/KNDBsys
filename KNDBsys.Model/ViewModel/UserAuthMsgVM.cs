using KNDBsys.Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Model.ViewModel
{
    public class UserAuthMsgVM
    {
        public UserInfo LoginUser { get; set; }
        public List<Authority> UserAuths { get; set; }
        public List<string> Msg { get; set; }
    }
}
