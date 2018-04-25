using InitData.sqliteData;
using KNDBsys.BLL.BaseInfo;
using KNDBsys.IBLL.BaseInfo;
using KNDBsys.Model;
using KNDBsys.Model.BaseInfo;
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
            string sql = string.Format("CREATE table Sysdic (id integer PRIMARY KEY autoincrement, Dicname varchar(50), Dickey varchar(50),Dicval varchar(50),DicMeno varchar(50) ,Dicsetp int )");
            int count = sh.SQLiteNonQuery(sql);
            Assert.AreEqual("0", count + "");
        }


        [TestMethod]
        public void GetUserInfo()
        {
            UserInfoService uis = new UserInfoService();
            List<UserInfo> userInfos = uis.GetEntities(ui => ui.Uname == "kim");
            int count = userInfos.Count();

            Assert.AreEqual(count, 2);

        }


        [TestMethod]
        public void InitDB()
        {
            SQLHelper sh = new SQLHelper();

            Assert.AreEqual(sh.path, "kim");
        }

        [TestMethod]
        public void CreatClazz()
        {
            SugarTest st = new SugarTest();
            
            List<Goods> obj = st.testsql("Sysdic") as List<Goods>;
            Assert.AreEqual(obj.Count, 1);
        }
    }
}
