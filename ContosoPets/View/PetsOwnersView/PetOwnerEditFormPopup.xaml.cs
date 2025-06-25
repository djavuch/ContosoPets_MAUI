using CommunityToolkit.Maui.Views;
using ContosoPets.ViewModels.PetsOwnersViewModels;

namespace ContosoPets;

public partial class PetOwnerEditFormPopup : Popup
{
    public PetOwnerEditFormPopup(PetsOwnersViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}