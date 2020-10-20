using KNDBsys.Common;
using KNDBsys.Model.BaseInfo;
using KNDBsys.Model.ViewModel;
using KNDBsys.Model.Work;
using KNDBsys.Service.WorkSer.WorkCurdSer;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Service.WorkSer
{
    public class CheckInbll:BaseADOSer
    {
        private CheckInMTSer checkInMTSer = new CheckInMTSer();
        private CheckInDTSer checkInDTSer = new CheckInDTSer();
        private QRnumberSer qRnumberSer = new QRnumberSer();

        

        //public string GetCheckInMT(int customid,int stauts)
        //{
        //    List<CheckInMT> ciMT = CheckInMTDb.GetList( c => 
        //    c.delflag == false & c.CustomID == customid ).ToList();
        //    if (stauts == 0)
        //    {
        //        ciMT = ciMT.Where(e => e.ServerStauts == stauts).ToList();
        //    }
        //    return DataSwitch.HttpPostList<CheckInMT>(ciMT);
        //}

        public string GetCustomidByQR(string qrcode)
        {
            CheckInMT mT = checkInMTSer.GetCheckInMTByQR(qrcode);
            string customid = General.strFail;
            if (mT.id != 0) customid = mT.CustomID.ToString();
            return DataSwitch.HttpPostMsg(customid);
        }

        //public string GetCheckInDT(int checkinid)
        //{
        //    List<CheckInDT> ciDT = CheckInDTDb.GetList(c =>
        //   c.delflag == false & c.CheckInID == checkinid).OrderBy(k => k.CheckData).ToList();
        //    return DataSwitch.HttpPostList<CheckInDT>(ciDT);
        //}

        public string GetCheckInMTDT(int checkinmtid)
        {
            CheckInMT mT = checkInMTSer.GetCheckInMTByid(checkinmtid);
            List <CheckInDT> dtlist = checkInDTSer.GetCheckInDTs(checkinmtid);
            return DataSwitch.HttpPostData<CheckInDT,CheckInMT>(dtlist,mT);
        }

        public string AddCheckInMT(string checkinjson)
        {
            string str = checkInMTSer.AddEntity(checkinjson);
            return str;
        }

        public string AddcheckInDT(string checkindt)
        {
            string str = checkInDTSer.AddEntity(checkindt);
            return str;
        }

        public string UpdateCheckInMT(string checkinmt)
        {
            string str = checkInMTSer.UpdateEntity(checkinmt);
            return str;
        }

        public string UpdateCheckInDT(string checkindt)
        {
            string str = checkInDTSer.UpdateEntity(checkindt);
            return str;
        }

        //public string GetCustomHistory(int customid, int stauts)
        //{
        //    var q = Db.Queryable<CheckInMT, ServerType, Sysdic>(
        //        (cm, st, sd) => new object[] {
        //            JoinType.Left, cm.ServerTypeID == st.id,
        //            JoinType.Left,cm.ServerStauts == sd.id }
        //        ).Where((cm, st) => cm.delflag == false & cm.CustomID == customid)
        //        .Select((cm, st, sd) => new { cm.id, st.TreeName, cm.CheckDate, sd.Dicval, cm.FinishDate, cm.ServerStauts }).ToList();

        //    List<CustomHistoryVM> cvm = new List<CustomHistoryVM>();
        //    foreach (var vm in q)
        //    {
        //        CustomHistoryVM ch = new CustomHistoryVM {
        //            id = vm.id,
        //            CheckDate = vm.CheckDate,
        //            Dicname = vm.Dicval,
        //            FinishDate = vm.FinishDate,
        //            TypeName = vm.TreeName,
        //            ServerStauts =(int) vm.ServerStauts
        //        };
        //        cvm.Add(ch);
        //    }

        //    if (stauts != 0) cvm = cvm.Where(c => c.ServerStauts == stauts).ToList();
        //    cvm = cvm.OrderByDescending(c => c.id).ToList();
        //    return DataSwitch.HttpPostList<CustomHistoryVM>(cvm);
        //}

        public string GetQRnumber()
        {
            return qRnumberSer.GetNextNumber();
        }

    }
}
