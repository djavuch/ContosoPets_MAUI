using ContosoPets.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoPets.Utilities;

public static class PetDemoDataInitializer
{
    public static IEnumerable<PetModel> GetDemoPets()
    {
        return new ObservableCollection<PetModel>
        {
            new()
            {
                PetId = "1",
                PetName = "Max",
                PetSpecie = "Dog",
                PetAge = 5,
                PetPhysicalDescription = "Brown, medium-sized dog",
                PetPersonalDescription = "Friendly and playful",
                Owner = new PetOwnerModel { OwnerId = "1" },
                IsOwned = true
            },
            new()
            {
                PetId = "2",
                PetName = "Whiskers",
                PetSpecie = "Cat",
                PetAge = 3,
                PetPhysicalDescription = "Black and white cat",
                PetPersonalDescription = "Curious and independent",
                Owner = new PetOwnerModel { OwnerId = "2" },
                IsOwned = true
            },
            new()
            {
                PetId = "3",
                PetName = "Suzy",
                PetSpecie = "Cat",
                PetAge = 4,
                PetPhysicalDescription = "Black cat",
                PetPersonalDescription = "Curious and independent",
                Owner = null,
                IsOwned = false
            }
        };
    }
}
