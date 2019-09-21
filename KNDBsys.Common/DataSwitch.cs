using KNDBsys.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Common
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

        public static T JsonToObject2<T>(string json)
        {
            T t = ExpressionGenericMapper.Map<string, T>(json);


            return t;
        }


        public static string DataToJson_DES( object obj )
        {
            string str = JsonConvert.SerializeObject(obj);

            //str = Secret_string.EncryptDES(str);

            return str;
        }

        public static string DataToJson(object obj)
        {
            string str = JsonConvert.SerializeObject(obj);
            return str;
        }


        public static string HttpPostData<L,E> ( List<L> dlist, E obj = default(E), string msg = "")
        {
            PostData<L,E> postData = new PostData<L,E>
            {
                rows = dlist,
                total = dlist.Count,
                Msg = msg,
                Entity = obj
            };

            return DataToJson_DES(postData);
        }

        public static string HttpPostList<T>(List<T> dlist,string msg = "")
        {
            PostData<T, DBNull> post = new PostData<T, DBNull>
            {
                rows = dlist,
                total = dlist.Count,
                Msg = msg
            };
            return DataToJson_DES(post);
        }

        public static string HttpPostEntity<E>(E s,int count = 0, string msg = "")
        {
            if (s != null) count = 1;
            PostData<DBNull, E> post = new PostData<DBNull, E> {
                Entity = s,
                total = count,
                Msg = msg
            };
            return DataToJson_DES(post);
        }

        public static string HttpPostMsg(object msg,int count = 1)
        {
            PostData<DBNull, DBNull> post = new PostData<DBNull, DBNull>
            {
                total = count,
                Msg = msg.ToString()
            };
            return DataToJson_DES(post);
        }

    }
}
