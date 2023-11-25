using Featurize.AspNetCore;

namespace GBC.Accounts.Features.Account;

public class AccountFeature : IWebApplicationFeature
{
    public void Configure(IServiceCollection services)
    {
        
    }

    public void Use(WebApplication app)
    {
        var group = app.MapGroup("/api/accounts")
            .WithOpenApi(o => new(o)
            {
                Summary = "Manage Bank Accounts",
                Description = "",
            });


    }
}
