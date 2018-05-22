using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace KNDBsys.Service.FileSer
{
    public class FileLoadSer
    {
        public string GetVerlist(string path)
        {
            List<string> strlist = new List<string>();
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                strlist.Add(line);
            }
            string msg = strlist[0];
            msg = msg.Substring(msg.IndexOf(','));
            string post = DataSwitch.HttpPostList<string>(strlist,msg);

            return post;
        }
    }
}
