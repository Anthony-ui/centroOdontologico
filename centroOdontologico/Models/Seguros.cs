using System;
using System.Collections.Generic;

namespace centroOdontologico.Models
{
    public partial class Seguros
    {
        public Seguros()
        {
            Citas = new HashSet<Citas>();
        }

        public int IdSeguro { get; set; }
        public string? Institucion { get; set; }
        public string? Tipo { get; set; }

        public virtual ICollection<Citas> Citas { get; set; }
    }
}
