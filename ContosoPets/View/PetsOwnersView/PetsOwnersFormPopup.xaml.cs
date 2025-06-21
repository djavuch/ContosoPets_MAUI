using CommunityToolkit.Maui.Views;
using ContosoPets.ViewModels.PetsOwnersViewModels;

namespace ContosoPets;

public partial class PetsOwnersFormPopup : Popup
{
    public PetsOwnersFormPopup(PetsOwnersViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}