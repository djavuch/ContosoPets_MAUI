using ContosoPets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoPets.ViewModels.PetsOwnersViewModels;

public partial class PetsOwnersViewModel
{
    private string? _petOwnerSearchText;
    public string? PetOwnerSearchText
    {
        get => _petOwnerSearchText;
        set
        {
            SetProperty(ref _petOwnerSearchText, value);
            FilterPetOwners();
        }
    }

    private void FilterPetOwners()
    {
        PetsOwners.Clear();

        if (!_petOwnerService.PetsOwners.Any())
        {
            return;
        }

        var filteredOwners = string.IsNullOrEmpty(PetOwnerSearchText)
            ? _petOwnerService.PetsOwners.ToList()
            : _petOwnerService.PetsOwners
                .Where(po =>
                    (po.OwnerName?.Contains(PetOwnerSearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (po.OwnerPhone?.Contains(PetOwnerSearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (po.OwnerEmail?.Contains(PetOwnerSearchText, StringComparison.OrdinalIgnoreCase) ?? false))
                .ToList();

        foreach (var petOwner in filteredOwners)
        {
            PetsOwners.Add(petOwner);
        }

        UpdateOwnerIndexes();
    }
}
