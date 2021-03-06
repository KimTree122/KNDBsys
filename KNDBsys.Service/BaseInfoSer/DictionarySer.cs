﻿using KNDBsys.BLL.BaseInfo;
using KNDBsys.Common;
using KNDBsys.IBLL.BaseInfo;
using KNDBsys.Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Service.BaseInfoSer
{
    /// <summary>
    /// 字典服务，未继承通用增删查改服务
    /// </summary>
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
            List<Sysdic> cS = GetDicByTypeName(type);
            return DataSwitch.HttpPostList<Sysdic>(cS);
        }

        public List<Sysdic> GetDicByTypeName(string type)
        {
            return cSDic.GetEntities(d => d.Dicname == type).OrderBy(d => d.Dicsetp).ToList();
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
