using centroOdontologico.Filters;
using centroOdontologico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace centroOdontologico.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    [vacio(Roles = "secretaria,odontologo,administrador")]

    public class CalendarioController : Controller
    {



        private readonly centroOdontologicoContext _context;

        public CalendarioController(centroOdontologicoContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }



        public IActionResult Error()
        {
            return View();
        }

        public async Task <object> citas  ()

         {




            var listaReferencia = (from c in _context.Citas
                                   join p in _context.Pacientes on c.IdPaciente equals p.IdPaciente
                                   join d in _context.Doctores on c.IdDoctor equals d.IdDoctor
                                   join e in _context.Especialidades on  d.IdEspecialidad equals e.IdEspecialidad
                                   join s in _context.Seguros  on c.IdSeguro equals s.IdSeguro
                   
                                   select new CitasIntermediaria
                                   {
                                       idCita=c.IdCita,

                                       dia = Convert.ToDateTime(c.FechaCita).Day.ToString(),

                                       mes = Convert.ToDateTime(c.FechaCita).Month.ToString(),

                                       anio = Convert.ToDateTime(c.FechaCita).Year.ToString(),

                                       hora = Convert.ToDateTime(c.FechaCita).Hour.ToString() +":"+ Convert.ToDateTime(c.FechaCita).Minute.ToString(),

                                       idPaciente =p.IdPaciente,

                                       nombresPaciente=p.Nombres,

                                       idDoctor=d.IdDoctor,

                                       nombresDoctor=d.Nombres,

                                       idSeguro=s.IdSeguro,

                                       nombresSeguros=s.Institucion +"-"+s.Tipo,

                                       nombresEspecialidad=e.Nombre,

                                       estado= c.Estado,


                                   }).ToList();


            return  Json(listaReferencia);




        }


        public async Task<string> guardar  ([FromBody] IEnumerable<Detalle> detalle)
        {


            

            try
            {

                foreach (var item in detalle)
                {

                   
                    DetalleCitas detalleCitas = new DetalleCitas();
                    Citas citas = new Citas();
                    var valor = item.Valor.ToString().Replace(".", ",");
                    detalleCitas.IdCita = Convert.ToInt32(item.IdCita);
                    var consulta = _context.Citas.Where(x => x.IdCita ==Convert.ToInt32 (item.IdCita)).FirstOrDefault();
                    detalleCitas.IdProcedimiento = Convert.ToInt32(item.IdProcedimiento);
                    detalleCitas.Valor =  Convert.ToDecimal(valor);
                    _context.DetalleCitas.Add(detalleCitas);
                    consulta.Estado = 1;
                }
                _context.SaveChangesAsync();
                return "ok";


            }
            catch (Exception)
            {

                throw;
            }


          

    



        
        }





        public async Task<string> cancelar(int id)
            {

            try
            {

                var consulta = _context.Citas.Where(x => x.IdCita == id).FirstOrDefault();
                consulta.Estado = 2;
                _context.SaveChanges();
                return "ok";

            }
            catch (Exception ex)
            {

               return ex.Message;
            }

          





        }

        public async Task<IActionResult> listar()
        {

            try
            {

                return Json(await _context.Procedimientos.ToListAsync());

            }
            catch (Exception ex)
            {

                return Json(ex.Message);    
            }




        }



    }
}
