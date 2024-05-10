using EComerce.Api.Searchs.Interfaces;
using EComerce.Api.Searchs.Services;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IOrdersService, OrdersServices>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<ICustomersService, CustomersService>();

var UrlServiceOrders = builder.Configuration["Services:Orders"];
var UrlServiceProducts = builder.Configuration["Services:Products"];
var UrlServiceCustomers = builder.Configuration["Services:Customers"];

builder.Services.AddHttpClient("OrdersService", config =>
{
    config.BaseAddress = new Uri(UrlServiceOrders);
});

builder.Services.AddHttpClient("ProductsService", config =>
{
    config.BaseAddress = new Uri(UrlServiceProducts);
}).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5,_ => TimeSpan.FromMilliseconds(500) ));

builder.Services.AddHttpClient("CustomersService", config =>
{
    config.BaseAddress = new Uri(UrlServiceCustomers);
}).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
