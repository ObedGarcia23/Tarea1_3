using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using Tarea1_3.DataAccess;
using Tarea1_3.DTOs;
using Tarea1_3.Utilidades;
using Tarea1_3.Models;
using System.Collections.ObjectModel;
using Tarea1_3.Views;

namespace Tarea1_3.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly PersonaDBContext _dbContext;
        [ObservableProperty]
        private ObservableCollection<PersonaDTO>listaPersona = new ObservableCollection<PersonaDTO>();

        public MainViewModel(PersonaDBContext context)
        {
            _dbContext = context;
            MainThread.BeginInvokeOnMainThread(new Action(async () => await Obtener()));

            WeakReferenceMessenger.Default.Register<PersonaMensajeria>(this, (r, m) =>
            {
                PersonaMensajeRecibido(m.Value);
            });

        }

        public async Task Obtener()
        {
            var lista = await _dbContext.Personas.ToListAsync();

            if (lista.Any())
            {
                foreach(var item in lista)
                {
                    ListaPersona.Add(new PersonaDTO
                    {
                        Id = item.Id,
                        Nombres = item.Nombres,
                        Apellidos = item.Apellidos,
                        Edad = item.Edad,
                        Correo = item.Correo,
                        Direccion = item.Direccion

                    });
                }
            }
        }

        private void PersonaMensajeRecibido(PersonaMensaje personaMensaje)
        {
            var personaDto = personaMensaje.PersonaDto;

            if (personaMensaje.EsCrear)
            {
                ListaPersona.Add(personaDto);
            }
            else
            {
                var encontrado = ListaPersona
                    .First(e => e.Id == personaDto.Id);

                encontrado.Nombres = personaDto.Nombres;
                encontrado.Apellidos = personaDto.Apellidos;
                encontrado.Edad = personaDto.Edad;
                encontrado.Correo = personaDto.Correo;
                encontrado.Direccion = personaDto.Direccion;

            }
        }

        [RelayCommand]
        private async Task Crear ()
        {
            var uri = $"{nameof(PersonaPage)}?id=0";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Editar(PersonaDTO personaDto)
        {
            var uri = $"{nameof(PersonaPage)}?id={personaDto.Id}";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Eliminar(PersonaDTO personaDto)
        {
            bool answer = await Shell.Current.DisplayAlert("Mensaje", "Desea Eliminar el Empleado?", "SI", "NO");

            if (answer)
            {
                var encontrado = await _dbContext.Personas
                    .FirstAsync(e => e.Id == personaDto.Id);
                _dbContext.Personas.Remove(encontrado);
                await _dbContext.SaveChangesAsync();
                ListaPersona.Remove(personaDto);

            }
        }

    }
}
