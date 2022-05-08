using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using UniversityProject.DAL.Storage;

namespace UniversityProject.DAL.Repository
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal StorageFactory strgFactory;
        internal IStorage iStorage;
        internal Dictionary<int, TEntity> dictionary;

        public GenericRepository()
        {
            strgFactory = new ConcreteStorageFactory();
            iStorage = strgFactory.GetStorage();
            Object obj = iStorage.GetData();
           
            PropertyInfo[] props = iStorage.GetData().GetType().GetProperties();
            int index = 0;   
            foreach (PropertyInfo prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(false);
                if (prop.Name == typeof(TEntity).Name+"s") { dictionary = (Dictionary<int, TEntity>)prop.GetValue(obj); return; }
                index++;
            }
                     
        }

        public virtual IEnumerable<TEntity> GetValues(
         Expression<Func<TEntity, bool>> filter = null,
         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = dictionary.Values.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual Dictionary<int, TEntity> Get()
        {   
            return dictionary;
        }

        //public virtual void Update(TEntity entityToUpdate)
        //{
        //    dbSet.Attach(entityToUpdate);
        //    context.Entry(entityToUpdate).State = EntityState.Modified;
        //}

        //public virtual TEntity GetByID(object id)
        //{
        //    return dictionary.Find(id);
        //}

        //public virtual void Insert(TEntity entity)
        //{
        //    dbSet.Add(entity);
        //}

        //public virtual void Delete(object id)
        //{
        //    dictionary.Remove(id);
        //}

        //public virtual void Delete(TEntity entityToDelete)
        //{
        //    if (context.Entry(entityToDelete).State == EntityState.Detached)
        //    {
        //        dbSet.Attach(entityToDelete);
        //    }
        //    dbSet.Remove(entityToDelete);
        //}

    }
}
