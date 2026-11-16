
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalogService.Data;
using ProductCatalogService.Repositories;
using ProductCatalogService.Shared.Behaviors;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("productdb")
                       ?? throw new InvalidOperationException("Connection string 'productdb' not found.");
builder.Services.AddDbContext<ProductCatalogContext>(options =>
    options.UseSqlServer(connectionString));

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


//I'm using this to setup MediatR.
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

//I'm using this to setup FluentValidation.
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

//I'm using this to setup the ValidationBehavior for MediatR.
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
//app.UseHttpsRedirection();





var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}



app.UseAuthorization();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProductCatalogContext>();
    dbContext.Database.Migrate();
}
app.MapControllers();

app.Run();
