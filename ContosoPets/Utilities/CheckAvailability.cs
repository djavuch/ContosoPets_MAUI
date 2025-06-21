using ContosoPets.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Vpn;

namespace ContosoPets.Utilities;

public class CheckAvailability
{
    public static bool IsPetAvailable(ObservableCollection<PetsModel> petsCollection, string petId)
    {
        // Check if the pet name is already in the list of existing pet names
        var pet = petsCollection.FirstOrDefault(p => p.PetId == petId);
        return pet != null && !pet.IsOwned;
    }
}
