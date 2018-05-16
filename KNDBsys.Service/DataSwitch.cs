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
            try {
                return JsonConvert.DeserializeObject<T>(jsonobj);
            }
            catch (Exception) {
                return default(T);
            }
        }

        public static string DataToObject( object list )
        {
            return JsonConvert.SerializeObject(list);
        }

        public static string HttpPostData<L,E> ( List<L> dlist, E obj = default(E), string msg = "")
        {
            PostData<L,E> postData = new PostData<L,E>
            {
                DList = dlist,
                MCount = dlist.Count,
                Msg = msg,
                Entity = obj
            };

            return DataToObject(postData);
        }

        public static string HttpPostList<T>(List<T> dlist,string msg = "")
        {
            PostData<T, DBNull> post = new PostData<T, DBNull>
            {
                DList = dlist,
                MCount = dlist.Count,
                Msg = msg
            };
            return DataToObject(post);
        }

        public static string HttpPostEntity<E>(E s,string msg = "")
        {
            PostData<DBNull, E> post = new PostData<DBNull, E> {
                Entity = s,
                MCount = 1,
                Msg = msg
            };
            return DataToObject(post);
        }

        public static string HttpPostMsg(object msg)
        {
            PostData<DBNull, DBNull> post = new PostData<DBNull, DBNull>
            {
                MCount = 1,
                Msg = msg.ToString()
            };
            return DataToObject(post);
        }

    }
}
