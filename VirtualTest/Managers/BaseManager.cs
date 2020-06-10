using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace VirtualTest
{
    public abstract class BaseManager<TEntity, TContext> 
        where TEntity:class
        where TContext:DbContext
    {
        protected readonly TContext context;

        public BaseManager(TContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this.context = context;
        }

        public abstract void Add(TEntity entity);

        public TEntity GetById(int id)
        {
            return this.context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.context.Set<TEntity>();
        }

        public void Remove(int id)
        {
            var entity = this.GetById(id);

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this.context.Set<TEntity>().Remove(entity);
            this.context.SaveChanges();
        }
    }
}
