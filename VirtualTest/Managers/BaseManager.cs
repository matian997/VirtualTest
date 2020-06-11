using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace VirtualTest
{
    public abstract class BaseManager<TEntity, TContext> 
        where TEntity : class
        where TContext : DbContext
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

        public void Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            context.SaveChanges();
        }

        public TEntity GetById(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return context.Set<TEntity>();
        }

        public void Remove(int id)
        {
            var entity = GetById(id);

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Set<TEntity>().Remove(entity);
            context.SaveChanges();
        }
    }
}
