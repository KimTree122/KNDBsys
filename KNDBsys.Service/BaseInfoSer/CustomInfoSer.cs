﻿using KNDBsys.Model;
using KNDBsys.Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Service.BaseInfoSer
{
    public class CustomInfoSer : CurdService<CustomInfo>
    {
        public override void SetDbset(DbContext db)
        {
            dbSet = db.CustomInfoDb;
        }

        public string GetAllCustomInfo(string userid)
        {
            var list = dbSet.GetList(s => s.delflag == false).ToList();
            return DataSwitch.HttpPostData<CustomInfo>(list);
        }

        public string FindSameCumtomByTel(string tel)
        {
            int count = dbSet.Count( c=> c.CTel == tel);
            return DataSwitch.HttpPostData(count);
        }

    }
}