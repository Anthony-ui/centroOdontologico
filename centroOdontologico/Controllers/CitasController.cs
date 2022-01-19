using centroOdontologico.Filters;
using centroOdontologico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace centroOdontologico.Controllers
{

    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    [vacio(Roles = "secretaria,odontologo,administrador")]
    public class CitasController : Controller
    {
        private readonly centroOdontologicoContext _context;

        public CitasController(centroOdontologicoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> listar()
        {
            try
            {

               
                return Json(await _context.Citas.FromSqlRaw(@"select d.nombres, c.fechaCita, c.idCita,c.activo,c.estado,d.idDoctor,p.idPaciente,c.idSeguro from citas c   join pacientes p on p.idPaciente=c.idPaciente 
                join doctores d on d.idDoctor = c.idDoctor 
                 ").Include(x=>x.IdDoctorNavigation).Include(x=>x.IdPacienteNavigation).AsNoTracking().OrderByDescending(x=>x.IdCita).ToListAsync());
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public async Task<string> guardar(Citas _Citas)
        {
            try
            {
                _Citas.Estado = 0;

                var actual = DateTime.Now;

                var usu = await _context.Citas.AsNoTracking().OrderByDescending(x=>x.IdCita).Where(x => x.IdPaciente == _Citas.IdPaciente).FirstOrDefaultAsync();
                var totalDias = (Convert.ToDateTime(_Citas.FechaCita) - Convert.ToDateTime(actual)).Days;

                if (usu == null || _Citas.IdCita > 0)
                {
                    if (_Citas.FechaCita < actual)
                    {
                        return "menor";
                    }

                    if (_Citas.IdCita == 0)
                    {
                        if (_context.Citas.AsNoTracking().Where(x => x.FechaCita == _Citas.FechaCita).Count() > 0) return "repetido";
                    }


                   

                        if (_Citas.IdCita > 0) _context.Update(_Citas);
                        else _context.Add(_Citas);
                        await _context.SaveChangesAsync();
                        return "ok";
                    

                
                }
                else
                {

                    if (_Citas.FechaCita < actual)
                    {
                        return "menor";
                    }

                

                    var dias= (Convert.ToDateTime(usu.FechaCita) - Convert.ToDateTime(actual)).Days;
                   
                    
                    
                        if ( dias >= 0 && dias<=6)
                    {
                        return "agendada";
                    }
                    else
                    {
                        _context.Add(_Citas);
                        await _context.SaveChangesAsync();
                        return "ok";

                    }

                  
                }

            
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<IActionResult> cargar(Citas _Citas)
        {
            try
            {
                return Json(await _context.Citas.FindAsync(_Citas.IdCita));
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public async Task<string> eliminar(Citas Citas)
        {
            try
            {
                var item = await _context.Citas.FindAsync(Citas.IdCita);
                _context.Remove(item);
                await _context.SaveChangesAsync();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<IActionResult> comboPacientes()
        {
            try
            {
                var item = await _context.Pacientes.ToListAsync();

                return Json(item);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public async Task<IActionResult> comboDoctores()
        {
            try
            {
                var item = await _context.Doctores.ToListAsync();

                return Json(item);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public async Task<IActionResult> comboSeguros()
        {
            try
            {
                var item = await _context.Seguros.ToListAsync();

                return Json(item);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
    }
}