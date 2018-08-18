using KNDBsys.DAL;
using KNDBsys.Model;
using KNDBsys.Model.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Service.WorkSer.WorkCurdSer
{
    public class CheckInDTSer : CurdService<CheckInDT>
    {
        public override void SetDbset(SugarDBContext db)
        {
            dbSet = db.CheckInDTDb;
        }

        public List<CheckInDT> GetCheckInDTs(int checkinmtid)
        {
            List<CheckInDT> q = dbSet.GetList( d => d.CheckInID == checkinmtid ).ToList();
            return q;
        }
    }
}
