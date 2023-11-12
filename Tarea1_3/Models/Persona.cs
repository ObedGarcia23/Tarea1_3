using System.ComponentModel.DataAnnotations;

namespace Tarea1_3.Models
{
    public class Persona
    {

        [Key]
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Edad { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }


    }
}
