using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace centroOdontologico.Models
{
    public partial class Ciudades
    {
        public Ciudades()
        {
            Doctores = new HashSet<Doctores>();
            Pacientes = new HashSet<Pacientes>();
        }

        public int IdCiudad { get; set; }
        public string? Nombre { get; set; }

        [JsonIgnore]
        public virtual ICollection<Doctores> Doctores { get; set; }

        [JsonIgnore]
        public virtual ICollection<Pacientes> Pacientes { get; set; }
    }
}
