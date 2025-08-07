// Soubor: MainViewModel.cs
// Popis: Rozšíříme náš ViewModel o logiku pro příkazy a další vlastnosti.

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input; // Přidáme using pro přístup k [RelayCommand]
using CommunityToolkit.Mvvm.Messaging; // Přidáme using pro přístup k IMessenger

namespace CommunityToolkit;

public partial class MainViewModel : ObservableObject
{
    // Původní vlastnost pro text, který se zobrazuje.
    [ObservableProperty]
    string text;

    // Vlastnost pro ukládání vstupu od uživatele.
    // Generátor opět vytvoří plnohodnotnou vlastnost "Name".
    // Zároveň vygeneruje i "partial" metodu OnNameChanged(), kterou můžeme využít.
    [ObservableProperty]
    string name;

    /// <summary>
    /// Konstruktor je nyní rozšířen o IMessenger.
    /// DI kontejner nám sem automaticky předá instanci služby.
    /// </summary>
    public MainViewModel(IMessenger messenger)
    {
        Text = "Vítej v aplikaci!";
        Name = "Uživatel"; // Můžeme vrátit výchozí hodnotu

        // Zaregistrujeme tento ViewModel k příjmu zpráv typu UsernameChangedMessage.
        // První parametr (WeakReferenceRecipient) zajišťuje, že pokud bude tento ViewModel
        // zničen, registrace se automaticky zruší a předejde se memory leakům.
        // Druhý parametr je akce (lambda funkce), která se má vykonat, když zpráva dorazí.
        messenger.Register<UsernameChangedMessage>(this, (recipient, message) =>
        {
            // Když obdržíme zprávu, aktualizujeme naše vlastnosti.
            // message.Value obsahuje data, která byla se zprávou poslána.
            Name = message.Value;
            Text = $"Vítej zpět, {Name}!";
        });
    }

    /// <summary>
    /// Příkaz pro přechod na stránku nastavení.
    /// </summary>
    [RelayCommand]
    private async Task GoToSettings()
    {
        // Použijeme Shell navigaci pro přechod na stránku, jejíž routu jsme zaregistrovali.
        await Shell.Current.GoToAsync(nameof(SettingsPage));
    }


    /// <summary>
    /// Metoda, která se vykoná po stisknutí tlačítka.
    /// Díky atributu [RelayCommand] z ní generátor automaticky vytvoří
    /// plnohodnotnou implementaci ICommand s názvem "GreetCommand".
    /// Navíc požaduji i CanExecute, tak musím implementovat nějakou bool metodu a její libovolný název předat jako parametr do atributu [RelayCommand].
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanGreet))]
    private void Greet()
    {
        // Zkontrolujeme, zda uživatel zadal nějaké jméno.
        if (string.IsNullOrWhiteSpace(Name))
        {
            Text = "Prosím, zadej své jméno.";
            return;
        }

        // Změníme hodnotu vlastnosti "Text".
        // Protože je vlastnost "Text" typu [ObservableProperty],
        // UI se automaticky a okamžitě aktualizuje.
        Text = $"Ahoj, {Name}!";
    }

    /// <summary>
    /// Tato partial metoda je automaticky vygenerována atributem [ObservableProperty] u vlastnosti "Name".
    /// Je volána vždy, když se hodnota "Name" změní. existuje i OnNameChanging() volitelně volaná před změnou hodnoty
    /// Můžeme ji využít k dodatečné logice - v tomto případě k přehodnocení,
    /// zda má být příkaz "GreetCommand" aktivní.
    /// </summary>
    /// <param name="value">Nová hodnota vlastnosti Name.</param>
    partial void OnNameChanged(string value)
    {
        // Řekneme našemu vygenerovanému příkazu, aby přehodnotil svůj stav "CanExecute".
        // Tím se tlačítko v UI aktivuje nebo deaktivuje podle logiky v metodě CanGreet().
        GreetCommand.NotifyCanExecuteChanged();
    }

    /// <summary>
    /// Tato metoda určuje, zda může být příkaz "Greet" spuštěn.
    /// Název musí být ve formátu "Can" + název příkazu (Greet).
    /// Atribut [RelayCommand] ji automaticky najde a propojí.
    /// </summary>
    /// <returns>Vrací true, pokud je jméno zadané, jinak false.</returns>
    private bool CanGreet()
    {
        // Tlačítko bude aktivní pouze tehdy, pokud textové pole "Name" není prázdné.
        return !string.IsNullOrWhiteSpace(Name);
    }
}