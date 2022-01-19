using System;
using System.Collections.Generic;

namespace centroOdontologico.Models
{
    public partial class Procedimientos
    {
        public Procedimientos()
        {
            DetalleCitas = new HashSet<DetalleCitas>();
        }

        public int IdProcedimiento { get; set; }
        public string? Nombre { get; set; }
        public string? Detalle { get; set; }
        public decimal? Costo { get; set; }

        public virtual ICollection<DetalleCitas> DetalleCitas { get; set; }
    }
}
