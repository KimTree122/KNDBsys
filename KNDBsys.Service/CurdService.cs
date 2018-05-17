using KNDBsys.BLL;
using KNDBsys.IBLL;
using KNDBsys.Model;
using KNDBsys.Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Service
{
    public abstract class CurdService<T> where T : class,new ()
    {
        public DbSet<T> dbSet { get; set; }
        public DbContext db { get; set; }

        public CurdService()
        {
            db = new DbContext();
            SetDbset(db);
        }

        public abstract void SetDbset(DbContext db);


        /// <summary>
        /// 增加数据
        /// </summary>
        /// <param name="json">实体类json</param>
        /// <returns>Postdata msg id</returns>
        public string AddEntity(string json)
        {
            T entity = DataSwitch.JsonToObj<T>(json);
            if (entity == null) return General.strFail;
            int count = dbSet.InsertReturnIdentity(entity);
            return DataSwitch.HttpPostMsg(count);
        }

        public int AddEntity(T Entity)
        {
            int count = dbSet.InsertReturnIdentity(Entity);
            return count;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="json">实体类json</param>
        /// <returns>postdata msg 1,0</returns>
        public string UpdateEntity(string json)
        {
            T entity = DataSwitch.JsonToObj<T>(json);
            if (entity == null) return General.strFail;
            bool count = dbSet.Update(entity);
            return DataSwitch.HttpPostMsg(count ? General.strSucess : General.strFail);
        }

        public bool UpdateEntity(T Entity)
        {
            bool count = dbSet.Update(Entity);
            return count;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="json">实体类json</param>
        /// <returns>postdata msg 1,0</returns>
        public string DeleteEntity(string json)
        {
            T entity = DataSwitch.JsonToObj<T>(json);
            if (entity == null) return General.strFail;
            bool count = dbSet.Delete(entity);
            return DataSwitch.HttpPostMsg(count ? General.strSucess : General.strFail);
        }

        public bool DeleteEntity(T Entity)
        {
            bool count = dbSet.Delete(Entity);
            return count;
        }

    }
}
