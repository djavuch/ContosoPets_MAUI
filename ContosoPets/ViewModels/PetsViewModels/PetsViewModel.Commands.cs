using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using ContosoPets.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoPets.ViewModels.PetsViewModels;

public partial class PetsViewModel
{
    [RelayCommand]
    private async Task SavePet()
    {
        bool isValid = await SelectedPet.Validate();
        if (!isValid)
        {
            await Shell.Current.DisplayAlert("Validation Error", string.Join("\n", SelectedPet.GetErrors().Select(e => e.ErrorMessage)), "OK");
            return;
        }

        if (_originalPet != null)
        {
            _petsService.UpdatePet(_originalPet, SelectedPet);
        }
        else
        {
            _petsService.AddPet(SelectedPet);
            _petsService.UpdateIndexes();
            Debug.WriteLine($"Added New Pet: ID={SelectedPet.PetId}, Name={SelectedPet.PetName}, Specie={SelectedPet.PetSpecie}, Age={SelectedPet.PetAge}, PhysicalDesc={SelectedPet.PetPhysicalDescription}, PersonalDesc={SelectedPet.PetPersonalDescription}, Owner={(SelectedPet.Owner != null ? SelectedPet.Owner.ToString() : "None")}");
        }

        _petOwnerService.SyncPetsWithOwners(_petsService);
        await _currentPopup.CloseAsync();
    }

    [RelayCommand]
    private async Task DeletePet(PetsModel pet)
    {
        if (pet == null) return;

        var popup = new DeleteConfirmationPopup();
        var viewModel = new DeleteConfirmationViewModel(popup);

        popup.BindingContext = viewModel;

        var confirmed = await Shell.Current.ShowPopupAsync(popup);

        if (confirmed is bool)
        {
            _petsService.DeletePet(pet);
            Pets.Remove(pet);
            _petOwnerService.SyncPetsWithOwners(_petsService);
            _petsService.UpdateIndexes();
        }
    }

    [RelayCommand]
    private void ReleasePetOwnership()
    {
        if (SelectedPet == null || SelectedPet.Owner == null)
            return;

        SelectedPet.Owner = null;
        SelectedPet.IsOwned = false;
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

    [RelayCommand]
    private async Task DeleteSelectedPets()
    {
        var selectedPetsToRemove = Pets.Where(p => p.SelectedPetToRemove).ToList();

        if (selectedPetsToRemove.Count == 0) return;

        var popup = new DeleteConfirmationPopup();

        var viewModel = new DeleteConfirmationViewModel(popup);

        popup.BindingContext = viewModel;

        var confirmed = await Shell.Current.ShowPopupAsync(popup);

        if (confirmed is bool result && result)
        {
            foreach (var pet in selectedPetsToRemove)
            {
                _petsService.DeletePet(pet);
                Pets.Remove(pet);
            }
            _petsService.UpdateIndexes();
        }
    }
}
