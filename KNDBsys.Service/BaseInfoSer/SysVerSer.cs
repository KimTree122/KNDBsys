﻿using KNDBsys.Common;
using KNDBsys.DAL;
using KNDBsys.IService;
using KNDBsys.Model;
using KNDBsys.Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Service.BaseInfoSer
{
    public class SysVerSer : CurdService<SysVer>,ISysVerSer
    {
        public override void SetDbset(SugarDBContext<SysVer> db)
        {
            dbSet = db.EntityDb;
        }

        public string GetNewSysVer(string programtype)
        {
            int count = dbSet.GetList(v => v.programtype == programtype).Count;
            if (count > 0)
            {
                var q = dbSet.GetList().OrderByDescending(v => v.id).First().id;
                var sysver = dbSet.GetById(q);
                return DataSwitch.HttpPostEntity<SysVer>(sysver);
            }
            return DataSwitch.HttpPostMsg(General.strFail,General.intFail);
        }

    }
}
