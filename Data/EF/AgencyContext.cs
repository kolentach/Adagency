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
            db.Ads.Add(new Advertising { Name = "Ad on 'Kyiv Radiostation'", Type = "radio", Description = "Advertising every 30 minutes on the best radiostation of your city", Price = 10000 });
            db.Ads.Add(new Advertising { Name = "Ad on 1+1 channel", Type = "TV", Description = "1+1 is one of the most popular TV channel, post your ad there", Price = 200000 });
            db.Ads.Add(new Advertising { Name = "Print your ad", Type = "polygraphy", Description = "Print your ad in any edition which you would like (Price is for every instance)", Price = 10 });
        }
    }
}