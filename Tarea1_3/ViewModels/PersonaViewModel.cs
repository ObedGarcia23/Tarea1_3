using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using Tarea1_3.DataAccess;
using Tarea1_3.DTOs;
using Tarea1_3.Utilidades;
using Tarea1_3.Models;

namespace Tarea1_3.ViewModels
{
    public partial class PersonaViewModel : ObservableObject, IQueryAttributable
    {

        private readonly PersonaDBContext _dbContext;

        [ObservableProperty]
        private PersonaDTO personaDTO = new PersonaDTO();

        [ObservableProperty]
        private string tituloPagina;

        private int Id;

        [ObservableProperty]
        private bool loadingEsVisible = false;

        public PersonaViewModel(PersonaDBContext context)
        {
            _dbContext = context;
            
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var id = int.Parse(query["id"].ToString());
            Id = id;

            if (Id == 0)
            {
                TituloPagina = "Nuevo Registro";
            }
            else
            {
                TituloPagina = "Editar Registro";
                LoadingEsVisible = true;
                await Task.Run(async () =>
                {
                    var encontrado = await _dbContext.Personas.FirstAsync(e => e.Id == Id);
                    PersonaDTO.Id = encontrado.Id;
                    PersonaDTO.Nombres = encontrado.Nombres;
                    PersonaDTO.Apellidos = encontrado.Apellidos;
                    PersonaDTO.Edad = encontrado.Edad;
                    PersonaDTO.Correo = encontrado.Correo;
                    PersonaDTO.Direccion = encontrado.Direccion;

                    MainThread.BeginInvokeOnMainThread(() => { LoadingEsVisible = false; });


                });
            }
        }

        [RelayCommand]
        private async Task Guardar()
        {
            LoadingEsVisible = true;
            PersonaMensaje mensaje = new PersonaMensaje();
            await Task.Run(async () =>
            {
                if (Id==0)
                {
                    var tbPersona = new Persona
                    {
                        Nombres = PersonaDTO.Nombres,
                        Apellidos = PersonaDTO.Apellidos,
                        Edad = PersonaDTO.Edad,
                        Correo = PersonaDTO.Correo,
                        Direccion = PersonaDTO.Direccion,

                    };

                    _dbContext.Personas.Add(tbPersona);
                    await _dbContext.SaveChangesAsync();
                    PersonaDTO.Id = tbPersona.Id;
                    mensaje = new PersonaMensaje()
                    {
                        EsCrear = true,
                        PersonaDto = PersonaDTO
                    };
                }
                else
                {
                    var encontrado = await _dbContext.Personas.FirstAsync(e => e.Id ==  Id);
                    encontrado.Nombres = PersonaDTO.Nombres;
                    encontrado.Apellidos = PersonaDTO.Apellidos;
                    encontrado.Edad = PersonaDTO.Edad;
                    encontrado.Correo = PersonaDTO.Correo;
                    encontrado.Direccion = PersonaDTO.Direccion;

                    await _dbContext.SaveChangesAsync();

                    mensaje = new PersonaMensaje()
                    {
                        EsCrear = false,
                        PersonaDto = PersonaDTO
                    };

                }
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    LoadingEsVisible = false;
                    WeakReferenceMessenger.Default.Send(new PersonaMensajeria(mensaje));
                    await Shell.Current.Navigation.PopAsync();  
                });
            });
        }

    }
}
