using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using ContosoPets.Models;
using ContosoPets.View.PetsOwnersView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoPets.ViewModels.PetsOwnersViewModels;

public partial class PetsOwnersViewModel
{
    // Open details page for a selected pet owner
    [RelayCommand]
    private async Task OpenDetails(PetOwnerModel petOwner)
    {
        if (petOwner == null) return;

        var parameters = new Dictionary<string, object>
        {
            { "SelectedPetOwner", petOwner }
        };

        await Shell.Current.GoToAsync(nameof(PetOwnerDetailsPage), parameters);
    }

    // Open popup for adding a new pet owner
    [RelayCommand]
    private async Task OpenAddPetOwnerPopup()
    {
        SelectedPetOwner = new PetOwnerModel
        {
            Pets = new ObservableCollection<PetModel>()
        };
        _originalPetOwner = null; 
        _originalPetList.Clear(); 
        FilterPets();
        _currentPopup = new PetOwnerAddFormPopup(this);
        await Shell.Current.ShowPopupAsync(_currentPopup);
    }

    // Open popup for editing an existing pet owner
    [RelayCommand]
    private async Task OpenPetOwnerEditPopup(PetOwnerModel petOwner)
    {
        if (petOwner == null) return;

        _originalPetOwner = petOwner;
        _originalPetList = [.. petOwner.Pets]; // Store original pets list for comparison

        SelectedPetOwner = new PetOwnerModel
        {
            OwnerName = petOwner.OwnerName,
            OwnerPhone = petOwner.OwnerPhone,
            OwnerEmail = petOwner.OwnerEmail,
            OwnerAddress = petOwner.OwnerAddress,
            OwnerCity = petOwner.OwnerCity,
            OwnerState = petOwner.OwnerState,
            OwnerZipCode = petOwner.OwnerZipCode,
            Pets = [.. _originalPetList]
        };

        // Show available pets in the popup
        FilterPets();
        _currentPopup = new PetOwnerEditFormPopup(this);
        await Shell.Current.ShowPopupAsync(_currentPopup);
    }


    [RelayCommand]
    private async Task CancelAddPetOwner()
    {
        SelectedPetOwner = null;
        _petsToRemove.Clear();
        if (_currentPopup != null)
            await _currentPopup.CloseAsync(false);
    }

    [RelayCommand]
    private async Task CancelPetOwnerEdit()
    {
        if (_originalPetOwner != null)
        {
            _originalPetList.Clear();
            foreach (var pet in _originalPetList)
            {
                pet.Owner = _originalPetOwner;
                pet.IsOwned = true;
                _originalPetList.Add(pet);            
            }
            _petsToRemove.Clear();
        }

        SelectedPetOwner = null;
        _originalPetOwner = null;
        _originalPetList.Clear();

        if (_currentPopup != null)
            await _currentPopup.CloseAsync(false);
        FilterPets();
    }
}
