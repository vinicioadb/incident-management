using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace v1.Controllers
{
    [Authorize(Roles = "Tecnico")]
    public class TecnicoController : Controller
    {
        // GET: TecnicoController
        public ActionResult Index()
        {
            return View();
        }

    }
}
