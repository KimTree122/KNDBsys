﻿using KNDBsys.Common;
using KNDBsys.DAL;
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
        public override void SetDbset(SugarDBContext<QRnumber> db)
        {
            dbSet = db.EntityDb;
        }

        public string GetNextNumber()
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

            string qrnum = nday.ToString("yyyyMMdd")+upd.ToString().PadLeft(3,'0');
            qrnum ="CS"+ qrnum.Substring(2);
            return DataSwitch.HttpPostMsg(qrnum);
        }
    }
}
