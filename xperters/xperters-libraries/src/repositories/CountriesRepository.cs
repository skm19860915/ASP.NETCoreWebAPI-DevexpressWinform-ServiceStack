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
    public class CountriesRepository : IRepository<Country>
    {
        private readonly XpertersContext _context;
        private readonly ILogger<CategoriesRepository> _logger;

        public CountriesRepository(XpertersContext context)
        {
            _context = context;
        }

        public void Add(Country item)
        {
            _context.Countries.Add(item);
            _context.SaveChanges();
        }

        public void AddList(List<Country> items)
        {
            throw new NotImplementedException();
        }

        public Country Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Country Get(string field)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Country> Get()
        {
            return _context.Countries;
        }

        public IQueryable<Country> Get(Expression<Func<Country, bool>> whereCondition)
        {
            return _context.Countries.Where<Country>(whereCondition);
        }
        public bool Exists(Expression<Func<Country, bool>> whereCondition)
        {
            return _context.Countries.Any(whereCondition);
        }
        public void Update(Country country)
        {
            _context.Entry(country).State = EntityState.Modified;
        }

        public IQueryable<Country> Include(Expression<Func<Country, object>> whereCondition)
        {
            throw new NotImplementedException();
        }

     

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
