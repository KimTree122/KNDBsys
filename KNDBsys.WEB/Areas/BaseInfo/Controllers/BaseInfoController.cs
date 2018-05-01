﻿using KNDBsys.BLL.BaseInfo;
using KNDBsys.IBLL.BaseInfo;
using KNDBsys.Service.BaseInfoSer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KNDBsys.WEB.Areas.BaseInfo.Controllers
{
    public class BaseInfoController : Controller
    {
        //
        // GET: /BaseInfo/BaseInfo/

        public ActionResult Index()
        {
            return View();
        }

        private AuthoritySer authser = new AuthoritySer();

        private DictionarySer dictionarySer = new DictionarySer();

        private UserInfoSer userInfoSer = new UserInfoSer();


        #region 数据字典
        public string GetDictionary(string dictype)
        {
            return dictionarySer.GetDicbytype(dictype);
        }

        public string AddDictionary(string dic)
        {
            return dictionarySer.AddDictionary(dic); 
        }

        public string Updatedictionary(string dic)
        {
            return dictionarySer.UpdateDictionary(dic);
        }

        public string DeleteSysdic(string dic)
        {
            return dictionarySer.DeleteSysdic(dic);
        }
        #endregion

        #region 权限

        public string GetAllAuthority(string userid)
        {
            return authser.getAllAuthority();
        }

        public string AddAuthority(string authority)
        {
            return authser.AddEntity(authority);
        }

        public string UpdateAuthority(string authority)
        {
            return authser.UpdateEntity(authority);
        }

        public string DeleteAuthority(string authority)
        {
            return authser.DeleteEntity(authority);
        }

        #endregion

        #region 用户

        public string GetAllUserInfo(string userid)
        {
            return userInfoSer.GetAllUserInfo(userid);
        }

        public string AddUserInfo(string userinfo)
        {
            return userInfoSer.AddEntity(userinfo);
        }

        public string UpdateUserInfo(string userinfo)
        {
            return userInfoSer.UpdateEntity(userinfo);
        }

        public string DeleteUserInfo(string userinfo)
        {
            return userInfoSer.DeleteEntity(userinfo);
        }

        #endregion
    }
}
