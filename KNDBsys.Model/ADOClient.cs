﻿using KNDBsys.Model.BaseInfo;
using SqlSugar;
using System;
using System.Collections.Generic;
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

        public DbContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig { ConnectionString ="", DbType = DbType.Sqlite, IsAutoCloseConnection = true });
            
        }

        public DbSet<UserInfo> UserInfoDb { get { return new DbSet<UserInfo>(Db); } }



    }

}
