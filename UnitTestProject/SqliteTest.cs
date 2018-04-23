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
        public void CreatTable()
        {
            SQLHelper sh = new SQLHelper();
            string sql = string.Format("CREATE table CSDicTionary (id integer PRIMARY KEY autoincrement, DicType varchar(50), DicKeys varchar(50),DicVlaue varchar(50),DicMeno varchar(50) ,DicOrder float )");
            int count = sh.SQLiteNonQuery(sql);
            Assert.AreEqual("1", count + "");
        }


        [TestMethod]
        public void AddUserInfo()
        {
            UserInfoService uis = new UserInfoService();
            UserInfo ui = new UserInfo { Uname = "kim", UPost = "kim", delflag = true };

            UserInfo insui = uis.Add(ui);

            Assert.AreEqual(ui.Uname, insui.Uname);

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
            
            List<Goods> obj = st.testsql("UserAuth") as List<Goods>;
            Assert.AreEqual(obj[0].Gname, "kim");
        }
    }
}
