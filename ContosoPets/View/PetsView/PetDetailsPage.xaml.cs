using ContosoPets.ViewModels.PetsViewModels;

namespace ContosoPets.View.PetsView;

public partial class PetDetailsPage : ContentPage
{
    public PetDetailsPage(PetDetailsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}