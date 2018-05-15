using KNDBsys.Service.WorkSer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KNDBsys.WEB.Areas.Work.Controllers
{
    public class WorkBLLController : Controller
    {
        //
        // GET: /Work/WorkBLL/

        public ActionResult Index()
        {
            return View();
        }

        private CheckInbll checkInbll = new CheckInbll();


        #region 登记记录 

        public string GetCustomHistory(int customid, int stauts)
        {
            return checkInbll.GetCustomHistory(customid, stauts);
        }

        #endregion

    }
}
