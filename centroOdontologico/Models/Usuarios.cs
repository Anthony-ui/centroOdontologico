using System;
using System.Collections.Generic;

namespace centroOdontologico.Models
{
    public partial class Usuarios
    {
        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public string? Usuario { get; set; }
        public string? Clave { get; set; }
        public int? IdRol { get; set; }

        public virtual Roles? IdRolNavigation { get; set; }
    }
}
