using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace EComerce.Api.Costumers.Db
{
    public class CustomersDbCOntext : DbContext
    {
            
        public DbSet<Customer> Customers { get; set; }

        public CustomersDbCOntext(DbContextOptions options) : base(options)
        {
          
        }
    }
}
