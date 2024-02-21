var builder = DistributedApplication.CreateBuilder(args);

var accounts = builder.AddProject<Projects.Accounts>("accounts");

builder.AddProject<Projects.WebApp>("webapp")
    .WithReference(accounts);

builder.Build().Run();
