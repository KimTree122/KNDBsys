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
            string sql = string.Format("CREATE table CheckInMT (id integer PRIMARY KEY autoincrement, ServerTypeID varchar(50), CustomID varchar(50),ServerStauts varchar(50),CheckDate varchar(50),FinishDate varchar(50),Memo varchar(50),delflag bool )");
            int count = sh.SQLiteNonQuery(sql);
            Assert.AreEqual("0", count + "");
        }

        [TestMethod]
        public void CreatClazz()
        {
            SugarTest st = new SugarTest();

            st.testsql("QRnumber");
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
