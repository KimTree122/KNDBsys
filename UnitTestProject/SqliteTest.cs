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
        public void TestDB()
        {
            SugarTest st = new SugarTest();
            List<Goods> obj = st.testsql() as List<Goods>;
            Assert.AreEqual(obj[0].Gname, "kim");
        }
    }
}
