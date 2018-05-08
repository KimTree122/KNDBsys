using KNDBsys.Model.BaseInfo;
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
            return DataSwitch.HttpPostData<CheckInMT>(ciMT);
        }

        public string GetCheckInInfo(int customid, int stauts)
        {
            var q = Db.Queryable<CheckInMT, CustomInfo, ServerType>(
                (cm,ci, st)=> new object[] { cm.CustomID == ci.id, cm.ServerTypeID == st.id }
                ).Where((cm, ci, st) => cm.delflag == false & cm.CustomID == customid )
                .Select((cm, ci, st) => new  { cm.id,  });
            return "";
        }


        public string GetCheckInDT(int checkinid)
        {
            List<CheckInDT> ciDT = CheckInDTDb.GetList(c =>
           c.delflag == false & c.CheckInID == checkinid).OrderBy(k => k.CheckData).ToList();
            return DataSwitch.HttpPostData<CheckInDT>(ciDT);
        }

    }
}
