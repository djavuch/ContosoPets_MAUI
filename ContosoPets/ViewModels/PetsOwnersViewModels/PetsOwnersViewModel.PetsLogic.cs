using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContosoPets.Models;
using ContosoPets.Services;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoPets.ViewModels.PetsOwnersViewModels;

public partial class PetsOwnersViewModel
{
    // Temporary collection to hold pets that are being removed from an owner
    private readonly ObservableCollection<PetModel> _petsToRemove = [];

    private string? _petSearchText;
    public string? PetSearchText
    {
        get => _petSearchText;
        set
        {
            SetProperty(ref _petSearchText, value);
            FilterPets();
        }
    }

    [ObservableProperty]
    private ObservableCollection<PetModel> _filteredPets;

    // Collection of pets that are not owned and have no owner
    private PetModel? _selectedPet;
    public PetModel? SelectedPet
    {
        get => _selectedPet;
        set
        {
            if (SetProperty(ref _selectedPet, value) && value != null)
            {
                if (SelectedPetOwner.Pets != null && !SelectedPetOwner.Pets.Contains(value))
                {
                    value.IsOwned = true;
                    value.Owner = SelectedPetOwner;
                    SelectedPetOwner.Pets.Add(value);
                    FilterPets(); 
                }
            }
        }
    }

    private void FilterPets()
    {
        var availablePets = PetsCollection.Where(p => !p.IsOwned && p.Owner == null);

        if (string.IsNullOrWhiteSpace(PetSearchText))
        {
            FilteredPets = [.. availablePets];
        }
        else
        {
            FilteredPets = [.. availablePets.Where(p => p.PetName.Contains(PetSearchText, StringComparison.OrdinalIgnoreCase))];
        }
    }

    [RelayCommand]
    private void AddPetToOwner(PetModel pet)
    {
        if (pet != null && !SelectedPetOwner.Pets.Any(p => p.PetId == pet.PetId))
        {
            SelectedPetOwner.Pets.Add(pet);
            _petsToRemove.Remove(pet);
            OnPropertyChanged(nameof(SelectedPetOwner));
            FilterPets();
            Debug.WriteLine($"Added pet: {pet.PetName} to owner: {SelectedPetOwner.OwnerName}");
        }
    }

    [RelayCommand]
    private void RemovePetFromOwner(PetModel pet)
    {
        // Debug.WriteLine($"RemovePetFromOwner called with pet: {pet?.PetName}");
        if (pet != null && SelectedPetOwner.Pets.Any(p => p.PetId == pet.PetId))
        {
            if (!_petsToRemove.Any(p => p.PetId == pet.PetId))
            {
                _petsToRemove.Add(pet);
            }

            pet.Owner = null;
            pet.IsOwned = false;

            SelectedPetOwner.Pets.Remove(pet);  
            OnPropertyChanged(nameof(SelectedPetOwner));
            FilterPets();
        }
    }
}