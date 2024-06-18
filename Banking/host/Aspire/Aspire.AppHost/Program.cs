using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var accounts = builder.AddProject<Projects.Accounts>("accounts");
var customers = builder.AddProject<Projects.Customers>("customers");

builder.AddProject<Projects.WebApp>("webapp")
    .WithReference(accounts)
    .WithReference(customers);


builder.Build().Run();
