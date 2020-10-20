using KNDBsys.Common;
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
    public class ServerTypeSer : CurdService<ServerType>, IServerTypeSer
    {
        public override void SetDbset(SugarDBContext<ServerType> db)
        {
            dbSet = db.EntityDb;
        }

        public string GetAllServerType(string userid)
        {
            var list = dbSet.GetList(s => s.delflag == false ).ToList();
            return DataSwitch.HttpPostList<ServerType>(list);
        }

    }
}
