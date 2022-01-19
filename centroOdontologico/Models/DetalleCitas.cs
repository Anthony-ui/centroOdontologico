using System;
using System.Collections.Generic;

namespace centroOdontologico.Models
{
    public partial class DetalleCitas
    {
        public int IdDetalleCita { get; set; }
        public int? IdCita { get; set; }
        public int? IdProcedimiento { get; set; }
        public decimal? Valor { get; set; }

        public virtual Citas? IdCitaNavigation { get; set; }
        public virtual Procedimientos? IdProcedimientoNavigation { get; set; }
    }
}
