using Featurize;
using Featurize.AspNetCore;
using Featurize.Repositories;
using Common.Storage;
using GBC.Accounts.Features.Accounts.Commands;
using GBC.Accounts.Features.Accounts.ValueObjects;

namespace GBC.Accounts.Features.Accounts;

public class AccountFeature : IWebApplicationFeature,
    IConfigureOptions<RepositoryProviderOptions>
{
    public void Configure(IServiceCollection services)
    {
        
    }

    public void Configure(RepositoryProviderOptions options)
    {
        options.AddAggregate<Account, BankAccountNumber>();
    }

    public void Use(WebApplication app)
    {
        var group = app.MapGroup("/api/accounts")
            .WithOpenApi(o => new(o)
            {
                Summary = "Manage Bank Accounts",
                Description = "",
            });

        group.MapTransfrer();

    }
}
