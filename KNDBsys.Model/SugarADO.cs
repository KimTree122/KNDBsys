using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace KNDBsys.Model
{
    public  class SugarADO
    {
        public static string ConnectionString = "Data Source=192.168.61.93;Initial Catalog=sysDB;User ID=sa;password=147852";

        public SqlSugarClient GetInstance()
        {
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = SugarADO.ConnectionString,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true
            });
            return db;
        }
    }
}
