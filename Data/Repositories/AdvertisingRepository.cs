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

        public void Create(Advertising ad)
        {
            db.Ads.Add(ad);
        }

        public void Update(Advertising ad)
        {
            var adInDb = db.Ads.Find(ad.ID);
            if (adInDb == null)
            {
                db.Ads.Add(ad);
                return;
            }
            
            db.Entry(adInDb).CurrentValues.SetValues(ad);
            db.Entry(adInDb).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IEnumerable<Advertising> Find(Func<Advertising, Boolean> predicate)
        {
            return db.Ads.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Advertising ad = db.Ads.Find(id);
            if (ad != null)
                db.Ads.Remove(ad);
        }
    }
}