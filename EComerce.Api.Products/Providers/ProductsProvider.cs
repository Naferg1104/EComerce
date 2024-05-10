using AutoMapper;
using EComerce.Api.Products.Db;
using EComerce.Api.Products.Interfaces;
using EComerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace EComerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext productsDBContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProductsDbContext productsDBContext, ILogger<ProductsProvider> logger, IMapper mapper) { 
            this.productsDBContext = productsDBContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!productsDBContext.Products.Any())
            {
                productsDBContext.Products.Add(new Db.Product()
                {
                    Id = 1,
                    Name = "Keyboard",
                    Inventory = 100,
                    Price = 20
                });

                productsDBContext.Products.Add(new Db.Product()
                {
                    Id = 2,
                    Name = "Mouse",
                    Inventory = 200,
                    Price = 5
                });

                productsDBContext.Products.Add(new Db.Product()
                {
                    Id = 3,
                    Name = "Monitor",
                    Inventory = 100,
                    Price = 150
                });

                productsDBContext.Products.Add(new Db.Product()
                {
                    Id = 4,
                    Name = "CPU",
                    Inventory = 200,
                    Price = 200
                });

                productsDBContext.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, IEnumerable<Models.Product> products, string errorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await productsDBContext.Products.ToListAsync();

                if (products!= null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Product>,IEnumerable<Models.Product>>(products);

                    return (true,result,null);
                }

                return (false, null, "Not found");

            }catch (Exception ex)
            {
                logger?.LogError(ex.ToString());

                return (false, null, ex.Message);
            }
        }

        public async Task<(bool isSuccess, Models.Product product, string errorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await productsDBContext.Products.FirstOrDefaultAsync(p => p.Id == id);

                if (product != null)
                {
                    var result = mapper.Map<Db.Product, Models.Product>(product);

                    return (true, result, null);
                }

                return (false, null, "Not found");

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());

                return (false, null, ex.Message);
            }
        }
    }
}
