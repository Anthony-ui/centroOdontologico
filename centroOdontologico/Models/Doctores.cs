using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace centroOdontologico.Models
{
    public partial class Doctores
    {
        public Doctores()
        {
            Citas = new HashSet<Citas>();
        }

        public int IdDoctor { get; set; }
        public string? Cedula { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Direccion { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdEspecialidad { get; set; }

        public virtual Ciudades? IdCiudadNavigation { get; set; }
        public virtual Especialidades? IdEspecialidadNavigation { get; set; }

        [JsonIgnore]
        public virtual ICollection<Citas> Citas { get; set; }
    }
}
