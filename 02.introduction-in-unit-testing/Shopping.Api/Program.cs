using Shopping.Business.Carts;
using Shopping.Business.Products;
using Shopping.Core.Repositories;
using Shopping.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddSingleton<ICartRepository, CartRepository>()
    .AddSingleton<CartService>()
    .AddSingleton<IProductsService, ExternalProductsService>()
    .AddSingleton(new CartSettings { ExpiresAfter = TimeSpan.FromSeconds(15) });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
