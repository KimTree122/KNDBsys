using KNDBsys.Model;
using KNDBsys.Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Service.BaseInfoSer
{
    public class ServerTypeSer : CurdService<ServerType>
    {
        public override void SetDbset(DbContext db)
        {
            dbSet = db.ServerTypeDb;
        }

        public string GetAllServerType(string userid)
        {
            var list = dbSet.GetList(s => s.delflag == false ).ToList();
            return DataSwitch.HttpPostList<ServerType>(list);
        }

    }
}
