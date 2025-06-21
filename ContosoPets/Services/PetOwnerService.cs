using ContosoPets.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ContosoPets.Services;

public class PetOwnerService
{
    private readonly ObservableCollection<PetsOwnersModel> _petsOwners = [];

    // Service to manage pet data
    private readonly PetService _petService;

    public ObservableCollection<PetsOwnersModel> PetsOwners { get; } =
    [
        new PetsOwnersModel()
        {
            OwnerId = "1",
            OwnerName = "John Doe",
            OwnerPhone = "123-456-7890",
            OwnerEmail = "johndoe@mail.com",
            OwnerAddress = "123 Main St",
            OwnerCity = "Seattle",
            OwnerState = "WA",
            OwnerZipCode = "98101",
            Pets = []
        },
        new PetsOwnersModel()
        {
            OwnerId = "2",
            OwnerName = "Jane Smith",
            OwnerPhone = "987-654-3210",
            OwnerEmail = "j_smith@mail.com",
            OwnerAddress = "456 Elm St",
            OwnerCity = "Bellevue",
            OwnerState = "WA",
            OwnerZipCode = "98004",
            Pets = []
        }
    ];

    public PetOwnerService(PetService petService)
    {
        _petService = petService;
        SyncPetsWithOwners(petService);
    }

    public void SyncPetsWithOwners(PetService petService)
    {
        foreach (var owner in PetsOwners)
        {
            owner.Pets.Clear();
        }

        foreach (var pet in petService.Pets)
        {
            var owner = PetsOwners.FirstOrDefault(o => o.OwnerId == pet.Owner?.OwnerId || o.OwnerId == pet.Owner?.OwnerName); 
            pet.Owner = owner;
            pet.IsOwned = owner != null;

            if (owner != null)
            {
                owner.Pets.Add(pet);
            }
        }
    }

    public async Task AddPetOwner(PetsOwnersModel petOwner)
    {
        if (petOwner == null || string.IsNullOrEmpty(petOwner.OwnerId))
            throw new ArgumentException("Invalid owner or OwnerId");

        bool isValid = await CheckDuplicateOwnerEmailOrPhone(petOwner);
        if (!isValid)
            return;

        petOwner.Pets ??= new ObservableCollection<PetsModel>();
        PetsOwners.Add(petOwner);
        SyncPetsWithOwners(_petService);
    }

    public async Task UpdateOwner(PetsOwnersModel oldPetOwnerData, PetsOwnersModel newPetOwnerData)
    {
        var index = PetsOwners.IndexOf(oldPetOwnerData);
        if (index != -1)
        {
            bool isValid = await CheckDuplicateOwnerEmailOrPhone(newPetOwnerData);
            if (!isValid)
                return;

            oldPetOwnerData.OwnerName = newPetOwnerData.OwnerName;
            oldPetOwnerData.OwnerPhone = newPetOwnerData.OwnerPhone.Trim();
            oldPetOwnerData.OwnerEmail = newPetOwnerData.OwnerEmail.Trim();
            oldPetOwnerData.OwnerAddress = newPetOwnerData.OwnerAddress.Trim();
            oldPetOwnerData.OwnerCity = newPetOwnerData.OwnerCity.Trim();
            oldPetOwnerData.OwnerState = newPetOwnerData.OwnerState.Trim();
            oldPetOwnerData.OwnerZipCode = newPetOwnerData.OwnerZipCode.Trim();
            oldPetOwnerData.Pets = newPetOwnerData.Pets;
            SyncPetsWithOwners(_petService);
        }
    }

    public void DeletePetOwner(PetsOwnersModel owner)
    {
        if (owner == null)
        {
            foreach (var pet in owner.Pets)
            {
                pet.Owner = null; 
            }
            PetsOwners.Remove(owner);
            SyncPetsWithOwners(_petService);
        }
    }

    public void UpdateOwnerIndexes()
    {
        for (int i = 0; i < PetsOwners.Count; i++)
        {
            PetsOwners[i].DisplayIndexOwner = i + 1;
        }
    }

    public async Task<bool> CheckDuplicateOwnerEmailOrPhone(PetsOwnersModel ownerToCheck)
    {
        var duplicateEmail = PetsOwners.Any(o =>
            o != ownerToCheck &&
            string.Equals(o.OwnerEmail, ownerToCheck.OwnerEmail, StringComparison.OrdinalIgnoreCase));

        var duplicatePhone = PetsOwners.Any(o =>
            o != ownerToCheck &&
            string.Equals(o.OwnerPhone, ownerToCheck.OwnerPhone, StringComparison.OrdinalIgnoreCase));

        if (duplicateEmail)
        {
            await Shell.Current.DisplayAlert("Duplicate Email", "An owner with this email already exists.", "OK");
        }

        if (duplicatePhone)
        {
            await Shell.Current.DisplayAlert("Duplicate Phone", "An owner with this phone number already exists.", "OK");
        }

        return !duplicateEmail && !duplicatePhone;
    }
}
