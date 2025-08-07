// Soubor: UsernameChangedMessage.cs
// Popis: Třída reprezentující zprávu, která se posílá, když se změní uživatelské jméno.
// Dědíme z ValueChangedMessage<T>, což je užitečná třída z CommunityToolkit,
// která standardizuje zprávy o změně hodnoty.

using CommunityToolkit.Mvvm.Messaging.Messages;

namespace CommunityToolkit;

/// <summary>
/// Tato zpráva je odeslána pomocí IMessenger, když uživatel uloží nové jméno na stránce nastavení.
/// Obsahuje novou hodnotu jména.
/// </summary>
/// <param name="value">Nové uživatelské jméno.</param>
public class UsernameChangedMessage(string value) : ValueChangedMessage<string>(value)
{
}