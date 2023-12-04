using Featurize;

namespace WebApp.Features.User;

public class UserFeature : IServiceCollectionFeature
{
    public void Configure(IServiceCollection services)
    {
        services.AddHttpClient<UserService>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:5001");
        });
    }
}
