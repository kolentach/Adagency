using System;
using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using Data.EF;
using Data.Interfaces;
using System.Data.Entity;

namespace Data.Repositories
{
    public class AdvertisingRepository : IRepository<Advertising>
    {
        private AgencyContext db;

        public AdvertisingRepository(AgencyContext context)
        {
            this.db = context;
        }

        public IEnumerable<Advertising> GetAll()
        {
            return db.Ads;
        }

        public Advertising Get(int id)
        {
            return db.Ads.Find(id);
        }

        public void Create(Advertising book)
        {
            db.Ads.Add(book);
        }

        public void Update(Advertising book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public IEnumerable<Advertising> Find(Func<Advertising, Boolean> predicate)
        {
            return db.Ads.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Advertising book = db.Ads.Find(id);
            if (book != null)
                db.Ads.Remove(book);
        }
    }
}