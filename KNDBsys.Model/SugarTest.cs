using KNDBsys.Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Model
{
    public class SugarTest
    {
        public object testsql(string tablename)
        {
            //var db =new SugarADO().sc;
            SugarADO sa = new SugarADO();
            var db = sa.GetInstance();

            db.DbFirst.Where(tablename).CreateClassFile(@"C:\DataBase\SqlClz");

            return 1;
        }

        public object testview()
        {
            SugarADO sugar = new SugarADO();
            var db = sugar.GetInstance();
            string sql = string.Format("select * from userauthview where  userid = 1");
            var authlist = db.SqlQueryable<Authority>(sql).ToList();

            return authlist;
        }
    }
}
