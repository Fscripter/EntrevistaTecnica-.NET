using EntrevistaTecnica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntrevistaTecnica.Controllers
{
    public class PlayerController : Controller
    {
        private readonly Test1Context _context;
        public PlayerController(Test1Context context) { _context = context; }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(string name, string last_name)
        {

            if (!validateUser(name, last_name))
            {
                Response.StatusCode = 400;
                return Json(new { description = "No se aceptan campos nulos", errorCode = 1 });
            }
            try
            {

                var player = new Jugador() { Nombre = name, Apellidos = last_name };
                _context.Add(player);
                await _context.SaveChangesAsync();

                // player.Id contiene el ID generado por la base de datos después de guardar el jugador
                Response.StatusCode = 200;
                return Json(new { id = player.Id, message = "Usuario creado" }); // Devolver el ID generado como JSON
            }
            catch (Exception ex)
            {
                // En caso de error, devolver un mensaje de error
                Response.StatusCode = 400;
                return Json(new { description = "Error al guardar los datos en la base de datos. Detalles: " + ex.Message });
            }
        }
        private bool validateUser(string name, string last_name)
        {
            if (name == null || last_name == null)
            {
                return false;
            }
            return true;
        }
    }
}
