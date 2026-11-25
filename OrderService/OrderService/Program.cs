using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OrderService.Data;
using OrderService.Shared.Behaviors;
using System.Text;
using OrderService.Repositories.OrderRepository;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
var connectionString = builder.Configuration.GetConnectionString("orderdb")
                       ?? throw new InvalidOperationException("Connection string 'orderdb' not found.");
builder.Services.AddDbContext<OrderContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["Secret"] ?? throw new InvalidOperationException("JwtSettings:Secret missing");
var key = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

//I'm using this to setup MediatR.
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

//I'm using this to setup FluentValidation.
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

//I'm using this to setup the ValidationBehavior for MediatR.
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<OrderDbSeeding>();
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();


    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<OrderContext>();
    var seeder = scope.ServiceProvider.GetRequiredService<OrderDbSeeding>();
    await dbContext.Database.MigrateAsync();

    await seeder.SeedAsync(dbContext);
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
