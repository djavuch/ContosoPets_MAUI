using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContosoPets.Models;
using ContosoPets.Services;
using ContosoPets.View.PetsView;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ContosoPets.ViewModels.PetsViewModels;
public partial class PetsViewModel : ObservableObject
{
    // Service to manage pet data
    private readonly PetService _petsService;

    private readonly PetOwnerService _petOwnerService;

    // The original pet object before editing
    private PetsModel _originalPet;

    // The current popup instance
    private Popup _currentPopup;

    // List of species options for the picker
    public ObservableCollection<string> SpeciesOptions { get; } =
    [
        "Dog", "Cat",
    ];

    // The list of pets
    [ObservableProperty]
    private ObservableCollection<PetsModel> _pets;

    [ObservableProperty]
    private ObservableCollection<PetsOwnersModel> _petsOwners;

    [ObservableProperty]
    private PetsModel _selectedPet;

    // Flag to indicate if the form is in edit mode
    [ObservableProperty]
    private bool _isEditMode;

    public PetsViewModel(PetService petsService, PetOwnerService petOwnerService)
    {
        _petsService = petsService;
        _petOwnerService = petOwnerService;
        Pets = _petsService.Pets;
        SelectedPet = new PetsModel();
        _petsService.UpdateIndexes();
    }
}