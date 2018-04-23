using KNDBsys.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Service
{
    public class DataSwitch
    {
        public static List<T> JsonToList<T>(string jsonlist)
        {
            try{ return JsonConvert.DeserializeObject<List<T>>(jsonlist); }
            catch (Exception){ return default(List<T>); }
        }

        public static T JsonToObj<T>(string jsonobj)
        {
            try { return JsonConvert.DeserializeObject<T>(jsonobj); }
            catch (Exception) { return default(T); }
        }

        public static string DataToObject( object list )
        {
            return JsonConvert.SerializeObject(list);
        }

        public static string HttpPostData<T,S> ( List<T> dlist,string msg = "", S obj = default(S) )
        {
            PostData<T,S> postData = new PostData<T,S>
            {
                DList = dlist,
                MCount = dlist.Count,
                Msg = msg,
                Obj = obj
            };

            return DataToObject(postData);
        }

        public static string HttpPostData<T>(List<T> dlist,string msg = "")
        {
            PostData<T, DBNull> post = new PostData<T, DBNull>
            {
                DList = dlist,
                MCount = dlist.Count,
                Msg = msg
            };
            return DataToObject(post);
        }
    }
}
