using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace centroOdontologico.Models
{
    public partial class Especialidades
    {
        public Especialidades()
        {
            Doctores = new HashSet<Doctores>();
        }

        public int IdEspecialidad { get; set; }
        public string? Nombre { get; set; }
        [JsonIgnore]
        public virtual ICollection<Doctores> Doctores { get; set; }
    }
}
