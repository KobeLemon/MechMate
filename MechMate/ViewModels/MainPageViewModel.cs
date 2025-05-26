using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MechMate.Models;

namespace MechMate.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    private readonly INavigation _navigation;
    public MainPageViewModel(INavigation navigation)
    {
        _navigation = navigation;
    }

    [RelayCommand]
    private async Task GoToMyRidePage()
    {
        await _navigation.PushAsync(new MyRidePage("123"));
    }
}
