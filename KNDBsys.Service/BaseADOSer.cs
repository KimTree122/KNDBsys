using KNDBsys.Common;
using KNDBsys.DAL;
using KNDBsys.Model;
using KNDBsys.Model.BaseInfo;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Service
{
    public class BaseADOSer
    {
        public T GetEntity<T>(string json)
        {
            T t = DataSwitch.JsonToObj<T>(json);
            return t;
        }

        public List<T> GetEntities<T>(string json)
        {
            List<T> list = DataSwitch.JsonToList<T>(json);
            return list;
        }

        public PostData<T, S> GetPostData<T, S>(string json)
        {
            PostData<T, S> postData = DataSwitch.JsonToObj<PostData<T, S>>(json);
            return postData;
        }
    }
}
