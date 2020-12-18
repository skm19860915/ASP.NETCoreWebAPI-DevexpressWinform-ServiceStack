using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using xperters.entities;
using xperters.entities.Entities;
namespace xperters.repositories
{
    public class CardRepository : IRepository<Card>
    {
        private readonly XpertersContext _context;
        public CardRepository(XpertersContext context)
        {
            _context = context;
        }

        public void Add(Card card)
        {
            _context.Cards.Add(card);
            _context.SaveChanges();
        }

        public void AddList(List<Card> items)
        {
            throw new NotImplementedException();
        }

        public Card Get(Guid id)
        {

            return _context.Cards.First(x => x.Id == id);
        }

        public Card Get(string field)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Card> Get()
        {
            return _context.Cards;
        }

        public IQueryable<Card> Get(Expression<Func<Card, bool>> whereCondition)
        {
            return _context.Cards.Where(whereCondition);
        }

        public bool Exists(Expression<Func<Card, bool>> whereCondition)
        {
            return _context.Cards.Any(whereCondition);
        }

        public void Update(Card card)
        {
            _context.Entry(card).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public IQueryable<Card> Include(Expression<Func<Card, object>> whereCondition)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            Card card = _context.Cards.Find(id);

            _context.Cards.Remove(card);

            _context.SaveChanges();
        }

    
    }
}
