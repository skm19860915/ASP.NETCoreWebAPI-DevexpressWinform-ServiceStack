using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace xperters.repositories
{
    public interface IRepositoryReadOnly<T>
    {
        T Get(Guid id);
        IEnumerable<T> Get(int page, int pageSize);
        IQueryable<T> Get(Expression<Func<T, bool>> whereCondition);
        bool Exists(Expression<Func<T, bool>> whereCondition);
        IEnumerable<T> Search(string title, DateTime? date);
    }
}