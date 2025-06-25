using CommunityToolkit.Maui.Views;
using ContosoPets.ViewModels.PetsViewModels;

namespace ContosoPets;

public partial class PetAddFormPopup : Popup
{
    public PetAddFormPopup(PetsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
