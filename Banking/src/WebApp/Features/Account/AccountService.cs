using WebApp.Features.Account.Entities;

namespace WebApp.Features.Account;

public class AccountService(HttpClient httpClient)
{
    public HttpClient HttpClient { get; } = httpClient;

    public async Task<AccountModel> GetAccountAsync()
    {
        var result = await HttpClient.GetFromJsonAsync<AccountModel>("{id}");
        return result;
    }
}
