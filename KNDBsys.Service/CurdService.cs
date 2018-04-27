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
        private DbContext db = new DbContext();

        public CurdService()
        {
            SetDbset(new DbContext());
        }

        public abstract void SetDbset(DbContext db);


        public string AddEntity(string json)
        {
            T entity = DataSwitch.JsonToObj<T>(json);
            if (entity == null) return General.reFail;
            int count = dbSet.InsertReturnIdentity(entity);
            return DataSwitch.HttpPostData(count);
        }

        public string UpdateEntity(string json)
        {
            T entity = DataSwitch.JsonToObj<T>(json);
            if (entity == null) return General.reFail;
            bool count = dbSet.Update(entity);
            return DataSwitch.HttpPostData(count ? General.reSucess : General.reFail);
        }

        public string DeleteEntity(string json)
        {
            T entity = DataSwitch.JsonToObj<T>(json);
            if (entity == null) return General.reFail;
            bool count = dbSet.Delete(entity);
            return DataSwitch.HttpPostData(count ? General.reSucess : General.reFail);
        }

    }
}
