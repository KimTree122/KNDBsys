﻿using KNDBsys.Service.BaseInfoSer.BaseView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KNDBsys.WEB.Areas.BaseInfo.Controllers
{
    public class BaseInfoViewController : Controller
    {
        //
        // GET: /BaseInfo/BaseInfoView/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetUserAuth(string userid)
        {
            return View();
        }

        public ActionResult AuthManage()
        {
            LoginUserAuthSer loginUser = new LoginUserAuthSer();
            ViewBag.allau = loginUser.AllAuthTreeNode_json();
            return View();
        }

    }
}