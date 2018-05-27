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


        public FileStreamResult DownFile(string filePath, string fileName)
        {
            //string absoluFilePath = Server.MapPath(downloadpath + filePath);
            string path = downloadpath + filePath;
            return File(new FileStream(path, FileMode.Open), "application/octet-stream", Server.UrlEncode(fileName));
        }

        public ActionResult DownFile2(string filePath, string fileName)
        {
            filePath = Server.MapPath(downloadpath + filePath);
            FileStream fs = new FileStream(filePath, FileMode.Open);
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

        public FileResult MyFile()
        {
            string root = Server.MapPath("~/App_Data");
            string fileName = "test.jpg";
            string filePath = Path.Combine(root, fileName);
            //string s = MimeMapping.GetMimeMapping(fileName);
            string s = "";
            return File(filePath, s, Path.GetFileName(filePath));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public string UpLoadFile(string filename,string ver)
        {
            Stream sr = Request.InputStream;
            string str = string.Empty;
            if (sr.Length > 0)
            {
                return filePresent.UpLoadStreamFile(sr, uploadpath+"\\"+ver, filename);
            }
            return str;
        }
    }
}
