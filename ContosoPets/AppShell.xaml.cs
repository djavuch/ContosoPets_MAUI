using ContosoPets.View.PetsView;
using ContosoPets.View.PetsOwnersView;

namespace ContosoPets
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(PetDetailsPage), typeof(PetDetailsPage));
            Routing.RegisterRoute(nameof(PetOwnerDetailsPage), typeof(PetOwnerDetailsPage));
        }
    }
}
