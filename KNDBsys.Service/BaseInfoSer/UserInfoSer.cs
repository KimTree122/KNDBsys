using KNDBsys.Common;
using KNDBsys.DAL;
using KNDBsys.IService;
using KNDBsys.Model;
using KNDBsys.Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Service.BaseInfoSer
{
    public class UserInfoSer : CurdService<RegUserInfo>,IUserInfoSer
    {
        public override void SetDbset(SugarDBContext<RegUserInfo> db)
        {
            dbSet = db.EntityDb;
        }

        public string GetAllUserInfo(string userid)
        {
            var userlist = dbSet.FullClient.Queryable<RegUserInfo>().Where( e => e.delflag == false).Select((ui)=>
            new RegUserInfo() { id = ui.id, RegName = ui.RegName , RegTel = ui.RegTel }).ToList();
            return DataSwitch.HttpPostList<RegUserInfo>(userlist);
        }

        public string GetUserInfoByAccount(string account, string pwd)
        {
            var q = dbSet.GetList().Find(u => u.RegAccount == account & u.RegPwd == pwd);
            return DataSwitch.HttpPostEntity<RegUserInfo>(q);
        }


        public RegUserInfo GetUserInfobyID_claz(string userid)
        {
            var userlist = dbSet.GetById(userid);
            return userlist;
        }

        public string GetUserInfobyID(string userid)
        {
            var entity = dbSet.GetById(userid);
            return DataSwitch.HttpPostEntity<RegUserInfo>(entity);
        }

        public RegUserInfo GetUserInfoByAccount_claz(string account, string pwd)
        {
            var q = dbSet.GetList().Find(u => u.RegAccount == account & u.RegPwd == pwd);
            return q;
        }

    }
}
