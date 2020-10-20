using KNDBsys.BLL.BaseInfo;
using KNDBsys.Common;
using KNDBsys.DAL;
using KNDBsys.IBLL.BaseInfo;
using KNDBsys.IService;
using KNDBsys.Model;
using KNDBsys.Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Service.BaseInfoSer
{
    public class AuthoritySer:CurdService<Authority>, IAuthoritySer
    {
        public override void SetDbset(SugarDBContext<Authority> db)
        {
            dbSet = db.EntityDb;
        }

        public string GetAllAuthority()
        {
            var authlist = GetAllAuth();
            return DataSwitch.HttpPostList<Authority>( authlist);
        }

        public List<Authority> GetAllAuth()
        {
            return dbSet.GetList(au =>au.id !=0);
        }

        public string GetUserAuth(string userid,string portType)
        {
            var authlist = GetAuthoritiesClz(userid,portType);
            return DataSwitch.HttpPostList<Authority>(authlist);
        }

        public List<Authority> GetAuthoritiesClz(string userid,string portType)
        {
            string sql = string.Format("select * from userauthview where  userid = '{0}' and sysport = '{1}'", userid,portType);
            var authlist = dbSet.FullClient.SqlQueryable<Authority>(sql).ToList();
            return authlist;
        }

        public string GetOperAuthByTag(string authtype, int tag)
        {
            var authlist = dbSet.GetList(a => a.AuthTypeName == authtype & a.ParentID == tag);
            return DataSwitch.HttpPostList<Authority>(authlist);
        }
    }
}
