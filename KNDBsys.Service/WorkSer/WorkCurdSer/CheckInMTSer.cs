using KNDBsys.Model;
using KNDBsys.Model.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Service.WorkSer
{
    public class CheckInMTSer : CurdService<CheckInMT>
    {
        public override void SetDbset(DbContext db)
        {
            dbSet = db.CheckInMTDb; 
        }

        public CheckInMT GetCheckInMTByid(int id)
        {
            CheckInMT q = dbSet.GetById(id);
            return q;
        }

        public CheckInMT GetCheckInMTByQR(string qrcode)
        {
            var q = dbSet.GetList(c => c.QRcode == qrcode);
            if (q.Count > 0)
            {
                CheckInMT mT = q.First();
                return mT;
            }
            return new CheckInMT();
        }

    }
}
