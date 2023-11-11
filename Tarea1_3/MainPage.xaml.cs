using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace Tarea1_3
{
    public partial class MainPage : ContentPage
    {
        private PersonasRepository _personasRepository;
        private ListView listView; // Agrega la definición del ListView

        public MainPage()
        {
            InitializeComponent();
            _personasRepository = new PersonasRepository();
            CargarDatos();
        }

        private void CargarDatos()
        {
            // Obtener y mostrar la lista de personas en la interfaz de usuario
            var personas = _personasRepository.ObtenerTodasLasPersonas();
            listView.ItemsSource = personas;
        }

        private void Agregar_Clicked(object sender, EventArgs e)
        {
            // Implementar lógica para agregar una nueva persona
            // Actualizar la interfaz de usuario después de agregar
        }

        private void Actualizar_Clicked(object sender, EventArgs e)
        {
            // Implementar lógica para actualizar una persona
            // Actualizar la interfaz de usuario después de la actualización
        }

        private void Eliminar_Clicked(object sender, EventArgs e)
        {
            // Implementar lógica para eliminar una persona
            // Actualizar la interfaz de usuario después de la eliminación
        }
    }
}
