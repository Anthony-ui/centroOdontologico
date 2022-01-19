using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace centroOdontologico.Models
{
    public partial class Roles
    {
        public Roles()
        {
            Usuarios = new HashSet<Usuarios>();
        }

        public int IdRol { get; set; }
        public string? Nombre { get; set; }

        [JsonIgnore]
        public virtual ICollection<Usuarios> Usuarios { get; set; }
    }
}
