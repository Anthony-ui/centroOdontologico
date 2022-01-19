using System;
using System.Collections.Generic;

namespace centroOdontologico.Models
{
    public partial class Citas
    {
        public Citas()
        {
            DetalleCitas = new HashSet<DetalleCitas>();
        }

        public int IdCita { get; set; }
        public DateTime? FechaCita { get; set; }
        public bool? Activo { get; set; }
        public int? Estado { get; set; }
        public int? IdPaciente { get; set; }
        public int? IdDoctor { get; set; }
        public int? IdSeguro { get; set; }

        public virtual Doctores? IdDoctorNavigation { get; set; }
        public virtual Pacientes? IdPacienteNavigation { get; set; }
        public virtual Seguros? IdSeguroNavigation { get; set; }
        public virtual ICollection<DetalleCitas> DetalleCitas { get; set; }
    }
}
