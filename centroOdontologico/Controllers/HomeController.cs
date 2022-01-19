using centroOdontologico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace centroOdontologico.Controllers
{
    public class HomeController : Controller
    {


        private readonly ILogger<HomeController> _logger;
        private readonly centroOdontologicoContext _context;


        public HomeController(ILogger<HomeController> logger, centroOdontologicoContext context)
        {
            _logger = logger;
            _context = context;
        }



        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
         {

           if (HttpContext.Session.GetString("usuario")!=null)
            {

               return RedirectToAction("Index", "Doctores");

            }
           
                return View();


            


        }


        public IActionResult redireccion()
        {

            if (HttpContext.Session.GetString("rol").ToLower() == "administrador".ToLower())
            {

                return RedirectToAction("Index", "Doctores");

            }
            else
            {
                return RedirectToAction("Index", "Pacientes");

            }

            return View();


        }




        public IActionResult Salir()
        {

            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    

        public string login(Usuarios _usuario)
        {
            try
            {


             
                var usuario = _context.Usuarios.Include(x => x.IdRolNavigation).Where(x => x.Usuario == _usuario.Usuario && x.Clave == _usuario.Clave).FirstOrDefault();


                if(usuario==null)
                {
                    return "error";
                }
                else
                 {
               

                    HttpContext.Session.SetString("usuario",usuario.Nombre.ToString());
                    HttpContext.Session.SetInt32("idUsuario", usuario.IdUsuario);
                    HttpContext.Session.SetString("rol", usuario.IdRolNavigation.Nombre);
                    return "ok";

                }

            }
            catch (Exception e)
            {

              return  e.Message;
            }
        }


    }
}