using CommunityToolkit.Mvvm.ComponentModel;
using ContosoPets.Services;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ContosoPets.Models;

public partial class PetOwnerModel : ObservableValidator
{
    [ObservableProperty]
    private ObservableCollection<PetOwnerModel> _petsOwners;

    public PetOwnerModel()
    {
        Pets = [];
    }

    private bool _selectedOwnerToRemove;
    public bool SelectedOwnerToRemove
    {
        get => _selectedOwnerToRemove;
        set => SetProperty(ref _selectedOwnerToRemove, value);
    }

    private int _displayIndexOwner;
    public int DisplayIndexOwner
    {
        get => _displayIndexOwner;
        set => SetProperty(ref _displayIndexOwner, value);
    }

    [ObservableProperty]
    private string _ownerId;

    [ObservableProperty]
    [Required(ErrorMessage = "Name is required")]
    [MinLength(3, ErrorMessage = "Owner name must be at least 3 characters long")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Owner name can only contain letters and spaces")]
    private string _ownerName;

    [ObservableProperty]
    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Invalid phone number format")]
    private string _ownerPhone;

    [ObservableProperty]
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address format")]
    private string _ownerEmail;

    [ObservableProperty]
    [Required(ErrorMessage = "Address is required")]
    [StringLength(100, ErrorMessage = "Address cannot exceed 100 characters")]
    private string _ownerAddress;

    [ObservableProperty]
    [Required(ErrorMessage = "City is required")]
    [StringLength(50, ErrorMessage = "City cannot exceed 50 characters")]
    private string _ownerCity;

    [ObservableProperty]
    [Required(ErrorMessage = "State is required")]
    [StringLength(50, ErrorMessage = "State cannot exceed 50 characters")]
    private string _ownerState;

    [ObservableProperty]
    [Required(ErrorMessage = "Zip code is required")]
    [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid zip code format")]
    private string _ownerZipCode;

    [ObservableProperty]
    private ObservableCollection<PetModel> _pets;

    public async Task<bool> Validate()
    {
        ValidateAllProperties();
        return !HasErrors;
    }
}
