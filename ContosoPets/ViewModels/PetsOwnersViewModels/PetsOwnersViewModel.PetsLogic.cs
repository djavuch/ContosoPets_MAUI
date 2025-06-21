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
    private readonly ObservableCollection<PetsModel> _petsToRemove = [];

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
    private ObservableCollection<PetsModel> _filteredPets;

    // Collection of pets that are not owned and have no owner
    private PetsModel? _selectedPet;
    public PetsModel? SelectedPet
    {
        get => _selectedPet;
        set
        {
            if (SetProperty(ref _selectedPet, value) && value != null)
            {
                if (SelectedPetOwner.Pets != null && !SelectedPetOwner.Pets.Contains(value))
                    SelectedPetOwner.Pets.Add(value);
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
    private void AddPetToOwner()
    {
        if (SelectedPet != null && !SelectedPet.IsOwned && SelectedPet.Owner == null && 
            !SelectedPetOwner.Pets.Any(p => p.PetId == SelectedPet.PetId))
        {
            // Debug.WriteLine($"Adding pet: {SelectedPet.PetName}, Id: {SelectedPet.PetId}");

            foreach (var owner in PetsOwners.Where(o => o != SelectedPetOwner))
            {
                var petToRemove = owner.Pets.FirstOrDefault(p => p.PetId == SelectedPet.PetId);
                if (petToRemove != null)
                {
                    // Debug.WriteLine($"Removing pet {SelectedPet.PetName} from owner {owner.OwnerName}");
                    owner.Pets.Remove(petToRemove);
                    petToRemove.Owner = null;
                }
            }

            // Set the owner of the pet and add it to the owner's pets collection
            SelectedPet.Owner = SelectedPetOwner;
            SelectedPet.IsOwned = true;
            SelectedPetOwner.Pets.Add(SelectedPet);

            if (_petsToRemove.Any(p => p.PetId == SelectedPet.PetId))
            {
                var petToRemove = _petsToRemove.First(p => p.PetId == SelectedPet.PetId);
                _petsToRemove.Remove(petToRemove);
                // Debug.WriteLine($"Removed pet {SelectedPet.PetName} from _petsToRemove");
            }

            _petOwnerService.SyncPetsWithOwners(_petService);
            OnPropertyChanged(nameof(SelectedPetOwner));
            FilterPets(); 
            SelectedPet = null; 
        }
        else
        {
            // Debug.WriteLine("SelectedPet is null or already in SelectedPetOwner.Pets");
            SelectedPet = null; 
        }
    }

    [RelayCommand]
    private void RemovePetFromOwner(PetsModel pet)
    {
        // Debug.WriteLine($"RemovePetFromOwner called with pet: {pet?.PetName}");
        if (pet != null && SelectedPetOwner.Pets.Any(p => p.PetId == pet.PetId))
        {
            if (!_petsToRemove.Any(p => p.PetId == pet.PetId))
            {
                _petsToRemove.Add(pet);
            }

            SelectedPetOwner.Pets.Remove(pet);  
            OnPropertyChanged(nameof(SelectedPetOwner));
            FilterPets();
        }
    }
}