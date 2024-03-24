using IdentityModel.OidcClient.Browser;
 
namespace MauiStockTake.UI.Helpers;
 
public class AuthBrowser : IdentityModel.OidcClient.Browser.IBrowser
{
    public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
    {
        WebAuthenticatorResult authResult = await
            WebAuthenticator.AuthenticateAsync(new Uri(
                options.StartUrl), new Uri(Constants.RedirectUri));
 
        return new BrowserResult()
        {
            Response = ParseAuthenticationResult(
                authResult)
        };
    }
 
    private string ParseAuthenticationResult(
        WebAuthenticatorResult result)
    {
        string code = result?.Properties["code"];
        string scope = result?.Properties["scope"];
        string state = result?.Properties["state"];
        string sessionState = result?.Properties["session_state"];
        return $"{Constants.RedirectUri}#code={code}&scope={scope}&state={state}&session_state={sessionState}";
    }
}