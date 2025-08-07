// Soubor: MauiProgram.cs
// Popis: Hlavní konfigurační soubor MAUI aplikace.

using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.Messaging;

namespace CommunityToolkit;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        // Zde registrujeme naše ViewModely a stránky (Views) do Dependency Injection kontejneru.
        // To nám umožní je automaticky "injektovat" (vkládat) do konstruktorů tam, kde je potřebujeme.

        // AddSingleton - vytvoří se pouze jedna instance pro celou aplikaci.
        // AddTransient - vytvoří se nová instance pokaždé, když je služba vyžádána.

        // Registrujeme náš nový MainViewModel. Bude existovat jen jedna jeho instance.
        builder.Services.AddSingleton<MainViewModel>();

        // Registrujeme i naši hlavní stránku. Bude se vytvářet nová instance při každém požadavku.
        builder.Services.AddTransient<MainPage>();



        // Zaregistrujeme nový ViewModel a stránku pro nastavení.
        builder.Services.AddTransient<SettingsViewModel>();
        builder.Services.AddTransient<SettingsPage>();

        // Messenger se registruje automaticky jako singleton, pokud použijeme CommunityToolkit.Mvvm.
        // Není potřeba ho explicitně přidávat, ale pro přehlednost:
        builder.Services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);


        return builder.Build();
    }
}