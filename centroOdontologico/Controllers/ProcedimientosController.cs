using centroOdontologico.Filters;
using centroOdontologico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace centroOdontologico.Controllers
{

    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    [vacio(Roles = "administrador")]



    public class ProcedimientosController : Controller
    {
    


        private readonly centroOdontologicoContext _context;


        public ProcedimientosController(centroOdontologicoContext context)
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

                return Json(await _context.Procedimientos.ToListAsync());

            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }




        }




        public async Task<string> guardar(Procedimientos _Procedimientos)
        {
            try
            {

                if (_Procedimientos.IdProcedimiento == 0)
                {
                    if (_context.Procedimientos.AsNoTracking().Where(x => x.Nombre == _Procedimientos.Nombre).Count() > 0) return "repetido";
                }
                else
                {

                      var item = await _context.Procedimientos.AsNoTracking().Where(x => x.IdProcedimiento == _Procedimientos.IdProcedimiento).FirstOrDefaultAsync();

                if (!(item.Nombre == _Procedimientos.Nombre))
                {
                    if ((await _context.Procedimientos.AsNoTracking().Where(x => x.Nombre == _Procedimientos.Nombre).ToListAsync()).Count() > 0) return "repetido";
                }

                }

              

                if (_Procedimientos.IdProcedimiento > 0) _context.Update(_Procedimientos);
                else _context.Add(_Procedimientos);
                await _context.SaveChangesAsync();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        public async Task<IActionResult> cargar(Procedimientos _Procedimientos)
        {
            try
            {
                return Json(await _context.Procedimientos.FindAsync(_Procedimientos.IdProcedimiento));
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }




        public async Task<string> eliminar(Procedimientos Procedimientos)
        {
            try
            {
                var item = await _context.Procedimientos.FindAsync(Procedimientos.IdProcedimiento);
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
