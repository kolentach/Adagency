using System.Data.Entity;
using Data.Entities;

namespace Data.EF
{
    public class AgencyContext : DbContext
    {
        public DbSet<Advertising> Ads { get; set; }
        public DbSet<Order> Orders { get; set; }

        static AgencyContext()
        {
            Database.SetInitializer<AgencyContext>(new StoreDbInitializer());
        }
        public AgencyContext(string connectionString)
            : base(connectionString)
        {
        }
    }

    public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<AgencyContext>
    {
        protected override void Seed(AgencyContext db)
        {
            db.Ads.Add(new Advertising { Name = "dsfsdfs", Type = "radio", Description = "dsfffffffffff", Price = 100 });
            db.Ads.Add(new Advertising { Name = "dsfsdfs", Type = "radio", Description = "dsfffffffffff", Price = 100 });
            db.Ads.Add(new Advertising { Name = "dsfsdfs", Type = "radio", Description = "dsfffffffffff", Price = 100 });
        }
    }
}