using KNDBsys.Common;
using KNDBsys.Model;
using KNDBsys.Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Service.WorkSer
{
    public class QRnumberSer : CurdService<QRnumber>
    {
        public override void SetDbset(DbContext db)
        {
            dbSet = db.QRnumberDb;
        }

        public int GetNextNumber()
        {
            var q = dbSet.GetById(1);
            DateTime qrday = q.QRday.StrToDateTime();
            DateTime nday = DateTime.Now.Date;
            int num = nday.CompareTo(qrday);
            if (num != 0)
            {
                q.QRday = nday.ToShortDateString();
                q.QROrder = 1;
            }
            else
            {
                q.QROrder++;
            }

            bool count = dbSet.Update(q);
            int upd = count ? (int)q.QROrder : 0;
            return upd;
        }


        public QRnumber GetQRnumber()
        {
            return dbSet.GetById(1);
        }


    }
}
