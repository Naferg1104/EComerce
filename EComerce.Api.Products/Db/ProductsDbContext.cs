﻿using Microsoft.EntityFrameworkCore;

namespace EComerce.Api.Products.Db
{
    public class ProductsDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductsDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
    }
}
