using CommunityToolkit.Maui.Views;
using ContosoPets.ViewModels;

namespace ContosoPets;

public partial class DeleteConfirmationPopup : Popup
{
    public DeleteConfirmationPopup()
    {
        InitializeComponent();
        BindingContext = new DeleteConfirmationViewModel(this);
    }
}