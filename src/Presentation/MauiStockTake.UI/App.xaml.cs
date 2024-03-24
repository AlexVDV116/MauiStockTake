namespace MauiStockTake.UI;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
    
    protected override async void OnStart()
    {
        base.OnStart();
 
        await MainPage.Navigation.PushModalAsync(new LoginPage());
    }
}