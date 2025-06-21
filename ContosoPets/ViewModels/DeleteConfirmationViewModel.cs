using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ContosoPets.ViewModels;

public partial class DeleteConfirmationViewModel(Popup popup) : ObservableObject
{
    private readonly Popup _popup = popup;

    [RelayCommand]
    private async Task Confirm()
    {
        await _popup.CloseAsync(true); // Return true when confirmed
    }

    [RelayCommand]
    private async Task Cancel()
    {
        await _popup.CloseAsync(false); // Return false when canceled
    }
}
