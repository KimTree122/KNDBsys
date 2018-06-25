using KNDBsys.Common;
using KNDBsys.Common.VerifyCode;
using KNDBsys.Model.BaseInfo;
using KNDBsys.Service.BaseInfoSer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KNDBsys.WEB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MainView()
        {
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        public int CLogin(string userid, string pwd, string vcode)
        {
            if (Session["v$code"] == null)
            {
                return 0;
            }

            if (Session["v$code"].ToString() != vcode)
            {
                return 0;
            }
            UserInfoSer userInfoSer = new UserInfoSer();
            string str = userInfoSer.GetUserInfoByAccount(userid, pwd).DecDES();
            int index = str.IndexOf("1");
            return index;
        }

        public ActionResult yzm()
        {
            Verify_Code verify = new Verify_Code();
            string code = verify.RandCode();
            Session["v$code"] = null;
            Session["v$code"] = code.ToLower();
            return File(verify.CreateImage(code), @"image/jpeg");
        }

    }
}
