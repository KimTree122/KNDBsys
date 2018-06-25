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

        public ActionResult MainView(string id)
        {
            UserInfoSer userInfoSer = new UserInfoSer();
            UserInfo user = userInfoSer.GetUserInfobyID(id);
            return View(user);
        }

        public ActionResult Login()
        {
            return View();
        }

        public string CLogin(string userid, string pwd, string vcode)
        {
            if (Session["v$code"] == null)
            {
                return "验证码失效";
            }

            if (Session["v$code"].ToString() != vcode)
            {
                Session["v$code"] = null;
                return "验证码错误";
            }
            UserInfoSer userInfoSer = new UserInfoSer();
            string str = userInfoSer.GetUserInfoByAccount(userid, pwd).DecDES();
            int index = str.IndexOf("1");
            if (index > 0)
            {
                Session["v$code"] = null;
                return index + "";
            }
            else
            {
                Session["v$code"] = null;
                return "账号密码错误";
            }
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
