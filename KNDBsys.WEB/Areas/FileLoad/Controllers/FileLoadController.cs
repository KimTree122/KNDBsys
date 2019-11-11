using KNDBsys.Common.FileManager;
using KNDBsys.Service.FileSer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace KNDBsys.WEB.Areas.FileLoad.Controllers
{
    public class FileLoadController : Controller
    {
        //
        // GET: /FileLoad/FileLoad/

        private FileLoadSer fileser = new FileLoadSer();
        private static readonly string uploadpath = System.Configuration.ConfigurationManager.AppSettings["fileupload"];
        private static readonly string downloadpath = System.Configuration.ConfigurationManager.AppSettings["filedownload"];

        private FilePresent filePresent = new FilePresent();


        public ActionResult Index()
        {
            return View();
        }

        #region 程序更新


        #endregion

        public ActionResult DownFile(string filePath, string fileName)
        {
            //需要在iis添加虚拟目录
            string abspath = Server.MapPath(downloadpath+filePath+"\\"+fileName);
            FileStream fs = new FileStream(abspath, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(fileName));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
            return new EmptyResult();
        }

        //弃用
        public FileStreamResult DownFile2(string filePath, string fileName)
        {
            string abspath = Server.MapPath(downloadpath + filePath);
            return File(new FileStream(abspath, FileMode.Open), "application/octet-stream", Server.UrlEncode(fileName));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public string UpLoadFile(string filename,string filePath)
        {
            string str = string.Empty;
            //Stream sr = Request.InputStream;
            //if (sr.Length > 0)
            //{
            //    return filePresent.UpLoadStreamFile(sr, uploadpath+"\\"+ver, filename);
            //}
            HttpPostedFileBase hpfb = Request.Files[0];

            str = filePresent.UpLoadFile(hpfb, uploadpath+"\\"+ filePath);

            return str;
        }

        /// <Response分块下载>
        ///  Response分块下载，输出硬盘文件，提供下载 支持大文件、续传、速度限制、资源占用小
        /// </summary>
        /// <param name="fileName">客户端保存的文件名</param>
        /// <param name="filePath">客户端保存的文件路径（包括文件名）</param>
        /// <returns></returns>
        public bool ResponseDownLoad(string fileName, string filePath)
        {
             //fileName = "wenjian.txt";//客户端保存的文件名
             //filePath = "/Upload/wenjian.txt"; //路径（后续从webconfig读取）

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);
            if (fileInfo.Exists == true)
            {
                const long ChunkSize = 1;//100K 每次读取文件，只读取100K，这样可以缓解服务器的压力
                byte[] buffer = new byte[ChunkSize];

                Response.Clear();
                System.IO.FileStream iStream = System.IO.File.OpenRead(filePath);
                long dataLengthToRead = iStream.Length;//获取下载的文件总大小
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName));
                while (dataLengthToRead > 0 && Response.IsClientConnected)
                {
                    int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));//读取的大小
                    Response.OutputStream.Write(buffer, 0, lengthRead);
                    Response.Flush();
                    dataLengthToRead = dataLengthToRead - lengthRead;
                }
                Response.Close();
                return true;
            }
            else
                return false;
        }

        //文件流下载
        public FileStreamResult Filestream_download()
        {
            string fileName = "wenjian.txt";//客户端保存的文件名
            string filePath = Server.MapPath("/Upload/wenjian.txt");//指定文件所在的全路径
            return File(new FileStream(filePath, FileMode.Open), "text/plain",//"text/plain"是文件MIME类型
          fileName);
        }

        //文件下载
        public FileResult File_download()
        {
            string filePath = Server.MapPath("/Upload/wenjian.txt");//路径
            return File(filePath, "text/plain", "wenjian.txt"); //"text/plain"是文件MIME类型,welcome.txt是客户端保存的名字
        }
    }
}
