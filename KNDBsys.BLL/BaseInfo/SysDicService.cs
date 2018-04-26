using KNDBsys.DAL.BaseInfo;
using KNDBsys.IBLL.BaseInfo;
using KNDBsys.Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.BLL.BaseInfo
{
    public class SysDicService : BaseServiceBLL<Sysdic>, ISysDicService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = new SysDicDal();
        }
    }
}
