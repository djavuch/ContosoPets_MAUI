using ContosoPets.Models;
using ContosoPets.Utilities;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ContosoPets.Services;

public class PetOwnerService
{
    private readonly ObservableCollection<PetOwnerModel> _petsOwners = [];
    private readonly PetService _petService;
    public ObservableCollection<PetOwnerModel> PetsOwners => _petsOwners;

    public PetOwnerService(PetService petService)
    {
        _petService = petService;
        InitializeDemoData();
        SyncPetsWithOwners(petService);
    }

    private void InitializeDemoData()
    {
        var demoOwners = PetOwnerDemoDataInitializer.GetDemoPetsOwners().ToList();
        foreach (var petOwner in demoOwners)
        {
            _petsOwners.Add(petOwner);
        }
    }

    public void AddPetOwner(PetOwnerModel petOwner)
    {
       ArgumentNullException.ThrowIfNull(petOwner);

       if (string.IsNullOrEmpty(petOwner.OwnerId))
       {
           petOwner.OwnerId = Guid.NewGuid().ToString(); 
       }

      petOwner.Pets ??= [];

      foreach (var pet in petOwner.Pets)
      {
          pet.Owner = petOwner;
          pet.IsOwned = true;
          if (!_petService.Pets.Contains(pet))
          {
              _petService.AddPet(pet);
          }
      }

      _petsOwners.Add(petOwner);
      SyncPetsWithOwners(_petService);
    }

    public void UpdatePetOwner(PetOwnerModel owner, IEnumerable<PetModel> petsToRemove = null, PetOwnerModel originalOwner = null)
    {
        if (owner == null || string.IsNullOrEmpty(owner.OwnerId))
            throw new ArgumentException("Invalid owner or OwnerId");

        if (petsToRemove != null)
        {
            foreach (var pet in petsToRemove)
            {
                pet.Owner = null;
                pet.IsOwned = false;
            }
        }    

        originalOwner.Pets.Clear();
        foreach (var pet in owner.Pets)
        {
            pet.Owner = originalOwner;
            pet.IsOwned = true;
            originalOwner.Pets.Add(pet);
        }

        originalOwner.OwnerName = owner.OwnerName;
        originalOwner.OwnerPhone = owner.OwnerPhone.Trim();
        originalOwner.OwnerEmail = owner.OwnerEmail.Trim();
        originalOwner.OwnerAddress = owner.OwnerAddress.Trim();
        originalOwner.OwnerCity = owner.OwnerCity.Trim();
        originalOwner.OwnerState = owner.OwnerState.Trim();
        originalOwner.OwnerZipCode = owner.OwnerZipCode.Trim();
        
        SyncPetsWithOwners(_petService);
    }

    public void DeletePetOwner(PetOwnerModel owner)
    {
        if (owner == null)
            throw new ArgumentNullException(nameof(owner));
        {
            foreach (var pet in owner.Pets)
            {
                pet.Owner = null; 
                pet.IsOwned = false;
            }
            _petsOwners.Remove(owner);
            SyncPetsWithOwners(_petService);
        }
    }

    public void SyncPetsWithOwners(PetService petService)
    {
        var petsOwnersCopy = PetsOwners.ToList();

        foreach (var owner in petsOwnersCopy)
        {
            owner.Pets.Clear();
        }

        foreach (var pet in petService.Pets.ToList())
        {
            var owner = petsOwnersCopy.FirstOrDefault(o => o.OwnerId == pet.Owner?.OwnerId); 
            pet.Owner = owner;
            pet.IsOwned = owner != null;

            if (owner != null)
            {
                owner.Pets.Add(pet);
            }
        }
    }
}
