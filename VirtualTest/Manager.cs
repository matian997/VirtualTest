using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace VirtualTest
{
    public abstract class Manager<TEntity, TDbContext> 
        where TEntity:class
        where TDbContext:DbContext
    {
        protected readonly TDbContext dbContext;

        public Manager(TDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            this.dbContext = dbContext;
        }

        public abstract bool Add(TEntity entity);

        public TEntity GetById(int id)
        {
            return this.dbContext.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.dbContext.Set<TEntity>();
        }

        public bool Remove(int id)
        {
            var entity = this.GetById(id);
            if (entity != null)
            {
                this.dbContext.Set<TEntity>().Remove(entity);
                this.dbContext.SaveChanges();
                
                return true;
            }

            return false;
        }
    }
}
