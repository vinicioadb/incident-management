using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace v1.Controllers
{

    //[Authorize(Roles = "Administrador")]
    public class AdministradorController : Controller
    {
        // GET: AdministradorController
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult addticket()
        {
            return View(viewName: "add-ticket");
        }

        public IActionResult lista_usuarios()
        {
            return View("Usuarios/Index");
        }
    }
}
