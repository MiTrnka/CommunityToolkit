// Soubor: UsernameChangedMessage.cs
// Popis: Třída reprezentující zprávu, která se posílá, když se změní uživatelské jméno.

using CommunityToolkit.Mvvm.Messaging.Messages;

namespace CommunityToolkit;

/// <summary>
/// Tato zpráva je odeslána pomocí IMessenger, když uživatel uloží nové jméno na stránce nastavení.
/// Obsahuje novou hodnotu jména.
/// </summary>
/// <param name="value">Nové uživatelské jméno.</param>
public class UsernameChangedMessage
{
    public string NewUsername { get; }

    /// <summary>
    /// Konstruktor, který přijme a uloží nové jméno.
    /// </summary>
    public UsernameChangedMessage(string newUsername)
    {
        NewUsername = newUsername;
    }
}