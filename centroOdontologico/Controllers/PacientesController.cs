using centroOdontologico.Filters;
using centroOdontologico.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace centroOdontologico.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    [vacio(Roles = "secretaria,odontologo,administrador")]
    public class PacientesController : Controller
    {

        private readonly centroOdontologicoContext _context;


        public PacientesController(centroOdontologicoContext context)
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

                return Json(await _context.Pacientes.Include(x => x.IdCiudadNavigation).ToListAsync());

            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }




        }




        public async Task<string> guardar(Pacientes _Pacientes)
        {
            try
            {

                if (_Pacientes.IdPaciente == 0)
                {
                    if (_context.Pacientes.AsNoTracking().Where(x => x.Cedula == _Pacientes.Cedula).Count() > 0) return "repetido";
                }
                else
                {
                    var item = await _context.Pacientes.AsNoTracking().Where(x => x.IdPaciente == _Pacientes.IdPaciente).FirstOrDefaultAsync();

                    if (!(item.Cedula == _Pacientes.Cedula))
                    {
                        if ((await _context.Pacientes.AsNoTracking().Where(x => x.Cedula == _Pacientes.Cedula).ToListAsync()).Count() > 0) return "repetido";
                    }


                }


                if (_Pacientes.IdPaciente > 0) _context.Update(_Pacientes);
                else _context.Add(_Pacientes);
                await _context.SaveChangesAsync();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        public async Task<IActionResult> cargar(Pacientes _Pacientes)
        {
            try
            {
                return Json(await _context.Pacientes.FindAsync(_Pacientes.IdPaciente));
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }




        public async Task<string> eliminar(Pacientes Pacientes)
        {
            try
            {
                var item = await _context.Pacientes.FindAsync(Pacientes.IdPaciente);
                _context.Remove(item);
                await _context.SaveChangesAsync();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        public async Task<IActionResult> comboCiudades()
        {
            try
            {
                var item = await _context.Ciudades.ToListAsync();


                return Json(item);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public async Task<IActionResult> comboEspecialidades()
        {
            try
            {
                var item = await _context.Especialidades.ToListAsync();


                return Json(item);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }






    }
}
