namespace centroOdontologico.Models
{
    public class CitasIntermediaria
    {
        public int? idCita { get; set; }
        public string? dia { get; set; }
        public string? mes { get; set; }
        public string? anio { get; set; }
        public int? idPaciente { get; set; }
        public string? nombresPaciente { get; set; }
        public int? idDoctor { get; set; }
        public string? nombresDoctor { get; set; }
        public int? idSeguro { get; set; }
        public string? nombresSeguros { get; set; }
        public string? hora { get; set; }
        public string? nombresEspecialidad { get; set; }
        public int? estado { get; set; }


    }
}
