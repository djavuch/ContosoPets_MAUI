using ContosoPets.ViewModels.PetsOwnersViewModels;
using System.Diagnostics;

namespace ContosoPets.View.PetsOwnersView;

public partial class PetsOwnersPage : ContentPage
{
    public PetsOwnersPage(PetsOwnersViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        Debug.WriteLine("PetsOwnersPage initialized with BindingContext");
    }
}