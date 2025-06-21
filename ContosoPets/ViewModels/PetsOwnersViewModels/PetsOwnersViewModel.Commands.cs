using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using ContosoPets.Services;
using ContosoPets.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContosoPets.Models;
using System.Collections.ObjectModel;
using ContosoPets.View.PetsOwnersView;
using System.Diagnostics;

namespace ContosoPets.ViewModels.PetsOwnersViewModels;

public partial class PetsOwnersViewModel
{
    // Save currect pet owner
    [RelayCommand]
    private async Task SavePetOwner()
    {
        //Check if pet is available. Commented out because I added filtering logic in the popup and unavailable pets are not shown in the list.
        //foreach (var pet in SelectedPetOwner.Pets)
        //{
        //    var existingOwner = PetsOwners.FirstOrDefault(o => o != _originalPetOwner && o.Pets.Any(p => p.PetId == pet.PetId));
        //    if (existingOwner != null)
        //    {
        //        await Shell.Current.DisplayAlert("Error", $"Pet '{pet.PetName}' is already owned by '{existingOwner.OwnerName}'. Please select a different pet.", "OK");
        //        return;
        //    }
        //    pet.Owner = SelectedPetOwner; // Set the owner of the pet
        //}

        if (_originalPetOwner != null)
        {
            bool isDuplicateCheckPassed = await _petOwnerService.CheckDuplicateOwnerEmailOrPhone(SelectedPetOwner);
            if (!isDuplicateCheckPassed)
            {
                return; 
            }

            foreach (var pet in _petsToRemove)
            {
                pet.Owner = null;  
                pet.IsOwned = false; 
            }

            _originalPetOwner.Pets.Clear();

            foreach (var pet in SelectedPetOwner.Pets)
            {
                pet.Owner = SelectedPetOwner;
                pet.IsOwned = true;
                _originalPetOwner.Pets.Add(pet);
            }    
                

            _originalPetList = [.. _originalPetOwner.Pets];

            _petsToRemove.Clear();
        }
        else
        {
            bool isDuplicateCheckPassed = await _petOwnerService.CheckDuplicateOwnerEmailOrPhone(SelectedPetOwner);
            if (!isDuplicateCheckPassed)
            {
                return; 
            }

            // Generate unuqie ID for new pet owner with first chars of the name and random numbers
            SelectedPetOwner.OwnerId = GenerateUniqueId.CreateUniqueId(SelectedPetOwner.OwnerName);
            
            // Add new pet owner
            foreach (var pet in SelectedPetOwner.Pets)
            {
                pet.Owner = SelectedPetOwner;
                pet.IsOwned = true;

                if (pet.Owner?.OwnerId != SelectedPetOwner.OwnerId)
                    pet.Owner.OwnerId = SelectedPetOwner.OwnerId;

                if (!_petService.Pets.Contains(pet))
                {           
                    _petService.AddPet(pet);
                }
            }

            _petOwnerService.AddPetOwner(SelectedPetOwner);
            _petOwnerService.UpdateOwnerIndexes();
            _petsToRemove.Clear();
        }

        _petOwnerService.SyncPetsWithOwners(_petService);
        await _currentPopup.CloseAsync();
        FilterPets();

        Debug.WriteLine($"Pets count to save: {SelectedPetOwner.Pets.Count}");
    }

    // Delete current pet owner
    [RelayCommand]
    private async Task DeletePetOwner(PetsOwnersModel petOwner)
    {
        if (petOwner == null) return;

        var popup = new DeleteConfirmationPopup();
        var viewModel = new DeleteConfirmationViewModel(popup);

        popup.BindingContext = viewModel;
        var confirmed = await Shell.Current.ShowPopupAsync(popup);

        if (confirmed is bool)
        {
            foreach (var pet in petOwner.Pets)
            {
                pet.Owner = null; 
            }

            _petOwnerService.DeletePetOwner(petOwner);
            PetsOwners.Remove(petOwner);
            _petOwnerService.SyncPetsWithOwners(_petService);
            _petOwnerService.UpdateOwnerIndexes();
            FilterPets();
        }
    }

    // Delete selected pet owners
    [RelayCommand]
    private async Task DeleteSelectedOwners()
    {
        var selectedOwnersToRemove = PetsOwners.Where(o => o.SelectedOwnerToRemove).ToList();

        if (selectedOwnersToRemove.Count == 0) return;

        var popup = new DeleteConfirmationPopup();
        var viewModel = new DeleteConfirmationViewModel(popup);
        popup.BindingContext = viewModel;

        var confirmed = await Shell.Current.ShowPopupAsync(popup);

        if (confirmed is bool result && result)
        {
            foreach (var owner in selectedOwnersToRemove)
            {
                _petOwnerService.DeletePetOwner(owner);
                PetsOwners.Remove(owner);
            }
            _petOwnerService.SyncPetsWithOwners(_petService);
            _petOwnerService.UpdateOwnerIndexes();
        }
    }
}