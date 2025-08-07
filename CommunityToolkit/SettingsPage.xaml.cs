// Soubor: SettingsPage.xaml.cs
namespace CommunityToolkit;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(SettingsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}