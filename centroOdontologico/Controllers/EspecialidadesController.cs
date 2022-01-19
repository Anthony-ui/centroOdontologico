using centroOdontologico.Filters;
using centroOdontologico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace centroOdontologico.Controllers
{

    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    [vacio(Roles = "administrador")]



    public class EspecialidadesController : Controller
    {

        private readonly centroOdontologicoContext _context;


        public EspecialidadesController(centroOdontologicoContext context)
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

                return Json(await _context.Especialidades.ToListAsync());

            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }




        }




        public async Task<string> guardar(Especialidades _Especialidades)
        {
            try
            {

                if (_Especialidades.IdEspecialidad == 0)
                {
                    if (_context.Especialidades.AsNoTracking().Where(x => x.Nombre == _Especialidades.Nombre).Count() > 0) return "repetido";
                }
                else
                {

                    var item = await _context.Especialidades.AsNoTracking().Where(x => x.IdEspecialidad == _Especialidades.IdEspecialidad).FirstOrDefaultAsync();

                    if (!(item.Nombre == _Especialidades.Nombre))
                    {
                        if ((await _context.Especialidades.AsNoTracking().Where(x => x.Nombre == _Especialidades.Nombre).ToListAsync()).Count() > 0) return "repetido";
                    }

                }



                if (_Especialidades.IdEspecialidad > 0) _context.Update(_Especialidades);
                else _context.Add(_Especialidades);
                await _context.SaveChangesAsync();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        public async Task<IActionResult> cargar(Especialidades _Especialidades)
        {
            try
            {
                return Json(await _context.Especialidades.FindAsync(_Especialidades.IdEspecialidad));
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }




        public async Task<string> eliminar(Especialidades Especialidades)
        {
            try
            {
                var item = await _context.Especialidades.FindAsync(Especialidades.IdEspecialidad);
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
