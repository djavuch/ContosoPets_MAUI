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
    private async Task OpenDetails(PetModel pet)
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
        SelectedPet = new PetModel();
        _currentPopup = new PetAddFormPopup(this);
        await Shell.Current.ShowPopupAsync(_currentPopup);
    }

    [RelayCommand]
    private async Task OpenEditPopup(PetModel pet)
    {
        if (pet == null) return;

        _originalPet = pet;

        SelectedPet = new PetModel
        {
            PetName = pet.PetName,
            PetSpecie = pet.PetSpecie,
            PetAge = pet.PetAge,
            PetPhysicalDescription = pet.PetPhysicalDescription,
            PetPersonalDescription = pet.PetPersonalDescription,
            Owner = pet.Owner
        };

        _currentPopup = new PetEditFormPopup(this);
        await Shell.Current.ShowPopupAsync(_currentPopup);
    }

    [RelayCommand]
    private async Task CancelAddPet()
    {
        SelectedPet = null;
        await _currentPopup.CloseAsync();   
    }

    [RelayCommand]
    private async Task CancelEditPet()
    {
        if (_originalPet != null)
        {
            SelectedPet.PetId = _originalPet.PetId;
            SelectedPet.PetName = _originalPet.PetName;
            SelectedPet.PetSpecie = _originalPet.PetSpecie;
            SelectedPet.PetAge = _originalPet.PetAge;
            SelectedPet.PetPhysicalDescription = _originalPet.PetPhysicalDescription;
            SelectedPet.PetPersonalDescription = _originalPet.PetPersonalDescription;
            SelectedPet.Owner = _originalPet.Owner;
            SelectedPet.IsOwned = _originalPet.IsOwned;
        }

        if (_currentPopup != null)
            await _currentPopup.CloseAsync();
    }
}
