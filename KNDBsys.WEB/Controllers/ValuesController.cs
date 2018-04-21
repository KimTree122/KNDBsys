using KNDBsys.Model;
using KNDBsys.Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace KNDBsys.WEB.Controllers
{
    public class ValuesController : Controller
    {
        // GET api/values
        public string TestSugarorm()
        {
            SugarTest st = new SugarTest();
            List<Goods> sds = st.testsql() as List<Goods>;
            string s = "";
            foreach (Goods sd in sds)
            {
                s += sd.id;
            }
            return s;
        }



    }
}