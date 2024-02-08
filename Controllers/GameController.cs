using EntrevistaTecnica.Models;
using Microsoft.AspNetCore.Mvc;

namespace EntrevistaTecnica.Controllers
{
    public class GameController : Controller
    {
        private readonly Test1Context _context;
        public GameController(Test1Context context) { _context = context; }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(int player1, int player2)
        {
            try
            {
                var partida = new Partidum() { IdJugador1 = player1, IdJugador2 = player2 };

                _context.Add(partida);
                await _context.SaveChangesAsync();
                return Json(new { description = "La partida ha sido creada exitosamente.", id = partida.Id });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new { description = "Al menos un player no existe" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetWinner(int roundId)
        {
            try
            {
                var ronda = _context.Ronda.FirstOrDefault(r => r.Id == roundId);
                if (ronda == null)
                {
                    Response.StatusCode = 400;
                    return Json(new { description = "La ronda no existe" });
                }
                var winnerChicken = GameRule((int)ronda.MovimientoJugador1, (int)ronda.MovimientoJugador2);

                ronda.Ganador = 0;
                if(winnerChicken == 1)
                {
                    ronda.Ganador = ronda.IdJugador1;
                }
                if(winnerChicken == 2)
                {
                    ronda.Ganador = ronda.IdJugador2;
                }

                await _context.SaveChangesAsync();
                return Json(new { winner = winnerChicken });

            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new { description = "No se pudo definir un ganador" });
            }
        }
        private static int GameRule(int a, int b)
        {
            if (a == b)
            {
                return 0;
            }; 
            if (a == 1 && b == 2)
            {
                return 2;
            };
            if (a == 1 && b == 3)
            {
                return 1;
            };
            if (a == 2 && b == 1) return 1;
            if (a == 2 && b == 3) return 2;
            if (a == 3 && b == 1) return 2;
            if (a == 3 && b == 2) return 1;
            return 0;
        }
    }
}
