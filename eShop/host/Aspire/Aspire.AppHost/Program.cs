var builder = DistributedApplication.CreateBuilder(args);


var catalogApi = builder.AddProject<Projects.Catalog>("catalog-api");
var basketApi = builder.AddProject<Projects.BasketApi>("basket-api");
var orderingApi = builder.AddProject<Projects.Ordering>("ordering-api");

builder.AddProject<Projects.WebApp>("webapp")
    .WithReference(catalogApi)
    .WithReference(basketApi)
    .WithReference(orderingApi)
    .WithLaunchProfile("http");

builder.Build().Run();
