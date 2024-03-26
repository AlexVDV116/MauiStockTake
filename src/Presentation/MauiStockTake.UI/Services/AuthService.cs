using IdentityModel.OidcClient;
using MauiStockTake.UI.Helpers;
using IBrowser = IdentityModel.OidcClient.Browser.IBrowser;

 
namespace MauiStockTake.UI.Services;
 
public class AuthService : IAuthService
{
    private readonly OidcClientOptions _options;
 
    public AuthService(IBrowser browser)
    {
        _options = new OidcClientOptions
        {
            Authority   = Constants.AuthorityUri,
            ClientId    = Constants.ClientId,
            Scope       = Constants.Scope,
            RedirectUri = Constants.RedirectUri,
            Browser     = browser
        };
    }
    public async Task<bool> LoginAsync()
    {
        var oidcClient = new OidcClient(_options);
 
        var loginResult = await oidcClient.LoginAsync(new LoginRequest());
 
        if (loginResult.IsError)
        {
            // TODO: inspect and handle error
            return false;
        }
 
        return true;
    }
}