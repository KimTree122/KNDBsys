using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Model
{
    public  class SugarADO
    {
        public static string ConnectionString = @"DataSource=C:\DataBase\cs.db";

        public  SqlSugarClient GetInstance()
        {
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig() { 
                ConnectionString = SugarADO.ConnectionString, DbType = DbType.Sqlite, IsAutoCloseConnection = true });
            return db;
        }


    }
}
