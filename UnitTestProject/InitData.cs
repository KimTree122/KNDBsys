using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InitData.sqliteData;
using KNDBsys.Model;

namespace UnitTestProject
{
    [TestClass]
    public class InitData
    {

        [TestMethod]
        public void CreatTable()
        {
            SQLHelper sh = new SQLHelper();
            string sql = string.Format("CREATE table SysVer (id integer PRIMARY KEY autoincrement, sysver varchar(50), upgradedate  varchar(50), filelist  varchar(50), note  varchar(50), programtype  varchar(50) )");
            int count = sh.SQLiteNonQuery(sql);
            Assert.AreEqual("0", count + "");
        }

        [TestMethod]
        public void CreatClazz()
        {
            SugarTest st = new SugarTest();

            st.testsql("SysVer");
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void InsertQRnumber()
        {
            SQLHelper sh = new SQLHelper();
            string sql = string.Format("insert into qrnumber(qrday,qrorder) values('2018.05.12',1)");
            int count = sh.SQLiteNonQuery(sql);
            Assert.AreEqual("1", count + "");
        }

    }
}
