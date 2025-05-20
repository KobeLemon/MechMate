using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace MechMate.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        string text;

        void Add()
        {
            Text = string.Empty;
        }
    }
}
