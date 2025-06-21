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

    // The original pet owner object before editing
    private PetsOwnersModel? _originalPetOwner;

    // The current popup instance
    private Popup _currentPopup;

    [ObservableProperty]
    private ObservableCollection<PetsOwnersModel> _petsOwners;

    [ObservableProperty]
    private ObservableCollection<PetsModel> _petsCollection;

    // Selected pet owner for editing
    [ObservableProperty]
    private PetsOwnersModel _selectedPetOwner;

    // Original pet list for owner
    private List<PetsModel> _originalPetList = [];

    public PetsOwnersViewModel(PetOwnerService petOwnerService, PetService petService, PetsViewModel petsViewModel)
    {
        _petOwnerService = petOwnerService;
        _petService = petService;
        PetsOwners = _petOwnerService.PetsOwners;
        SelectedPetOwner = new PetsOwnersModel();
        PetsCollection = petsViewModel.Pets;
        FilteredPets = [.. PetsCollection];
        _petOwnerService.SyncPetsWithOwners(_petService);
        _petOwnerService.UpdateOwnerIndexes();
    }
}