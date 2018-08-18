using KNDBsys.Common;
using KNDBsys.DAL;
using KNDBsys.Model;
using KNDBsys.Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Service.BaseInfoSer
{
    public class UserAuthSer : CurdService<UserAuth>
    {
        public override void SetDbset(SugarDBContext db)
        {
            dbSet = db.UserauthDb;
        }

        public string InsertTable(string authlistjson , string userid)
        {
            
            List<UserAuth> auths = dbSet.GetList( u => u.UserID == userid.ToInt());
            List<Authority> authlist = DataSwitch.JsonToList<Authority>(authlistjson);
            List<UserAuth> userAuths = new List<UserAuth>();
            List<Authority> addauth = new List<Authority>();
            foreach (var auth in authlist)
            {
                bool check = true;
                foreach (var au in auths)
                {       
                    if (auth.id == au.AuthID) check = false;
                }
                if (check)
                {
                    UserAuth userAuth = new UserAuth { AuthID = auth.id, UserID = userid.ToInt() };
                    userAuths.Add(userAuth);
                    addauth.Add(auth);
                }
            }
            if (userAuths.Count == 0) return DataSwitch.HttpPostList(addauth);
            bool insert = dbSet.InsertRange(userAuths.ToArray());
            if (insert) return DataSwitch.HttpPostList(addauth);
            return DataSwitch.HttpPostMsg(General.strFail);
            
        }

        public string DeleteAuth(string userauthjson, string userid)
        {
            List<UserAuth> authlist = DataSwitch.JsonToList<UserAuth>(userauthjson);
            var user = dbSet.GetList( u => u.UserID == userid.ToInt());

            foreach (var dbauth in user)
            {
                foreach (var auth in authlist)
                {
                    if (auth.AuthID == dbauth.AuthID)
                    {
                        dbSet.Delete(dbauth);
                    }
                }
            }
            return DataSwitch.HttpPostMsg(General.strSucess);
        }

        public string CopyAuth(string userid, string copyuserid)
        {
            var userauth = dbSet.GetList( u => u.UserID == userid.ToInt());
            foreach (var u in userauth)
            {
                u.UserID = copyuserid.ToInt();
            }
            bool check = dbSet.InsertRange(userauth.ToArray());
            string str = check ? General.strSucess : General.strFail;
            return DataSwitch.HttpPostMsg(str);
        }

    }
}
