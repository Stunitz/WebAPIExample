using System.Data.Entity;
using WebAPIExample.Models.Data;

namespace WebAPIExample.Models
{
    public class Context : DbContext
    {

        public Context() : base("MentoradoContext") { }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Purchase> Purchase { get; set; }

    }
}