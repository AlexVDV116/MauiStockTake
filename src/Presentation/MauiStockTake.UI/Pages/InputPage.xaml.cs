using MauiStockTake.UI.Models;

namespace MauiStockTake.UI.Pages;

public partial class InputPage : ContentPage
{
    public InputPage()
    {
        InitializeComponent();
    }
    
    private async void Button_Clicked(object sender, EventArgs e)
    {
        var product = new Product { Name = "MauiStockTake", ManufacturerName = "BeachBytes" };
 
        var pageParams = new Dictionary<string, object>
        {
            { "Product", product }
        };
 
        await Shell.Current.GoToAsync("productdetails", pageParams);
    }
}