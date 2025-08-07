namespace CommunityToolkit;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Zaregistrujeme "cestu" (route) pro naši novou stránku.
        // Díky tomu můžeme v celé aplikaci používat navigaci pomocí Shell.Current.GoToAsync("SettingsPage").
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
    }
}
