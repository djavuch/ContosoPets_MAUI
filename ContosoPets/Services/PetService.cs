using ContosoPets.Models;
using ContosoPets.Utilities;
using System.Collections.ObjectModel;

namespace ContosoPets.Services;

public class PetService
{
    private readonly ObservableCollection<PetModel> _pets = [];
    public ObservableCollection<PetModel> Pets => _pets;

    public PetService()
    {
        InitializeDemoData();
    }

    private void InitializeDemoData()
    {
        foreach (var pet in PetDemoDataInitializer.GetDemoPets().ToList())
        {
            _pets.Add(pet);
        }
    }

    public void AddPet(PetModel pet)
    {
        if (string.IsNullOrEmpty(pet.PetId))
        {
            pet.PetId = Guid.NewGuid().ToString();
        }
        Pets.Add(pet);
    }

    public void UpdatePet(PetModel originalPetData, PetModel newPetData)
    {
        originalPetData.PetName = newPetData.PetName;
        originalPetData.PetSpecie = newPetData.PetSpecie;
        originalPetData.PetAge = newPetData.PetAge;
        originalPetData.PetPhysicalDescription = newPetData.PetPhysicalDescription;
        originalPetData.PetPersonalDescription = newPetData.PetPersonalDescription;
        originalPetData.Owner = newPetData.Owner; 
       
    }

    public void DeletePet(PetModel pet)
    {
        if (pet != null)
        {
            if (pet.Owner != null)
            {
                pet.Owner.Pets.Remove(pet);
                pet.Owner = null; // Clear the owner reference  
            }
            Pets.Remove(pet);
        }
    }

    public void DeleteSelectedPets(IEnumerable<PetModel> selectedPetToRemove)
    {
        if (selectedPetToRemove == null || !selectedPetToRemove.Any()) 
            return;
        foreach (var pet in selectedPetToRemove)
        {
            DeletePet(pet);
        }
    }
}
