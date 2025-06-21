using CommunityToolkit.Maui;
using ContosoPets.Services;
using ContosoPets.View.PetsView;
using ContosoPets.View.PetsOwnersView;
using ContosoPets.ViewModels;
using ContosoPets.ViewModels.PetsOwnersViewModels;
using ContosoPets.ViewModels.PetsViewModels;
using Microsoft.Extensions.Logging;
using System.Runtime.Versioning;

namespace ContosoPets
{
    public static class MauiProgram
    {   
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<PetsViewModel>();
            builder.Services.AddSingleton<PetsOwnersViewModel>();

            builder.Services.AddSingleton<PetsPage>();
            builder.Services.AddSingleton<PetsOwnersPage>();

            builder.Services.AddSingleton<PetService>();
            builder.Services.AddSingleton<PetOwnerService>();

            builder.Services.AddTransient<PetDetailsViewModel>();
            builder.Services.AddTransient<PetDetailsPage>();

            builder.Services.AddTransient<PetOwnerDetailsViewModel>();
            builder.Services.AddTransient<PetOwnerDetailsPage>();

            builder.Services.AddTransient<DeleteConfirmationViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
