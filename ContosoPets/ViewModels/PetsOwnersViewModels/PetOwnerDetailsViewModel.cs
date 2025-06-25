using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContosoPets.Models;


namespace ContosoPets.ViewModels.PetsOwnersViewModels;

[QueryProperty(nameof(PetOwner), "SelectedPetOwner")]
public partial class PetOwnerDetailsViewModel : ObservableObject
{
    private readonly PetsOwnersViewModel? _petOwnersViewModel;

    [ObservableProperty]
    private PetOwnerModel _petOwner;

    public PetOwnerDetailsViewModel(PetsOwnersViewModel petOwnersViewModel)
    {
        _petOwnersViewModel = petOwnersViewModel;
    }

    [RelayCommand]
    private async Task EditPetOwner()
    {
        if (PetOwner != null)
            await _petOwnersViewModel.OpenPetOwnerEditPopupCommand.ExecuteAsync(PetOwner);
    }

    [RelayCommand]
    private async Task DeletePetOwner()
    {
        if (PetOwner == null)
            return;

        // Создание и отображение попапа
        var popup = new DeleteConfirmationPopup(); // предположим, что у тебя есть XAML-попап
        var result = await Shell.Current.CurrentPage.ShowPopupAsync(popup);

        if (result is true)
        {
            _petOwnersViewModel?.PetsOwners.Remove(PetOwner);
            await _petOwnersViewModel!.DeletePetOwnerCommand.ExecuteAsync(PetOwner);
            await Shell.Current.GoToAsync("..");
        }
    }

    [RelayCommand]
    private static async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}

