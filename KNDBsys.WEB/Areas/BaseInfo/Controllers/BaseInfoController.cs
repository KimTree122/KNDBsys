﻿using KNDBsys.BLL.BaseInfo;
using KNDBsys.IBLL.BaseInfo;
using KNDBsys.IService;
using KNDBsys.Service.BaseInfoSer;
using KNDBsys.Service.WorkSer;
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
        private DictionarySer dictionarySer = new DictionarySer();
        private IAuthoritySer authser = new AuthoritySer();
        private IUserInfoSer userInfoSer = new UserInfoSer();
        private IUserAuthSer userAuthSer = new UserAuthSer();
        private IServerTypeSer serverTypeSer = new ServerTypeSer();
        private ICustomInfoSer customInfoSer = new CustomInfoSer();
        private ISysVerSer sysVerSer = new SysVerSer();


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
            return authser.GetAllAuthority();
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

        public string GetOperAuthByTag(string authtypeName, int tag)
        {
            return authser.GetOperAuthByTag(authtypeName, tag);
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
            return userInfoSer.UpdateEntity(userinfo);
        }

        public string GetUserInfoByAccount(string account, string pwd)
        {
            return userInfoSer.GetUserInfoByAccount(account,pwd);
        }

        public string GetUserInfoByID(string userid)
        {
            return userInfoSer.GetUserInfobyID(userid);
        }

        #endregion

        #region 用户权限

        public string GetUserAuth(string userid,string portType)
        {
            return authser.GetUserAuth(userid,portType);
        }

        public string AddUserAuth(string auth, string userid)
        {
            return userAuthSer.InsertTable(auth,userid);
        }

        public string DelteUserAuth(string userauthjson, string userid)
        {
            return userAuthSer.DeleteAuth(userauthjson,userid);
        }

        public string CopyUserAuth(string userid, string copyuserid)
        {
            return  userAuthSer.CopyAuth(userid, copyuserid);
        }

        #endregion

        #region 服务类别

        public string GetAllServerType(string userid)
        {
            return serverTypeSer.GetAllServerType(userid);
        }

        public string AddServerType(string servertype)
        {
            return serverTypeSer.AddEntity(servertype);
        }

        public string UpdatServerType(string servertype)
        {
            return serverTypeSer.UpdateEntity(servertype);
        }

        public string DeleteServerType(string servertype)
        {
            return serverTypeSer.DeleteEntity(servertype);
        }

        #endregion

        #region 客户信息

        public string GetAllCustomInfo(string userid)
        {
            return customInfoSer.GetAllCustomInfo(userid);
        }

        public string AddCustomInfo(string customInfo)
        {
            return customInfoSer.AddEntity(customInfo);
        }

        public string UpdatCustomInfo(string customInfo)
        {
            return customInfoSer.UpdateEntity(customInfo);
        }

        public string DeleteCustomInfo(string customInfo)
        {
            return customInfoSer.DeleteEntity(customInfo);
        }

        public string CountCustomTel(string tel)
        {
            return customInfoSer.FindSameCustomByTel(tel);
        }

        public string FindCustomByTel(string tel)
        {
            return customInfoSer.FindCustomByTel(tel);
        }

        public string FindCustomByid(string customid)
        {
            return customInfoSer.FindCustomByid(customid);
        }

        #endregion

        #region 程序更新

        public string GetNewSysVer(string programtype)
        {
            return sysVerSer.GetNewSysVer(programtype);
        }

        public string AddSysVer(string sysver)
        {
            return sysVerSer.AddEntity(sysver);
        }

        public ActionResult AddView()
        {
            return View();
        }


        #endregion

    }
}
