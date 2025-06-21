using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContosoPets.Models;

namespace ContosoPets.ViewModels.PetsOwnersViewModels;

[QueryProperty(nameof(PetOwner), "SelectedPetOwner")]
public partial class PetOwnerDetailsViewModel : ObservableObject
{
    private readonly PetsOwnersViewModel? _petOwnersViewModel;

    [ObservableProperty]
    private PetsOwnersModel _petOwner;

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
        if (PetOwner != null)
        {
            _petOwnersViewModel.PetsOwners.Remove(PetOwner);
            await _petOwnersViewModel.DeletePetOwnerCommand.ExecuteAsync(PetOwner);
            await Shell.Current.GoToAsync("..");
        }
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}

