using KNDBsys.BLL.BaseInfo;
using KNDBsys.Common;
using KNDBsys.IBLL.BaseInfo;
using KNDBsys.Model;
using KNDBsys.Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Service.BaseInfoSer
{
    public class AuthoritySer:CurdService<Authority>
    {
        public override void SetDbset(DbContext db)
        {
            dbSet = db.AuthorityDb;
        }

        public string getAllAuthority()
        {
            var authlist = dbSet.GetList(e => e.id != 0);
            return DataSwitch.HttpPostList<Authority>( authlist);
        }

        public string GetUserAuth(string userid)
        {
            string sql = string.Format("select * from userauthview where  userid = '{0}'",userid);
            var authlist = dbSet.FullClient.SqlQueryable<Authority>(sql).ToList();
            return DataSwitch.HttpPostList<Authority>(authlist);
        }
    }
}
