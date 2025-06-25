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
    private async Task AddPetOwner()
    {
        try
        {
            bool isValid = await SelectedPetOwner.Validate();
            if (!isValid)
            {
                var errors = string.Join("\n", SelectedPetOwner.GetErrors().Select(e => e.ErrorMessage));
                await Shell.Current.DisplayAlert("Validation Error", errors, "OK");
                return;
            }
            _petOwnerService.AddPetOwner(SelectedPetOwner);
            UpdateOwnerIndexes();
            FilterPetOwners();
            FilterPets();
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
    private async Task EditPetOwner()
    {
        try
        {
            bool isValid = await SelectedPetOwner.Validate();
            if (!isValid)
            {
                var errors = string.Join("\n", SelectedPetOwner.GetErrors().Select(e => e.ErrorMessage));
                await Shell.Current.DisplayAlert("Validation Error", errors, "OK");
                return;
            }
            _petOwnerService.UpdatePetOwner(SelectedPetOwner, _petsToRemove, _originalPetOwner);
            FilterPetOwners();
            FilterPets();
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

    // Delete current pet owner
    [RelayCommand]
    private async Task DeletePetOwner(PetOwnerModel petOwner)
    {
        if (petOwner == null) return;

        var popup = new DeleteConfirmationPopup();
        var viewModel = new DeleteConfirmationViewModel(popup);

        popup.BindingContext = viewModel;
        var confirmed = await Shell.Current.ShowPopupAsync(popup);

        if (confirmed is bool result && result)
        {
            _petOwnerService.DeletePetOwner(petOwner);
            FilterPetOwners();
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
            FilterPetOwners();
            FilterPets();
        }
    }
}