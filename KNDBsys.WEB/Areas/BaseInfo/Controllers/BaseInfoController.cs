using KNDBsys.BLL.BaseInfo;
using KNDBsys.IBLL.BaseInfo;
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

        //public string GetUserInfo(string name) 
        //{
        //    return iis.GetEntity(ent => ent.Uname == name).Uname;
        //}

        public string GetUserInfo(string name,string post)
        {
            return iis.GetEntity(ent => ent.Uname == name).Uname+post;
        }

    }
}
