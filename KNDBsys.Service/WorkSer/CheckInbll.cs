using KNDBsys.Model.BaseInfo;
using KNDBsys.Model.ViewModel;
using KNDBsys.Model.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Service.WorkSer
{
    public class CheckInbll:BaseADOSer
    {
        public string InsertCheckInMT(string checkinjson)
        {
            CheckInMT checkInMT = GetEntity<CheckInMT>(checkinjson);
            if (checkInMT != null)
            {
                CheckInMTDb.Insert(checkInMT);
            }
            return "";
        }

        public string GetCheckInMT(int customid,int stauts)
        {
            List<CheckInMT> ciMT = CheckInMTDb.GetList( c => 
            c.delflag == false & c.CustomID == customid ).ToList();
            if (stauts == 0)
            {
                ciMT = ciMT.Where(e => e.ServerStauts == stauts).ToList();
            }
            return DataSwitch.HttpPostList<CheckInMT>(ciMT);
        }

        public string GetCustomHistory(int customid, int stauts)
        {
            var q = Db.Queryable<CheckInMT, ServerType, Sysdic>(
                (cm, st, sd) => new object[] { cm.ServerTypeID == st.id, cm.ServerStauts == sd.id }
                ).Where((cm, st) => cm.delflag == false & cm.CustomID == customid)
                .Select((cm, st, sd) => new { cm.id, st.TypeName, cm.CheckDate, sd.Dicname, cm.FinishDate, cm.ServerStauts }).ToList();

            List<CustomHistoryVM> cvm = new List<CustomHistoryVM>();
            foreach (var vm in q)
            {
                CustomHistoryVM ch = new CustomHistoryVM {
                    id = vm.id,
                    CheckDate = vm.CheckDate,
                    Dicname = vm.Dicname,
                    FinishDate = vm.FinishDate,
                    TypeName = vm.TypeName,
                    ServerStauts =(int) vm.ServerStauts
                };
                cvm.Add(ch);
            }

            if (stauts != 0) cvm = cvm.Where(c => c.ServerStauts == stauts).ToList();

            return DataSwitch.HttpPostList<CustomHistoryVM>(cvm);
        }

        public string GetCheckInDT(int checkinid)
        {
            List<CheckInDT> ciDT = CheckInDTDb.GetList(c =>
           c.delflag == false & c.CheckInID == checkinid).OrderBy(k => k.CheckData).ToList();
            return DataSwitch.HttpPostList<CheckInDT>(ciDT);
        }

    }
}
