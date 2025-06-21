using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using ContosoPets.Models;
using ContosoPets.View.PetsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoPets.ViewModels.PetsViewModels;

public partial class PetsViewModel
{
    [RelayCommand]
    private async Task OpenDetails(PetsModel pet)
    {
        if (pet == null) return;

        var parameters = new Dictionary<string, object>
        {
            { "SelectedPet", pet }
        };

        await Shell.Current.GoToAsync(nameof(PetDetailsPage), parameters);
    }

    // Open popup for adding a new pet
    [RelayCommand]
    private async Task OpenAddPopup()
    {
        SelectedPet = new PetsModel();
        IsEditMode = false;
        _currentPopup = new PetFormPopup(this);
        await Shell.Current.ShowPopupAsync(_currentPopup);
    }

    [RelayCommand]
    private async Task OpenEditPopup(PetsModel pet)
    {
        _originalPet = pet;
        int petCount = 0;

        SelectedPet = new PetsModel
        {
            PetId = pet.PetSpecie + (petCount + 1),
            PetName = pet.PetName,
            PetSpecie = pet.PetSpecie,
            PetAge = pet.PetAge,
            PetPhysicalDescription = pet.PetPhysicalDescription,
            PetPersonalDescription = pet.PetPersonalDescription,
            Owner = pet.Owner
        };

        IsEditMode = true;

        _currentPopup = new PetFormPopup(this);
        await Shell.Current.ShowPopupAsync(_currentPopup);
    }
}
