using ContosoPets.Models;
using System.Collections.ObjectModel;

namespace ContosoPets.Services;

public class PetService
{
    public ObservableCollection<PetsModel> Pets { get; } =
    [
       new() {
           PetId = "1",
           PetName = "Max",
           PetSpecie = "Dog",
           PetAge = 5,
           PetPhysicalDescription = "Brown, medium-sized dog",
           PetPersonalDescription = "Friendly and playful",
           Owner = new PetsOwnersModel { OwnerId = "1" },
           IsOwned = true
       },
       new() {
           PetId = "2",
           PetName = "Whiskers",
           PetSpecie = "Cat",
           PetAge = 3,
           PetPhysicalDescription = "Black and white cat",
           PetPersonalDescription = "Curious and independent",
           Owner = new PetsOwnersModel { OwnerId = "2" },
           IsOwned = true
       },
       new() {
           PetId = "3",
           PetName = "Suzy",
           PetSpecie = "Cat",
           PetAge = 4,
           PetPhysicalDescription = "Black cat",
           PetPersonalDescription = "Curious and independent",
           Owner = null,
           IsOwned = false
       }
    ];

    public void AddPet(PetsModel pet)
    {
        int petCount = 0;

        if (string.IsNullOrEmpty(pet.PetId))
        {
            pet.PetId = (pet.PetSpecie[..1] + (petCount+ 1)).ToString();
        }
        Pets.Add(pet);
    }

    public void UpdatePet(PetsModel oldPetData, PetsModel newPetData)
    {
        var index = Pets.IndexOf(oldPetData);
        if (index != -1)
        {
            oldPetData.PetName = newPetData.PetName;
            oldPetData.PetSpecie = newPetData.PetSpecie;
            oldPetData.PetAge = newPetData.PetAge;
            oldPetData.PetPhysicalDescription = newPetData.PetPhysicalDescription;
            oldPetData.PetPersonalDescription = newPetData.PetPersonalDescription;
            oldPetData.Owner = newPetData.Owner; 
        }
    }

    public void DeletePet(PetsModel pet)
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

    public void UpdateIndexes()
    {
        for (int i = 0; i < Pets.Count; i++)
        {
            Pets[i].DisplayIndexPet = i + 1;
        }
    }
}
