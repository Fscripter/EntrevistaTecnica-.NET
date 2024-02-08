using Microsoft.AspNetCore.Mvc;

namespace EntrevistaTecnica.Controllers
{
    public class Lobby : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
