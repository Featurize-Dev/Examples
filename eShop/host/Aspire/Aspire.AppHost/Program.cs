var builder = DistributedApplication.CreateBuilder(args);


var catalogApi = builder.AddProject<Projects.Catalog>("catalog-api");

builder.AddProject<Projects.WebApp>("webapp")
    .WithReference(catalogApi)
    .WithLaunchProfile("http");

builder.Build().Run();
