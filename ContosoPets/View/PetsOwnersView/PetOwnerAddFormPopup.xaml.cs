using CommunityToolkit.Maui.Views;
using ContosoPets.ViewModels.PetsOwnersViewModels;

namespace ContosoPets;

public partial class PetOwnerAddFormPopup : Popup
{
    public PetOwnerAddFormPopup(PetsOwnersViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}