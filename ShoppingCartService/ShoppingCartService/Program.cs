using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using ShoppingCartService.Clients.ProductCatalogClient;
using ShoppingCartService.Repositories.CartRepository;
using ShoppingCartService.Shared.Behaviors;
using System.Text;

var builder = WebApplication.CreateBuilder(args);




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

builder.AddServiceDefaults();
builder.Services.AddDaprClient();
// Add services to the container.
builder.Services.AddHttpClient<IProductCatalogClient, HttpProductCatalogClient>(client =>
{
    var daprPort = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT") ?? "3504";
    client.BaseAddress = new Uri($"http://localhost:{daprPort}/");
    client.DefaultRequestHeaders.Add("dapr-app-id", "productcatalogservice");
});
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<ICartRepository, DaprCartRepository>();
//I'm using this to setup MediatR.
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

//I'm using this to setup FluentValidation.
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

//I'm using this to setup the ValidationBehavior for MediatR.
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}




//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
