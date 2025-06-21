using ContosoPets.ViewModels.PetsOwnersViewModels;

namespace ContosoPets.View.PetsOwnersView;

public partial class PetOwnerDetailsPage : ContentPage
{
    public PetOwnerDetailsPage(PetOwnerDetailsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}