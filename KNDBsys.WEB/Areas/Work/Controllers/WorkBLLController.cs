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

        public string GetCheckInByMTid(int checkinmtid)
        {
            return checkInbll.GetCheckInMTDT(checkinmtid);
        }

        public string GetCustomidByQR(string qrcode)
        {
            return checkInbll.GetCustomidByQR(qrcode);
        }

        public string AddCheckInMT(string checkinmt)
        {
            return checkInbll.AddCheckInMT(checkinmt);
        }

        public string UpdateCheckInMT(string checkinmt)
        {
            return checkInbll.UpdateCheckInMT(checkinmt);
        }

        public string AddCheckInDT(string checkindt)
        {
            return checkInbll.AddcheckInDT(checkindt);
        }

        public string UpdateCheckInDT(string checkindt)
        {
            return checkInbll.UpdateCheckInDT(checkindt);
        }

        #endregion

        #region 服务号
        public string GetSerNumber(string userid)
        {
            return checkInbll.GetQRnumber();
        }
        #endregion
    }
}
