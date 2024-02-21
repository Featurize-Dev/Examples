using Featurize;

namespace WebApp.Features.Account;

public class AccountFeature : IServiceCollectionFeature
{
    public void Configure(IServiceCollection services)
    {
        services.AddHttpClient<AccountService>(client =>
        {
            client.BaseAddress = new Uri("http://account-api");
        });
    }
}
