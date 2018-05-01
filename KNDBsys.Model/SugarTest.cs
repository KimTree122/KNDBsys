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

            List<Goods> all = db.Queryable<Goods>().ToList();

            return all.Count();

        }
    }
}
