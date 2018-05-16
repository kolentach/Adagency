using System;
using Data.Entities;
using Data.EF;
using Data.Interfaces;

namespace Data.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private AgencyContext db;
        private OrderRepository OrderRepository;
        private AdvertisingRepository AdvertisingRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new AgencyContext(connectionString);
        }
        public IRepository<Order> Orders
        {
            get
            {
                if (OrderRepository == null)
                    OrderRepository = new OrderRepository(db);
                return OrderRepository;
            }
        }

        public IRepository<Advertising> Ads
        {
            get
            {
                if (AdvertisingRepository == null)
                    AdvertisingRepository = new AdvertisingRepository(db);
                return AdvertisingRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}