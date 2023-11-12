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
        private PersonaDTO personaDto = new PersonaDTO();

        [ObservableProperty]
        private string tituloPagina;

        private int Id;

        [ObservableProperty]
        private bool loadingEsVisible = false;

        public PersonaViewModel(PersonaDBContext context)
        {
            _dbContext = context;
            //EmpleadoDto.FechaContrato = DateTime.Now;
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
                    PersonaDto.Id = encontrado.Id;
                    PersonaDto.Nombres = encontrado.Nombres;
                    PersonaDto.Apellidos = encontrado.Apellidos;
                    PersonaDto.Correo = encontrado.Correo;
                    PersonaDto.Direccion = encontrado.Direccion;
                    //EmpleadoDto.FechaContrato = encontrado.FechaContrato;

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
                if (Id == 0)
                {
                    var tbPersona = new Persona
                    {
                        Nombres = PersonaDto.Nombres,
                        Apellidos = PersonaDto.Apellidos,
                        Edad = PersonaDto.Edad,
                        Correo = PersonaDto.Correo,
                        Direccion = PersonaDto.Direccion,
                        
                        //FechaContrato = EmpleadoDto.FechaContrato,
                    };

                    _dbContext.Personas.Add(tbPersona);
                    await _dbContext.SaveChangesAsync();

                    PersonaDto.Id = tbPersona.Id;
                    mensaje = new PersonaMensaje()
                    {
                        EsCrear = true,
                        PersonaDto = PersonaDto
                    };

                }
                else
                {
                    var encontrado = await _dbContext.Personas.FirstAsync(e => e.Id == Id);
                    encontrado.Nombres = PersonaDto.Nombres;
                    encontrado.Apellidos = PersonaDto.Apellidos;
                    encontrado.Apellidos = PersonaDto.Apellidos;
                    encontrado.Apellidos = PersonaDto.Apellidos;
                    //encontrado.FechaContrato = EmpleadoDto.FechaContrato;

                    await _dbContext.SaveChangesAsync();

                    mensaje = new PersonaMensaje()
                    {
                        EsCrear = false,
                        PersonaDto = PersonaDto
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

