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
            PostData<string, string> postData = new PostData<string, string>();
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

                filedata.SaveAs(path + saveName);
            }
            catch (Exception e)
            {
                return DataSwitch.HttpPostMsg(e.Message, 0);
            }
            return DataSwitch.HttpPostMsg("上传成功", 1);
        }
    }
}
