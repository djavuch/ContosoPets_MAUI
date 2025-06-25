using CommunityToolkit.Mvvm.ComponentModel;
using ContosoPets.ViewModels.PetsViewModels;
using System.ComponentModel.DataAnnotations;

namespace ContosoPets.Models;

public partial class PetModel : ObservableValidator
{
    private int _displayIndexPet;

    public int DisplayIndexPet
    {
        get => _displayIndexPet;
        set => SetProperty(ref _displayIndexPet, value);
    }

    private bool _selectedPetToRemove;

    public bool SelectedPetToRemove
    {
        get => _selectedPetToRemove;
        set => SetProperty(ref _selectedPetToRemove, value);
    }

    [ObservableProperty]
    private string _petId;

    [ObservableProperty]
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, ErrorMessage = "Maximum length of pet name cannot exceed 50 characters, but cannot be shorter than 2.", MinimumLength = 2)]
    private string _petName;

    [ObservableProperty]
    [Required(ErrorMessage = "Specie is required")]
    private string _petSpecie;

    [ObservableProperty]
    [Required(ErrorMessage = "Age is required")]
    [Range(0, 25, ErrorMessage = "Age must be between 0 and 25 years.")]
    private int? _petAge;

    [ObservableProperty]
    [StringLength(2000, ErrorMessage = "Maximum length of physical description cannot exceed 1000 characters.")]
    private string _petPhysicalDescription;

    [ObservableProperty]
    [StringLength(2000, ErrorMessage = "Maximum length of physical description cannot exceed 1000 characters.")]
    private string _petPersonalDescription;

    [ObservableProperty]
    private bool _isOwned;

    private PetOwnerModel? _owner;

    // Check if the pet is owned
    public PetOwnerModel? Owner
    {
        get => _owner;
        set
        {
            if (SetProperty(ref _owner, value))
            {
                IsOwned = value != null;
            }
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj is PetModel other)
        {
            return PetId == other.PetId;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return PetId.GetHashCode();
    }

    public async Task<bool> Validate()
    {
        ValidateAllProperties();
        return !HasErrors;
    }
}