using KNDBsys.Model.BaseInfo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KNDBsys.WEB.Areas.TestView.Controllers
{
    public class DataTestController : Controller
    {
        //
        // GET: /TestView/DataTest/

        public ActionResult Index()
        {
            return View();
        }

        //ViewBag的类型是动态的，不确定的，直接就可以使用，它的传值范围是：controller向view传值，view自己和自己传值。
        public ActionResult ViewBagTest()
        {
            ViewBag.Message = "Hello,world ViewBagTest";
            return View();
        }

        //ViewData的类型是很明确的，使用的时候经常需要强制类型转换，它的传值范围是：controller向view传值，view自己和自己传值。
        public ActionResult ViewDataTest()
        {
            ViewData["message"] = "Hello,Word ViewDataTest";
            return View();
        }

        //TempData存在的目的就是为了防止redirect时候数据的丢失（ViewData、ViewBag在跳转后就会变成null，但是TempData不会），它的传值范围是当前controller和跳转后的controller之间。-未成功
        public ActionResult TempDataTest()
        {
            TempData["message"] = "Hello,world TempDataTest";
            return View();
        }

        public ActionResult TempDataTest2()
        {
            if ("Hello,world TempDataTest" == TempData["message"] as string )
            {
                TempData["message"] = "hello!";
            }
            else
            {
                TempData["message"] = "hello!2";
            }

            return View();
        }

        //普通页面传递model-razor实现
        public ActionResult ModelTest()
        {
            UserInfo userInfo = new UserInfo() {  Uname = "kim"};
            return View(userInfo);
        }

        //Redirect
        public ActionResult RedirectTest()
        {
            //(传递字符串)
            //RedirectToAction(控制器, 控制器方法, new { name = value,....})
            UserInfo userInfo = new UserInfo() { Uname = "kim" };
            return RedirectToAction("DataTest","ModelTest",userInfo);
        }

        //匿名类型
        public ActionResult JsonTest()
        {
            //ExpandoObject
            //dynamic user = new ExpandoObject();
            //user.name = "kim";

            string json = JsonConvert.SerializeObject(new { Uname = "kim",age = 30 });
            dynamic jsonObj = JsonConvert.DeserializeObject(json);
            return View(jsonObj);
        }

        public ActionResult MenuTest()
        {
            return View();
        }

        public ActionResult EasyMobileUI()
        {
            return View();
        }


    }
}
