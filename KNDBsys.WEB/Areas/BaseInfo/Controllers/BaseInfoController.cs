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

        public IUserInfoService iis = new UserInfoService();

        private AuthoritySer authser = new AuthoritySer();

        private DictionarySer dictionarySer = new DictionarySer();


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

        public string GetUserInfo(string name,string post)
        {
            var list = iis.GetEntities(ent => ent.Uname == name);

            return list.ToString();
        }

    }
}
