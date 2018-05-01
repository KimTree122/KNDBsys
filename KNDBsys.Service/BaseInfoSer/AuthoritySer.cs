﻿using KNDBsys.BLL.BaseInfo;
using KNDBsys.IBLL.BaseInfo;
using KNDBsys.Model;
using KNDBsys.Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Service.BaseInfoSer
{
    public class AuthoritySer:CurdService<Authority>
    {
        public override void SetDbset(DbContext db)
        {
            dbSet = db.AuthorityDb;
        }

        public string getAllAuthority()
        {
            var authlist = dbSet.GetList(e => e.id != 0);
            return DataSwitch.HttpPostData<Authority>( authlist);
        }
    }
}
