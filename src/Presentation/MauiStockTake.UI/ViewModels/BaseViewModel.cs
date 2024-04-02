using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiStockTake.UI.ViewModels;

// Use MVVM Toolkit to simplify view model creation
public class BaseViewModel : INotifyPropertyChanged
{
    private string _title;
    public string Title { get => _title; set { _title = value; OnPropertyChanged(); } }

    private bool _isLoading;
    public bool IsLoading { get => _isLoading; set { _isLoading = value; OnPropertyChanged(); } }

    public INavigation Navigation { get; set; }

    
    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        var changed = PropertyChanged;
        if (changed == null)
            return;

        changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}