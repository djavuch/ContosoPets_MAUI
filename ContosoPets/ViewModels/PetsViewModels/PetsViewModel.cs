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

    private PetOwnerService _petOwnerService;

    private readonly ObservableCollection<PetModel> _allPets;

    // The original pet object before editing
    private PetModel? _originalPet;

    // The current popup instance
    private Popup? _currentPopup;

    // List of species options for the picker
    public ObservableCollection<string> SpeciesOptions { get; } =
    [
        "Dog", "Cat",
    ];

    // The list of pets
    [ObservableProperty]
    private ObservableCollection<PetModel> _pets;

    [ObservableProperty]
    private ObservableCollection<PetOwnerModel> _petsOwners;

    [ObservableProperty]
    private PetModel _selectedPet;

    public PetsViewModel(PetService petsService, PetOwnerService petOwnerService)
    {
        _petsService = petsService;
        _petOwnerService = petOwnerService;
        _allPets = new ObservableCollection<PetModel>(_petsService.Pets);
        Pets = new ObservableCollection<PetModel>(_allPets);
        SelectedPet = new PetModel();
        UpdatePetIndexes();
        FilterPagePets();
    }

    private void UpdatePetIndexes()
    {
        for (int i = 0; i < Pets.Count; i++)
        {
            Pets[i].DisplayIndexPet = i + 1;
        }
    }
}