using Common.Storage;
using Customers.Features.ValueObjects;
using Featurize;
using Featurize.AspNetCore;
using Featurize.Repositories;

namespace Customers.Features;

public class CustomerFeature : IWebApplicationFeature,
     IConfigureOptions<RepositoryProviderOptions>
{
    public void Configure(IServiceCollection services)
    {
        
    }

    public void Configure(RepositoryProviderOptions options)
    {
        options.AddAggregate<Customer, CustomerId>();
    }

    public void Use(WebApplication app)
    {
        var group = app.MapGroup("/api/customers")
            .WithOpenApi(o => new(o)
            {
                Summary = "Manage Customers",
                Description = "",
            });
    }
}
