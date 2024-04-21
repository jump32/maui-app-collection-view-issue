using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace MauiCollectionViewTest;

public class MainPageViewModel : ObservableObject
{
    private IList<Item> _items;

    public IList<Item> Items
    {
        get => _items;
        set => SetProperty(ref _items, value);
    }
    
    public ICommand LoadCommand { get; }

    public MainPageViewModel()
    {
        LoadCommand = new RelayCommand(Load);
        Items = Enumerable.Repeat(new Item { IsLoading = true }, 20).ToList();
    }

    private void Load()
    {
        Items = Enumerable.Repeat(new Item { IsLoading = false }, 40).ToList();
    }
}