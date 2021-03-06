﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace InitData.sqliteData
{
    public class SQLiteHelper
    {
        public SQLiteConnection conn;
        public SQLiteCommand cmd;
        public readonly string path;

        public SQLiteHelper()
        {
            path = "C:\\DataBase\\cs.db";
            CheckDB();
        }


        private void CheckDB()
        {
            bool cf = File.Exists(path);
            if (!cf) SQLiteConnection.CreateFile(path);
            conn = new SQLiteConnection("DataSource=" + path);
            conn.Open();
            cmd = new SQLiteCommand();
            cmd.Connection = conn;
            if (!cf) CreatTable();
        }

        public void CreatTable()
        {
            string sql = string.Format("CREATE table UserInfo (id integer PRIMARY KEY autoincrement, Uname varchar(50),Uaccount varchar(50), Upwd varchar(50),Utel varchar(50), UPost varchar(50), delflag bool) ");
            SQLiteNonQuery(sql);

            sql = string.Format("CREATE table CustomInfo (id integer PRIMARY KEY autoincrement, Cname varchar(50), CTel varchar(50), Caddress varchar(50)) ");
            SQLiteNonQuery(sql);

            sql = string.Format("CREATE table Authority (id integer PRIMARY KEY autoincrement, TreeName varchar(50),Path varchar(50), ParentID int, AuthTypeID int, AuthTypeName varchar(50) ,Imageid  int , AOrder varchar(50))");
            SQLiteNonQuery(sql);

            sql = string.Format("CREATE TABLE UserAuth (id integer PRIMARY KEY autoincrement, UserID int, AuthID int)");
            SQLiteNonQuery(sql);

            sql = string.Format("CREATE table Sysdic (id integer PRIMARY KEY autoincrement, Dicname varchar(50), Dickey varchar(50),Dicval varchar(50),DicMeno varchar(50) ,Dicsetp int )");
            SQLiteNonQuery(sql);

            sql = string.Format("CREATE VIEW [UserAuthView] AS select a.[UserID], b.* from userauth as a left join authority as b on a.[authid] = b.[id]");
            SQLiteNonQuery(sql);

            sql = string.Format("CREATE TABLE [ServerType] ([id] integer PRIMARY KEY AUTOINCREMENT,[ParentID] int,[TreeName] varchar(50),[TypeName] varchar(50),[Typeid] varchar(50),[TMemo] varchar(50),[TOrder] varchar(50),[delflag] bool)");
            SQLiteNonQuery(sql);

            sql = string.Format("CREATE table CheckInMT (id integer PRIMARY KEY autoincrement, ServerTypeID int, CustomID int,ServerStauts int,CheckDate varchar(50),FinishDate varchar(50),Memo varchar(50),QRcode varchar(50), delflag bool )");
            SQLiteNonQuery(sql);

            sql = string.Format("CREATE table CheckInDT (id integer PRIMARY KEY autoincrement, CheckInID int, GoodsStauts varchar(50),Rating int ,CheckData varchar(50),Meno varchar(50),ServerPay int, delflag bool )");
            SQLiteNonQuery(sql);

            sql = string.Format("CREATE table QRnumber (id integer PRIMARY KEY autoincrement, QRday varchar(50), QROrder int )");
            SQLiteNonQuery(sql);

            sql = string.Format("CREATE table SysVer (id integer PRIMARY KEY autoincrement, sysver varchar(50), upgradedate  varchar(50), filelist  varchar(50), note  varchar(50), programtype  varchar(50) )");
            SQLiteNonQuery(sql);
        }

        /// <summary>
        /// 执行SQL返回datatable
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>datatable</returns>
        public DataTable SQLiteGetTable(string sql)
        {
            SQLiteDataAdapter command = new SQLiteDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            command.Fill(ds, "T0");
            return ds.Tables[0];
        }

        /// <summary>
        /// 执行SQL返回受影响行数
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>受影响行数</returns>
        public int SQLiteNonQuery(string sql)
        {
            cmd.CommandText = sql;
            return cmd.ExecuteNonQuery();
            //SQLiteCommand command = new SQLiteCommand(sql, conn);
            //return command.ExecuteNonQuery();

        }

        /// <summary>
        /// 执行SQL返回第一行第一列数据
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>受影响行数</returns>
        public object SQLExecuteScalar(string sql)
        {
            cmd.CommandText = sql;
            return cmd.ExecuteScalar();
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>
        public void ExecuteSqlTran(ArrayList SQLStringList)
        {
            SQLiteTransaction tx = conn.BeginTransaction();
            cmd.Transaction = tx;
            try
            {
                for (int n = 0; n < SQLStringList.Count; n++)
                {
                    string strsql = SQLStringList[n].ToString();
                    if (strsql.Trim().Length > 1)
                    {
                        cmd.CommandText = strsql;
                        cmd.ExecuteNonQuery();
                    }
                }
                tx.Commit();
            }
            catch (System.Data.SQLite.SQLiteException E)
            {
                tx.Rollback();
                throw new Exception(E.Message);
            }
        }
    }
}
