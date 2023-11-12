using CommunityToolkit.Mvvm.ComponentModel;

namespace Tarea1_3.DTOs
{
    public partial class PersonaDTO : ObservableObject
    {
        [ObservableProperty]
        public int id;

        [ObservableProperty]
        public string nombres;

        [ObservableProperty]
        public string apellidos;

        [ObservableProperty]
        public int edad;

        [ObservableProperty]
        public string correo;

        [ObservableProperty]
        public string direccion;
    }
}
