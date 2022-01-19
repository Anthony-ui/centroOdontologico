using centroOdontologico.Filters;
using centroOdontologico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace centroOdontologico.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    [vacio(Roles = "administrador")]

    public class UsuariosController : Controller
    {

        private readonly centroOdontologicoContext _context;


        public UsuariosController(centroOdontologicoContext context)
        {
            _context = context;
        }




        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Perfil()
        {
            return View();
        }






        public async Task<IActionResult> listar()
        {

            try
            {

                return Json(await _context.Usuarios.Include(x => x.IdRolNavigation).ToListAsync());

            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }




        }




        public async Task<string> guardar(Usuarios _Usuarios)
        {
            try
            {

                _Usuarios.FechaRegistro = DateTime.Now;

                if (_Usuarios.IdUsuario == 0)
                {
                    if (_context.Usuarios.AsNoTracking().Where(x => x.Usuario == _Usuarios.Usuario).Count() > 0) return "repetido";
                }
                else
                {
                    var item = await _context.Usuarios.AsNoTracking().Where(x => x.IdUsuario == _Usuarios.IdUsuario).FirstOrDefaultAsync();

                    if (!(item.Usuario == _Usuarios.Usuario))
                    {
                        if ((await _context.Usuarios.AsNoTracking().Where(x => x.Usuario == _Usuarios.Usuario).ToListAsync()).Count() > 0) return "repetido";
                    }


                }


                if (_Usuarios.IdUsuario > 0) _context.Update(_Usuarios);
                else _context.Add(_Usuarios);
                await _context.SaveChangesAsync();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        public async Task<IActionResult> cargar(Usuarios _Usuarios)
        {
            try
            {
                return Json(await _context.Usuarios.FindAsync(_Usuarios.IdUsuario));
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }


        public async Task<IActionResult> cargarPerfil()
        {
            try
            {
                return Json(await _context.Usuarios.FindAsync(HttpContext.Session.GetInt32("idUsuario")));
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }




        public async Task<string> eliminar(Usuarios Usuarios)
        {
            try
            {
                var item = await _context.Usuarios.FindAsync(Usuarios.IdUsuario);
                _context.Remove(item);
                await _context.SaveChangesAsync();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        public async Task<IActionResult> comboRoles()
        {
            try
            {
                var item = await _context.Roles.ToListAsync();


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
