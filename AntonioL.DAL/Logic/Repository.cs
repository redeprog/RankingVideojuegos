using AntonioL.DAL.Interfaces;
using AntonioL.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AntonioL.DAL.Logic
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly OL_Gamers_ALEntities Context;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(OL_Gamers_ALEntities context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual TEntity Get(int id)
        {
            return DbSet.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).ToList();
        }

        public virtual void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public virtual int Count()
        {
            return DbSet.Count();
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Any(predicate);
        }
    }
}
