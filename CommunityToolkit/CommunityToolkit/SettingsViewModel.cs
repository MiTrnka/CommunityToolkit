// Soubor: SettingsViewModel.cs
// Popis: ViewModel pro stránku s nastavením.

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging; // Důležitý using pro Messenger

namespace CommunityToolkit;

public partial class SettingsViewModel : ObservableObject
{
    // Pole pro uložení instance Messengeru.
    private readonly IMessenger _messenger;

    // Vlastnost pro uživatelské jméno, které budeme na této stránce upravovat.
    [ObservableProperty]
    string username;

    /// <summary>
    /// Konstruktor, do kterého si necháme injektovat IMessenger.
    /// </summary>
    public SettingsViewModel(IMessenger messenger)
    {
        // Uložíme si instanci pošťáka pro pozdější použití.
        _messenger = messenger;
        Username = "Uživatel"; // Předvyplníme nějakou hodnotou
    }

    /// <summary>
    /// Příkaz, který se spustí po stisknutí tlačítka "Uložit".
    /// Jeho úkolem je odeslat zprávu s novým jménem.
    /// </summary>
    [RelayCommand]
    private async Task Save()
    {
        // Vytvoříme novou instanci naší zprávy s aktuální hodnotou z pole Username.
        var message = new UsernameChangedMessage(Username);

        // Pomocí pošťáka (Messengeru) rozešleme zprávu do celé aplikace.
        // Kdokoliv, kdo se přihlásil k odběru zpráv typu UsernameChangedMessage, ji obdrží.
        _messenger.Send(message);

        // Po odeslání zprávy se vrátíme zpět na předchozí stránku.
        await Shell.Current.GoToAsync("..");
    }
}