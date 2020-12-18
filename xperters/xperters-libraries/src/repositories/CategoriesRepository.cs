using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using xperters.entities;
using xperters.entities.Entities;

namespace xperters.repositories
{
    public class CategoriesRepository : IRepository<Category>
    {
        private readonly XpertersContext _context;
        private readonly ILogger<CategoriesRepository> _logger;

        public CategoriesRepository(XpertersContext context)
        {
            _context = context;
        }

        public void Add(Category item)
        {
            throw new NotImplementedException();
        }

        public void AddList(List<Category> items)
        {
            throw new NotImplementedException();
        }

        public Category Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Category Get(string field)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Category> Get()
        {
            return _context.Categories;
        }

        public IQueryable<Category> Get(Expression<Func<Category, bool>> whereCondition)
        {
            return _context.Categories.Where<Category>(whereCondition);
        }
        public bool Exists(Expression<Func<Category, bool>> whereCondition)
        {
            return _context.Categories.Any(whereCondition);
        }
        public void Update(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
        }
        public IQueryable<Category> Include(Expression<Func<Category, object>> whereCondition)
        {
            throw new NotImplementedException();
        }

     

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
