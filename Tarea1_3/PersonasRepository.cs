// PersonasRepository.cs
using System.Collections.Generic;
using System.Linq;
using Tarea1_3;
using Tarea1_3.Models;


namespace Tarea1_3
{
    public class PersonasRepository
    {
        private readonly DatabaseService _databaseService;

        public PersonasRepository()
        {
            _databaseService = new DatabaseService();
            _databaseService.Database.EnsureCreated(); // Crea la base de datos si no existe
        }

        public List<Personas> ObtenerTodasLasPersonas()
        {
            return _databaseService.Personas.ToList();
        }

        public Personas ObtenerPersonaPorId(int id)
        {
            return _databaseService.Personas.Find(id);
        }

        public void AgregarPersona(Personas persona)
        {
            _databaseService.Personas.Add(persona);
            _databaseService.SaveChanges();
        }

        public void ActualizarPersona(Personas persona)
        {
            _databaseService.Personas.Update(persona);
            _databaseService.SaveChanges();
        }

        public void EliminarPersona(int id)
        {
            var persona = _databaseService.Personas.Find(id);
            if (persona != null)
            {
                _databaseService.Personas.Remove(persona);
                _databaseService.SaveChanges();
            }
        }
    }
}
