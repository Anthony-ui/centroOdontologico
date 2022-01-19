using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace centroOdontologico.Models
{
    public partial class Pacientes
    {
        public Pacientes()
        {
            Citas = new HashSet<Citas>();
        }

        public int IdPaciente { get; set; }
        public string? Cedula { get; set; }
        public string? Nombres { get; set; }
        public string? Direccion { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? Edad { get; set; }
        public int? IdCiudad { get; set; }

        public virtual Ciudades? IdCiudadNavigation { get; set; }

        [JsonIgnore]
        public virtual ICollection<Citas> Citas { get; set; }
    }
}
