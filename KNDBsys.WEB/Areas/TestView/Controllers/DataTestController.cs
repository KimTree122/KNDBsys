using KNDBsys.Common.ObserverPattern;
using KNDBsys.Model.BaseInfo;
using KNDBsys.Model.SYS;
using KNDBsys.WEB.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace KNDBsys.WEB.Areas.TestView.Controllers
{
    public class DataTestController : Controller
    {
        //
        // GET: /TestView/DataTest/



        //观察者模式
        public string ObserverTest()
        {
            ObserverTest ot = new ObserverTest();
            string msg =  ot.ObserverToDo();
            return msg;
        }

        public ActionResult CreateToken(string username, string pwd)
        {

            DataResult result = new DataResult();

            //假设用户名为"admin"，密码为"123"  
            if (username == "admin" && pwd == "123")
            {

                var payload = new Dictionary<string, object>
                {
                    { "UserCode",username },
                    { "LoginPWD", pwd },
                    { "LoginTime",DateTime.Now.AddMilliseconds(10)}
                };

                result.Token = JwtHelp.SetJwtEncode(payload);
                result.Success = true;
                result.Message = "成功";
            }
            else
            {
                result.Token = "";
                result.Success = false;
                result.Message = "生成token失败";
            }

            return Json(result);
            //get请求需要修改成这样
            //return Json(result,JsonRequestBehavior.AllowGet);
        }


        public string DynamicModel()
        {
            dynamic dynamicModel = new DynamicModel();
            List<string> myList = new List<string>();
            myList.Add("Name");
            myList.Add("Age");
            myList.Add("Hobby");

            List<string> myValueList = new List<string>();
            myValueList.Add("Mary");
            myValueList.Add("18");
            myValueList.Add("Dance");

            for (int i = 0; i < myList.Count; i++)
            {
                string myAttr = myList[i];
                dynamicModel.PropertyName = myAttr;
                dynamicModel.Property = myValueList[i];
            }
            string str = dynamicModel.Name;

            return dynamicModel.toValue;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DynamicsGridView()
        {
            return View();
        }

        public ActionResult DynamicsControl()
        {
            return View();
        }

        //锐浪报表测试
        public ActionResult RLReportTestView()
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
            if ("Hello,world TempDataTest" == TempData["message"] as string)
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
            UserInfo userInfo = new UserInfo() { Uname = "kim" };
            return View(userInfo);
        }

        //Redirect
        public ActionResult RedirectTest()
        {
            //(传递字符串)
            //RedirectToAction(控制器, 控制器方法, new { name = value,....})
            UserInfo userInfo = new UserInfo() { Uname = "kim" };
            return RedirectToAction("DataTest", "ModelTest", userInfo);
        }

        //匿名类型
        public ActionResult JsonTest()
        {
            //ExpandoObject
            //dynamic user = new ExpandoObject();
            //user.name = "kim";

            string json = JsonConvert.SerializeObject(new { Uname = "kim", age = 30 });
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

        //数据流接收
        public ActionResult StreamData()
        {
            //Stream s = System.Web.HttpContext.Current.Request.InputStream;
            //byte[] b = new byte[s.Length];
            //s.Read(b, 0, (int)s.Length);
            //string  postStr = Encoding.UTF8.GetString(b);
            //if (!string.IsNullOrWhiteSpace(postStr))
            //{
            //    string str = postStr;
            //}

            //using (Stream inputstream = System.Web.HttpContext.Current.Request.InputStream )
            //{
            //    byte[] b = new byte[inputstream.Length];
            //    inputstream.Read(b, 0, (int)inputstream.Length);
            //    string postStr = Encoding.UTF8.GetString(b);
            //    if (!string.IsNullOrWhiteSpace(postStr))
            //    {
            //        json = postStr;
            //    }
            //    inputstream.Close();
            //}

            //Stream postData = Request.InputStream;
            //StreamReader sRead = new StreamReader(postData);
            //string postContent = sRead.ReadToEnd();
            //sRead.Close();
            return Content("");
        }

        //动态数据
        public ActionResult DynamicsData()
        {
            return View();
        }

        //弹框测试
        public ActionResult DialogFrame()
        {
            return View();
        }

        //测试新弹框
        public ActionResult SearchFrame()
        {
            return View();
        }

        //基础数据查询测试
        public ActionResult BaseDataDataGridTest()
        {
            return View();
        }

        //流程图测试
        public ActionResult WorkFlowView()
        {
            return View();
        }

        //GOJS流程图
        public ActionResult GoJsWorkFlow()
        {
            return View();
        }

        public ActionResult GoJsWorkFlow2()
        {
            return View();
        }

        public ActionResult AWorkflowView()
        {
            return View();
        }

        public ActionResult DynamicsTable()
        {
            return View();
        }

        public string ReceiveDynamics(string json)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            IList<Dictionary<string, string>> jsonlist = js.Deserialize<IList<Dictionary<string, string>>>(json);

            string jsonid = string.Empty;

            foreach (var item in jsonlist)
            {
                jsonid += item["id"] + ",";
            }

            return jsonid;
        }

        public string ReceiveDynamics2(string json)
        {
            JObject jObject = JObject.Parse(json);
            string jsonstr = string.Empty;
            foreach (var pro in jObject.Properties())
            {
                jsonstr += "name:" + pro.Name + "value:" + pro.Value;
            }
            return jsonstr;
        }


    }
}
