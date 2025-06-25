using CommunityToolkit.Maui.Views;
using ContosoPets.ViewModels.PetsViewModels;

namespace ContosoPets;

public partial class PetEditFormPopup : Popup
{
    public PetEditFormPopup(PetsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
