using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using xperters.entities.Entities;

namespace xperters.repositories
{
    public interface IRepository<T>
    {
        T Get(Guid id);
        T Get(string field);
        void Add(T item);
        void AddList(List<T> items);
        IQueryable<T> Get();
        IQueryable<T> Get(Expression<Func<T, bool>> whereCondition);
        bool Exists(Expression<Func<T, bool>> whereCondition);
        void Update(T item);
        IQueryable<T> Include(Expression<Func<T, object>> whereCondition);
        void Delete(Guid id);
    }
}
 