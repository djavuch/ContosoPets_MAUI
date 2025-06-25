using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContosoPets.Models;
using ContosoPets.ViewModels.PetsOwnersViewModels;

namespace ContosoPets.ViewModels.PetsViewModels;

[QueryProperty(nameof(Pet), "SelectedPet")]
public partial class PetDetailsViewModel : ObservableObject
{
    private readonly PetsViewModel _petsViewModel;

    [ObservableProperty]
    private PetModel _pet;

    public PetDetailsViewModel(PetsViewModel petsViewModel)
    {
        _petsViewModel = petsViewModel;
    }

    [RelayCommand]
    private async Task EditPet()
    {
        if (Pet != null)
            await _petsViewModel.OpenEditPopupCommand.ExecuteAsync(Pet);
    }

    [RelayCommand]
    private async Task DeletePet()
    {
        if (Pet == null)
            return;

        var popup = new DeleteConfirmationPopup();
        var result = await Shell.Current.CurrentPage.ShowPopupAsync(popup);

        if (result is true)
        {
            _petsViewModel?.Pets.Remove(Pet);
            await _petsViewModel!.DeletePetCommand.ExecuteAsync(Pet);
            await Shell.Current.GoToAsync("..");
        }
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}
