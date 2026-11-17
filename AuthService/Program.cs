using AuthService.Data;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("authdb")
                       ?? throw new InvalidOperationException("Connection string 'authdb' not found.");
builder.Services.AddDbContext<AuthContext>(options =>
    options.UseSqlServer(connectionString));

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddScoped<IUserRepository, UserRepository>();
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

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
