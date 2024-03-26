using MauiStockTake.UI.Helpers;
using Microsoft.Extensions.Logging;
using IBrowser = IdentityModel.OidcClient.Browser.IBrowser;

namespace MauiStockTake.UI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .UsePageResolver();
        
        builder.Services.AddSingleton<IBrowser, AuthBrowser>();
        builder.Services.AddSingleton<IAuthService, AuthService>();
        
        builder.Services.AddTransient<LoginPage>();
        

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}