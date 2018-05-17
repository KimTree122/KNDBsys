using KNDBsys.BLL.BaseInfo;
using KNDBsys.IBLL.BaseInfo;
using KNDBsys.Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Service.BaseInfoSer
{
    public class DictionarySer
    {
        private ISysDicService cSDic = new SysDicService();

        public string AddDictionary(string dic)
        {
            Sysdic csDic = DataSwitch.JsonToObj<Sysdic>(dic);
            if (cSDic == null) return General.strFail;
            int count = cSDic.Add(csDic).id;
            if (count > 0) return DataSwitch.HttpPostMsg(count);
            return DataSwitch.HttpPostMsg(General.strFail);
        }

        public string GetDicbytype(string type)
        {
            List<Sysdic> cS = cSDic.GetEntities(d => d.Dicname == type).OrderBy(d => d.Dicsetp).ToList();
            return DataSwitch.HttpPostList<Sysdic>(cS);
        }

        public string UpdateDictionary(string dic)
        {
            Sysdic csDic = DataSwitch.JsonToObj<Sysdic>(dic);
            if (cSDic == null) return General.strFail;
            bool count = cSDic.Update(csDic);
            return DataSwitch.HttpPostMsg(count ? General.strSucess:General.strFail);
        }

        public string DeleteSysdic(string dic)
        {
            Sysdic sysDic = DataSwitch.JsonToObj<Sysdic>(dic);
            if (sysDic == null) return General.strFail;
            bool count = cSDic.Del(sysDic);
            return DataSwitch.HttpPostMsg(count ? General.strSucess : General.strFail);
        }

    }
}
