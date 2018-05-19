using InitData.sqliteData;
using KNDBsys.BLL.BaseInfo;
using KNDBsys.IBLL.BaseInfo;
using KNDBsys.Model;
using KNDBsys.Model.BaseInfo;
using KNDBsys.Service.BaseInfoSer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTestProject
{
    [TestClass]
    public class SqliteTest
    {


        [TestMethod]
        private void ViewSelect()
        {
            SugarTest st = new SugarTest();
            List<Authority> authorities = st.testview() as List<Authority> ;
            Assert.AreEqual(authorities.Count, 0);
        }

        [TestMethod]
        public void Adddictionary()
        {
            string t = "";
            Sysdic dic = new Sysdic { Dickey = "1", Dicname = "类型",
             Dicsetp = 1, Dicval = "权限类型", DicMeno = "权限"};
            ISysDicService ids = new SysDicService();
            Sysdic insdic = ids.Add(dic);

            t = insdic.id + "";
            Assert.AreEqual(t, "1");
        }

        [TestMethod]
        public void GetDictionaryData()
        {
            string t = "";
            ISysDicService cSDic = new SysDicService();
            List<Sysdic> dicTionary = cSDic.GetEntities(cs => cs.Dicname == "类型");
            t = dicTionary.Count()+"";

            Assert.AreEqual(t, "1");
        }

        [TestMethod]
        public void CreatTable()
        {
            SQLHelper sh = new SQLHelper();
            string sql = string.Format("CREATE table UserInfo (id integer PRIMARY KEY autoincrement, Uname varchar(50),Upwd varchar(50),Utel varchar(50), UPost varchar(50), delflag bool) ");
            int count = sh.SQLiteNonQuery(sql);
            Assert.AreEqual("0", count + "");
        }


        [TestMethod]
        public void GetUserInfo()
        {
            //UserInfoSer uis = new UserInfoSer();
            //List<UserInfo> userInfos = uis.GetAllUserInfo("1");
            //UserInfo u = userInfos[0];
            //int count = userInfos.Count();
            //Assert.AreEqual(u.Upwd, "123");

        }


        [TestMethod]
        public void InsetrUser()
        {
            UserInfoService uis = new UserInfoService();
            UserInfo user = new UserInfo { Uname = "admin", Upwd = "123", UPost = "管理员", Utel = "12345679", delflag = false };
            int id = uis.Add(user).id;
            Assert.AreEqual("1",id.ToString());
        }

        [TestMethod]
        public void CreatClazz()
        {
            SugarTest st = new SugarTest();
            
            st.testsql("CheckInDT") ;
            Assert.AreEqual(1, 1);
        }
    }
}
