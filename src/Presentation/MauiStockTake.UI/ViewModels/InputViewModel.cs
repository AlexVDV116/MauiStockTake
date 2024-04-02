using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MauiStockTake.UI.ViewModels;

public class InputViewModel : BaseViewModel
{
    private readonly IProductService _productService;
    private readonly IInventoryService _inventoryService;

    public ICommand SearchProductsCommand { get; set; }

    public ICommand AddCountCommand { get; set; }

    public ObservableCollection<ProductDto> SearchResults { get; set; } = new();

    public ProductDto SelectedProduct { get; set; }

    private int _count = 0;

    // Uses a backing field and raise a property changed notification when the value changes
    public int Count
    {
        get => _count;
        set
            {
                _count = value;
            OnPropertyChanged();
        }
    }

    public string SearchTerm { get; set; }

    public InputViewModel(IProductService productService, IInventoryService inventoryService)
    {
        _productService = productService;
        _inventoryService = inventoryService;
        SearchProductsCommand = new Command(async () => await UpdateSearchResults());
        AddCountCommand = new Command(async () => await AddCount());
    }

    private async Task UpdateSearchResults()
    {
        IsLoading = true;

        SearchResults.Clear();

        var results = await _productService.SearchProducts(SearchTerm);

        IsLoading = false;

        results.ForEach(res => SearchResults.Add(res));
    }

    private async Task AddCount()
    {
        if (SelectedProduct is null)
        {
            await App.Current.MainPage.DisplayAlert("Product Required", "You have not selected a product to record a count for", "OK");
            return;
        }
        
        IsLoading = true;

        var added = await _inventoryService.AddStockCount(SelectedProduct, Count);
	
	    IsLoading = false;

        if (added)
        {
            await App.Current.MainPage.DisplayAlert("Added", "Stock count has been added to inventory", "OK");
            ResetForm();
        }
        else
        {
            await App.Current.MainPage.DisplayAlert("Error", "Something went wrong, please try again.", "OK");
        }
    }

    private void ResetForm()
    {
        SearchResults.Clear();
        Count = 0;
        SelectedProduct = null;
        SearchTerm = string.Empty;
        OnPropertyChanged(nameof(SearchTerm));
    }

}
