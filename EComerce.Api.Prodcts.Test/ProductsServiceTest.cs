using AutoMapper;
using EComerce.Api.Products.Db;
using EComerce.Api.Products.Profiles;
using EComerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;

namespace EComerce.Api.Prodcts.Test
{
    public class ProductsServiceTest
    {
        [Fact]
        public async Task GetProductsReturnAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase(nameof(GetProductsReturnAllProducts)).Options;
            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productProvider = new ProductsProvider(dbContext, null, mapper);

            var products = await productProvider.GetProductsAsync();

            Assert.True(products.isSuccess);
            Assert.True(products.products.Any());
            Assert.Null(products.errorMessage);

        }

        [Fact]
        public async Task GetProductReturnsProductUsingValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase(nameof(GetProductReturnsProductUsingValidId)).Options;
            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productProvider.GetProductAsync(1);

            Assert.True(product.isSuccess);
            Assert.NotNull(product.product);
            Assert.True(product.product.Id == 1);
            Assert.Null(product.errorMessage);

        }

        [Fact]
        public async Task GetProductReturnsProductUsingInvalidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase(nameof(GetProductReturnsProductUsingInvalidId)).Options;
            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productProvider.GetProductAsync(-1);

            Assert.False(product.isSuccess);
            Assert.Null(product.product);
            Assert.NotNull(product.errorMessage);

        }

        private void CreateProducts(ProductsDbContext dbContext)
        {         
            for (int i = 1; i < 10; i++)
            {
                dbContext.Products.Add(new Product()
                {
                    Id = i,
                    Name = i.ToString(),
                    Inventory = i + 10,
                    Price  = (decimal)(i * 3.14)
                });
            }

            dbContext.SaveChanges();
        }
    }
}