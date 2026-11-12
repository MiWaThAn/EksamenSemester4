using Microsoft.AspNetCore.Authentication;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var authService = builder.AddProject<Projects.AuthService>("authservice");

var orderService = builder.AddProject<Projects.OrderService>("orderservice");

var productCatalogService = builder.AddProject<Projects.ProductCatalogService>("productcatalogservice");

var shoppingCartService = builder.AddProject<Projects.ShoppingCartService>("shoppingcartservice");

var warehouseService = builder.AddProject<Projects.WarehouseService>("warehouseservice");

builder.AddProject<Projects.ApiGateway>("apigateway")
    .WithReference(authService)
    .WithReference(orderService)
    .WithReference(productCatalogService)
    .WithReference(shoppingCartService)
    .WithReference(warehouseService);

builder.Build().Run();
