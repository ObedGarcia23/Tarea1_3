using Tarea1_3.ViewModels;


namespace Tarea1_3.Views;
public partial class PersonaPage : ContentPage
{
    public PersonaPage(PersonaViewModel viewModel)
    {
		InitializeComponent();
        BindingContext = viewModel;
    }
}