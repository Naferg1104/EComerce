using Microsoft.EntityFrameworkCore;

namespace EComerce.Api.Orders.Db
{
    public class OrdersDbContext: DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public OrdersDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
    }
}
