using KNDBsys.Model.BaseInfo;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace KNDBsys.Model
{
    public class DbSet<T>: SimpleClient<T> where T :class,new()
    {
        public DbSet(SqlSugarClient context) : base(context) { }

        public List<T> GetByID(dynamic[] ids)
        {
            return Context.Queryable<T>().In(ids).ToList();
        }
    }


    public class DbContext
    {
        public SqlSugarClient Db;
        private string str = ConfigurationManager.AppSettings["dbpath"];

        public DbContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig { ConnectionString = str, DbType = DbType.Sqlite, IsAutoCloseConnection = true });
            
        }

        public DbSet<UserInfo> UserInfoDb { get { return new DbSet<UserInfo>(Db); } }

    }

}
