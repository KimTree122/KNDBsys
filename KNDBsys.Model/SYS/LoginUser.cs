using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Model.SYS
{
    public class LoginUser
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserCode { get; set; }
        public string LoginPWD { get; set; }
        public DateTime LoginTime { get; set; }
        public bool Login { get; set; }
        public DateTime LogoutTime { get; set; }
    }
}
