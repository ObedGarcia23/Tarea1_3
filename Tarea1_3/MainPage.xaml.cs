using Tarea1_3.ViewModels;


namespace Tarea1_3
{
    public partial class MainPage : ContentPage
    {
        //private PersonasRepository _personasRepository;
        //private ListView listView; // Agrega la definición del ListView

        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }


    }
}
