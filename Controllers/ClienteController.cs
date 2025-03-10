using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace v1.Controllers
{
    [Authorize(Roles = "Cliente")]
    public class ClienteController : Controller
    {
        // GET: ClienteController
        public ActionResult Index()
        {
            return View();
        }

    }
}
