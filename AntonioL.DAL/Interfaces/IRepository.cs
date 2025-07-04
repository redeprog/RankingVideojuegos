﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AntonioL.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        // Operaciones básicas CRUD
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        // Operaciones de escritura
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        // Operaciones adicionales útiles
        int Count();
        bool Any(Expression<Func<TEntity, bool>> predicate);
    }
}
