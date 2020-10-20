using KNDBsys.Common;
using KNDBsys.Common.VerifyCode;
using KNDBsys.Model;
using KNDBsys.Model.BaseInfo;
using KNDBsys.Model.ViewModel;
using KNDBsys.Service.BaseInfoSer;
using KNDBsys.Service.BaseInfoSer.BaseView;
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

        public ActionResult Registers()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult MainView()
        {
            object id = Session["U@id"];
            if (id == null)
            {
                //return View("Error");
                id = "1";
            }
            LoginUserAuthSer loginUser = new LoginUserAuthSer();
            UserAuthMsgVM vM = loginUser.GetUserAuthMsgByUserID(id.ToString(),"Web");
            string str = loginUser.AuthTreeNode_json(id.ToString(), "Web");

            ViewBag.au = str;
            return View(vM);
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
            RegUserInfo user = userInfoSer.GetUserInfoByAccount_claz(userid, pwd);

            if (user !=null)
            {
                Session["U@id"] = user.id;
                return "success";
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
