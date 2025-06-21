using CommunityToolkit.Maui.Views;
using ContosoPets.ViewModels.PetsViewModels;

namespace ContosoPets;

public partial class PetFormPopup : Popup
{
    public PetFormPopup(PetsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
