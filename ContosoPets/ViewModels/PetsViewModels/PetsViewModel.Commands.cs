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
    private async Task AddPet()
    {
        try
        {
            bool isValid = await SelectedPet.Validate();
            if (!isValid)
            {
                await Shell.Current.DisplayAlert("Validation Error", string.Join("\n", SelectedPet.GetErrors().Select(e => e.ErrorMessage)), "OK");
                return;
            }

            _petsService.AddPet(SelectedPet);
            FilterPagePets();
            await _currentPopup.CloseAsync();
        }
        catch (InvalidOperationException ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
        catch (ArgumentException ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
    }

    [RelayCommand]
    private async Task EditPet()
    {
        try
        {
            bool isValid = await SelectedPet.Validate();
            if (!isValid)
            {
                await Shell.Current.DisplayAlert("Validation Error", string.Join("\n", SelectedPet.GetErrors().Select(e => e.ErrorMessage)), "OK");
                return;
            }
            _petsService.UpdatePet(SelectedPet, _originalPet);
            FilterPagePets();
            await _currentPopup.CloseAsync();
        }
        catch (InvalidOperationException ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
        catch (ArgumentException ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
    }

    [RelayCommand]
    private async Task DeletePet(PetModel pet)
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
            FilterPagePets();
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
            _petsService.DeleteSelectedPets(selectedPetsToRemove);
            foreach (var pet in selectedPetsToRemove)
            {
                Pets.Remove(pet);
            }
            FilterPagePets();
        }
    }
}
