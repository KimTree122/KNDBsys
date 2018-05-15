using KNDBsys.Model;
using KNDBsys.Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Service.BaseInfoSer
{
    public class UserInfoSer : CurdService<UserInfo>
    {
        public override void SetDbset(DbContext db)
        {
            dbSet = db.UserInfoDb;
        }

        public string GetAllUserInfo(string userid)
        {
            var userlist = dbSet.FullClient.Queryable<UserInfo>().Where( e => e.delflag == false).Select((ui)=>
            new UserInfo() { id = ui.id, Uname = ui.Uname, UPost = ui.UPost, Utel = ui.Utel }).ToList();
            return DataSwitch.HttpPostList<UserInfo>(userlist);
        }
    }
}
