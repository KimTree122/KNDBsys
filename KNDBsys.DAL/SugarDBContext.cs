using KNDBsys.Model.BaseInfo;
using KNDBsys.Model.Work;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace KNDBsys.DAL
{

    public class DbSet<T> : SimpleClient<T> where T : class, new()
    {
        public DbSet(SqlSugarClient context) : base(context) { }

        public List<T> GetByID(dynamic[] ids)
        {
            return Context.Queryable<T>().In(ids).ToList();
        }
    }

    public class SugarDBContext
    {
        public SqlSugarClient Db;
        private string str = ConfigurationManager.AppSettings["dbpath"];
        //private string str = @"DataSource=C:\DataBase\cs.db";

        public SugarDBContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig { ConnectionString = str, DbType = DbType.Sqlite, IsAutoCloseConnection = true });

        }

        public DbSet<UserInfo> UserInfoDb { get { return new DbSet<UserInfo>(Db); } }
        public DbSet<Authority> AuthorityDb { get { return new DbSet<Authority>(Db); } }
        public DbSet<UserAuth> UserauthDb { get { return new DbSet<UserAuth>(Db); } }
        public DbSet<ServerType> ServerTypeDb { get { return new DbSet<ServerType>(Db); } }
        public DbSet<CustomInfo> CustomInfoDb { get { return new DbSet<CustomInfo>(Db); } }
        public DbSet<SysVer> SysVerDb { get { return new DbSet<SysVer>(Db); } }

        public DbSet<CheckInMT> CheckInMTDb { get { return new DbSet<CheckInMT>(Db); } }
        public DbSet<CheckInDT> CheckInDTDb { get { return new DbSet<CheckInDT>(Db); } }
        public DbSet<QRnumber> QRnumberDb { get { return new DbSet<QRnumber>(Db); } }
    }
}
