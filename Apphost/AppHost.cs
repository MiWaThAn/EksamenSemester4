using CommunityToolkit.Aspire.Hosting.Dapr;
using Microsoft.AspNetCore.Authentication;
using Projects;
using System.Collections.Immutable;

var builder = DistributedApplication.CreateBuilder(args);


//This is for locating my statestore and pubsub
var sharedPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "shared"));
var daprResources = ImmutableHashSet.Create(sharedPath);

// This is for my database
var sql = builder.AddSqlServer("sql")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume();

var authDb = sql.AddDatabase("authdb");
var orderDb = sql.AddDatabase("orderdb");
var productService = sql.AddDatabase("productdb");
var warehouseDb = sql.AddDatabase("warehousedb");


//This is for setting up the Authservice project, including its own database and dapr sidecar.
var authService = builder.AddProject<Projects.AuthService>("authservice")
    .WithDaprSidecar(new DaprSidecarOptions
    {
        AppId = "authservice",
        DaprHttpPort = 3501,
        ResourcesPaths = daprResources
    }).WithReference(authDb)
    .WaitFor(authDb);

//This is for setting up the OrderService project, including its own database and dapr sidecar.
var orderService = builder.AddProject<Projects.OrderService>("orderservice").WithDaprSidecar(new DaprSidecarOptions
{
    AppId = "orderservice",
    DaprHttpPort = 3502,
    ResourcesPaths = daprResources
}).WithReference(orderDb)
    .WaitFor(orderDb);

//This is for setting up the ProductCatalogService project, including its own database and dapr sidecar.
var productCatalogService = builder.AddProject<Projects.ProductCatalogService>("productcatalogservice").WithDaprSidecar(new DaprSidecarOptions
{
    AppId = "productcatalogservice",
    DaprHttpPort = 3503,
    ResourcesPaths = daprResources
})
    .WithReference(productService)
    .WaitFor(productService);

//This is for setting up the ShoppingCartService project, including its redis statestore and dapr sidecar .
var shoppingCartService = builder.AddProject<Projects.ShoppingCartService>("shoppingcartservice").WithDaprSidecar(new DaprSidecarOptions
{
    AppId = "shoppingcartservice",
    DaprHttpPort = 3504,
    ResourcesPaths = daprResources
    //I will not make a database for this, as I will be using a redis statestore.
});

//This is for setting up the WarehouseService project, including its own database and dapr sidecar.
var warehouseService = builder.AddProject<Projects.WarehouseService>("warehouseservice").WithDaprSidecar(new DaprSidecarOptions
{
    AppId = "warehouseservice",
    DaprHttpPort = 3505,
    ResourcesPaths = daprResources
})
    .WithReference(warehouseDb)
    .WaitFor(warehouseDb);

//This is for setting up the ApiGateway project, including its references to all the other services.
var apiGateway = builder.AddProject<Projects.ApiGateway>("apigateway")
    .WithReference(authService)
    .WithReference(orderService)
    .WithReference(productCatalogService)
    .WithReference(shoppingCartService)
    .WithReference(warehouseService);

builder.Build().Run();
