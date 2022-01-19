using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace centroOdontologico.Filters
{
    public class vacio : Attribute,IAuthorizationFilter
    {

        public string? Roles { get; set; }
        private AuthorizationFilterContext? _context;



        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _context = context;






            if (!verificarSesion())
            {

                context.Result = new RedirectResult("~/Home");

              
                //context.Result = new UnauthorizedResult();
                return;
            }



            if (!verificarRol())
            {
                context.Result = new RedirectResult("~/Calendario/Error");
                //context.Result = new UnauthorizedResult();
                return;
            }


         

         

        }
        private bool verificarSesion()
        {
            return !string.IsNullOrEmpty(_context?.HttpContext.Session.GetString("usuario"));
        }
        private bool verificarRol()
        {
            if (string.IsNullOrEmpty(Roles)) return true;
            var sesion_roles = _context?.HttpContext.Session.GetString("rol");
            if (string.IsNullOrEmpty(sesion_roles)) return false;
           var lista_roles_sesion = sesion_roles.Split(',');
            var lista_roles_requerido = Roles.Split(',');
            if (lista_roles_requerido.Count() == 0) return true;
            foreach (var item in lista_roles_sesion)
            {
                if (lista_roles_requerido.Contains(item)) return true;
            }
            return false;
        }


    }
}