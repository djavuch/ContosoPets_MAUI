using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoPets.ViewModels.PetsViewModels;

public partial class PetsViewModel
{
    private string? _petPageSearchText;
    public string? PetPageSearchText
    {
        get => _petPageSearchText;
        set
        {
            SetProperty(ref _petPageSearchText, value);
            FilterPagePets();
        }
    }

    private void FilterPagePets()
    {
        Pets.Clear();
        if (!_petsService.Pets.Any())
        {
            return;
        }

        var filteredPets = string.IsNullOrEmpty(PetPageSearchText)
            ? _petsService.Pets.ToList()
            : _petsService.Pets
                .Where(p =>
                    (p.PetName?.Contains(PetPageSearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (p.Owner?.OwnerName?.Contains(PetPageSearchText, StringComparison.OrdinalIgnoreCase) ?? false))
                .ToList();

        foreach (var pet in filteredPets)
        {
            Pets.Add(pet);
        }

        UpdatePetIndexes();
    }
}
