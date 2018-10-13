using KNDBsys.Service.BaseInfoSer.BaseView;
using Newtonsoft.Json;
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

        public string booltest(bool check)
        {
            string[] str = new string[2];
            str[0] = "A";
            str[1] = "B";

            string strarr = JsonConvert.SerializeObject(str);

            string[] jsontoarr = JsonConvert.DeserializeObject<string[]>(strarr);

            string strlist = "";

            foreach (string item in jsontoarr)
            {
                strlist += item+"-";
            }

            return strlist;
        }

    }
}
