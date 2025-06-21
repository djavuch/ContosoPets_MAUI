using ContosoPets.ViewModels.PetsViewModels;

namespace ContosoPets.View.PetsView;

public partial class PetsPage : ContentPage
{
    public PetsPage(PetsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}