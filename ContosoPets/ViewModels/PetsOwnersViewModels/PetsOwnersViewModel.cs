using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContosoPets.Models;
using ContosoPets.Services;
using ContosoPets.Utilities;
using ContosoPets.View.PetsOwnersView;
using ContosoPets.ViewModels.PetsViewModels;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ContosoPets.ViewModels.PetsOwnersViewModels;

public partial class PetsOwnersViewModel : ObservableObject
{
    // Service to manage owners data
    private readonly PetOwnerService _petOwnerService;

    private readonly PetService _petService;

    private readonly ObservableCollection<PetOwnerModel> _allPetOwners;

    // The original pet owner object before editing
    private PetOwnerModel? _originalPetOwner;

    // The current popup instance
    private Popup? _currentPopup;

    [ObservableProperty]
    private ObservableCollection<PetOwnerModel> _petsOwners;

    [ObservableProperty]
    private ObservableCollection<PetModel> _petsCollection;

    // Selected pet owner for editing
    [ObservableProperty]
    private PetOwnerModel _selectedPetOwner;

    // Original pet list for owner
    private List<PetModel> _originalPetList = [];

    public PetsOwnersViewModel(PetOwnerService petOwnerService, PetService petService, PetsViewModel petsViewModel)
    {
        _petOwnerService = petOwnerService;
        _petService = petService;
        _allPetOwners = new ObservableCollection<PetOwnerModel>(_petOwnerService.PetsOwners);
        PetsOwners = new ObservableCollection<PetOwnerModel>(_allPetOwners);
        SelectedPetOwner = new PetOwnerModel();
        PetsCollection = petsViewModel.Pets;
        FilteredPets = [.. PetsCollection];
        _petOwnerService.SyncPetsWithOwners(_petService);
        UpdateOwnerIndexes();
        FilterPetOwners();
    }  
    
    private void UpdateOwnerIndexes()
    {
        for (int i = 0; i < PetsOwners.Count; i++)
        {
            PetsOwners[i].DisplayIndexOwner = i + 1;
        }
    }
}