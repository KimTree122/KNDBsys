using KNDBsys.BLL.BaseInfo;
using KNDBsys.IBLL.BaseInfo;
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

        private AuthoritySer authser = new AuthoritySer();
        private DictionarySer dictionarySer = new DictionarySer();
        private UserInfoSer userInfoSer = new UserInfoSer();
        private UserAuthSer userAuthSer = new UserAuthSer();
        private ServerTypeSer serverTypeSer = new ServerTypeSer();
        private CustomInfoSer customInfoSer = new CustomInfoSer();


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
            return userInfoSer.UpdateEntity(userinfo);
        }

        #endregion

        #region 用户权限

        public string GetUserAuth(string userid)
        {
            return authser.GetUserAuth(userid);
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

    }
}
