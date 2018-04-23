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
        private ICSDicTionaryService cSDic = new CSDicTionaryService();

        public string AddDictionary(string dic)
        {
            CSDicTionary csDic = DataSwitch.JsonToObj<CSDicTionary>(dic);
            if (cSDic == null) return General.reFail;
            return cSDic.Add(csDic).id + "";
        }

        public string GetDicbytype(string type)
        {
            List<CSDicTionary> cS = cSDic.GetEntities(d => d.DicType == type);
            return DataSwitch.HttpPostData<CSDicTionary>(cS);
        }

    }
}
