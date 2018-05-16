using System;
using Data.Entities;

namespace Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Order> Orders { get; }
        IRepository<Advertising> Ads { get; }
        void Save();
    }
}
