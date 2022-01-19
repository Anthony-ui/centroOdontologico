using centroOdontologico.Filters;
using centroOdontologico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;






namespace centroOdontologico.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    [vacio(Roles = "administrador")]
    public class DoctoresController : Controller
    {
        

        private readonly centroOdontologicoContext _context;


        public DoctoresController(centroOdontologicoContext context)
        {
            _context = context;
        }
         

            

        public IActionResult Index()
        {
            return View();
        }




        public async Task <IActionResult> listar ()
        {

            try
            {

                return Json(await _context.Doctores.Include(x=>x.IdCiudadNavigation).Include(x=>x.IdEspecialidadNavigation).ToListAsync());

            }
            catch (Exception ex)
            {

                return Json (ex.Message);
            }

      


        }




        public async Task<string> guardar(Doctores _doctores)
        {
            try
            {

                if (_doctores.IdDoctor == 0)
                {
                    if (_context.Doctores.AsNoTracking().Where(x => x.Cedula == _doctores.Cedula).Count() > 0) return "repetido";
                }
                else
                {

                    var item = await _context.Doctores.AsNoTracking().Where(x => x.IdDoctor == _doctores.IdDoctor).FirstOrDefaultAsync();

                    if (!(item.Cedula == _doctores.Cedula))
                    {
                        if ((await _context.Doctores.AsNoTracking().Where(x => x.Cedula == _doctores.Cedula).ToListAsync()).Count() > 0) return "repetido";
                    }

                }

              

                if (_doctores.IdDoctor > 0) _context.Update(_doctores);
                else _context.Add(_doctores);
                await _context.SaveChangesAsync();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        public async Task<IActionResult> cargar(Doctores _doctores)
        {
            try
            {
                return Json(await _context.Doctores.FindAsync(_doctores.IdDoctor));
            }
            catch (Exception ex)
            {
                return Json (ex.Message);
            }
        }




        public async Task<string> eliminar(Doctores doctores)
        {
            try
            {
                var item = await _context.Doctores.FindAsync(doctores.IdDoctor);
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
                return Json (ex.Message);
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
