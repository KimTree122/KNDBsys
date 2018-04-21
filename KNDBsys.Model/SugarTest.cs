using KNDBsys.Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Model
{
    public class SugarTest
    {
        public object testsql()
        {
            //var db =new SugarADO().sc;
            SugarADO sa = new SugarADO();
            var db = sa.GetInstance();

            db.DbFirst.Where("CustomInfo").CreateClassFile(@"C:\DataBase\SqlClz");

            List<Goods> all = db.Queryable<Goods>().ToList();
            return all;
        }
    }
}
