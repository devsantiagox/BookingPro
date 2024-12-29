using System.Web.Mvc;
using BookingPro.Data;

namespace BookingPro.Controllers
{
    /**
     * Controlador de lógica para manejar las solicitudes relacionadas con la Página Principal.
     * Autor: Santiago Ruiz - santiago.develops@gmail.com
     * Copyright: Psycotick Software S.L.
     * Licencia: MIT
     */
    public class HomeController : Controller
    {
        private readonly DbContext dbContext;

        /**
         * Constructor por defecto.
         * Inicializa una nueva instancia del contexto de base de datos.
         */
        public HomeController()
        {
            dbContext = new DbContext();
        }

        /**
         * Acción para manejar la solicitud GET de la página principal.
         * @return La vista correspondiente a la página principal.
         */
        public ActionResult Index()
        {
            return View();
        }
    }
}
