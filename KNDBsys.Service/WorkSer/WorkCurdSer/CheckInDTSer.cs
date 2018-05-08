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
        public override void SetDbset(DbContext db)
        {
            dbSet = db.CheckInDTDb;
        }
    }
}
