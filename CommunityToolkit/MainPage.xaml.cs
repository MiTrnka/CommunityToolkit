// Soubor: MainPage.xaml.cs
// Popis: Code-behind soubor pro naši hlavní stránku.

namespace CommunityToolkit;

public partial class MainPage : ContentPage
{
    /// <summary>
    /// Konstruktor stránky MainPage.
    /// </summary>
    /// <param name="viewModel">Díky Dependency Injection se nám sem automaticky vloží instance MainViewModelu.</param>
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();

        // Nastavíme BindingContext stránky na náš ViewModel.
        // Tím propojíme UI (XAML) s daty a logikou ve ViewModelu.
        // Všechny {Binding} v XAMLu se nyní budou odkazovat na vlastnosti tohoto viewModelu.
        BindingContext = viewModel;
    }
}