using KNDBsys.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace KNDBsys.Common.FileManager
{
    public class FilePresent
    {

        public string UpLoadFile(HttpPostedFileBase filedata, string path)
        {
            
            if (filedata == null)
            {
                return DataSwitch.HttpPostMsg("未提交上传文件",0);
            }
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = Path.GetFileName(filedata.FileName);// 原始文件名称
                string fileExtension = Path.GetExtension(fileName); // 文件扩展名
                string saveName = Guid.NewGuid().ToString("P") + fileExtension; // 保存文件名称

                filedata.SaveAs(path+"/"+ saveName);
            }
            catch (Exception e)
            {
                return DataSwitch.HttpPostMsg(e.Message, 0);
            }
            return DataSwitch.HttpPostMsg("上传成功", 1);
        }

        //弃用,流式数据不能生成对应文件(包含上传参数)
        public string UpLoadStreamFile(Stream sr,string uploadpath, string filename)
        {
            try
            {
                int size = 1024;
                byte[] by = new byte[size];
                int length = sr.Read(by, 0, size);
                if (!Directory.Exists(uploadpath))
                {
                    Directory.CreateDirectory(uploadpath);
                }
                FileStream fs = new FileStream(uploadpath + "//" + filename, FileMode.Create, FileAccess.Write);
                while (length > 0)
                {
                    fs.Write(by, 0, by.Length);
                    length = sr.Read(by, 0, size);
                }
                fs.Close();
                sr.Close();

            }
            catch (Exception e)
            {
                return DataSwitch.HttpPostMsg(e.Message, 0);
            }
            
            return DataSwitch.HttpPostMsg("上传成功", 1);
        }


        public FileStream GetFile(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            return fs;
        }

    }
}
