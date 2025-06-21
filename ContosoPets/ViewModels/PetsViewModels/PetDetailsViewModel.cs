using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContosoPets.Models;

namespace ContosoPets.ViewModels.PetsViewModels;

[QueryProperty(nameof(Pet), "SelectedPet")]
public partial class PetDetailsViewModel : ObservableObject
{
    private readonly PetsViewModel _petsViewModel;

    [ObservableProperty]
    private PetsModel _pet;

    [RelayCommand]
    private async Task EditPet()
    {
        if (Pet != null)
            await _petsViewModel.OpenEditPopupCommand.ExecuteAsync(Pet);
    }

    public PetDetailsViewModel(PetsViewModel petsViewModel)
    {
        _petsViewModel = petsViewModel;
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}
