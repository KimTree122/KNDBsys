using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InitData.sqliteData;

namespace UnitTestProject
{
    [TestClass]
    public class InitData
    {

        [TestMethod]
        public void TestMethod2()
        {

        }


        [TestMethod]
        public void TestMethod1()
        {
            string str1 = "kim";
            string str2 = "kim";

            Assert.AreEqual(str1, str2);
        }

    }
}
