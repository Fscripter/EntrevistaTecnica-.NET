using EntrevistaTecnica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntrevistaTecnica.Controllers
{
    public class RoundController : Controller
    {
        private readonly Test1Context _context;
        public RoundController(Test1Context context) { _context = context; }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Create(int player1, int player2, int matchId)
        {
            if (!(isPlayerRegistered(player1) && isPlayerRegistered(player2)))
            {
                Response.StatusCode = 400;
                return Json(new { description = "Al menos uno de los jugadores no existe" });
            }

            if(!isMatchRegistered(matchId))
            {
                Response.StatusCode = 400;
                return Json(new { description = "La partida no existe" });
            }
            try
            {
                var ronda = new Rondum()
                {
                    IdJugador1 = player1,
                    IdJugador2 = player2,
                    Partida = matchId
                };

                _context.Add(ronda);
                await _context.SaveChangesAsync();


                return Json(new { id = ronda.Id, message = "Ronda creada" }); // Devolver el ID generado como JSON
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new
                {
                    description = "Error al crear ronda "
                });

            }
        }
        private bool isPlayerRegistered(int playerID)
        {
            var player = _context.Jugadors.FirstOrDefault(j => j.Id == playerID);

            return player != null;
        }
        private bool isMatchRegistered(int matchID)
        {
            var match = _context.Partida.FirstOrDefault(j => j.Id == matchID);
            return match != null;
        }

        [HttpPost]
        public async Task<ActionResult> RegisterMovement(int playerID, int movementID, int roundID)
        {
            try
            {
                var error = ValidateMovement(roundID, playerID, movementID);
                if (error != null)
                {
                    return error;
                }
                var ronda = await _context.Ronda.FindAsync(roundID);

                if (ronda.IdJugador1 == playerID)
                {
                    ronda.MovimientoJugador1 = movementID;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    ronda.MovimientoJugador2 = movementID;
                    await _context.SaveChangesAsync();
                }

                return Json(new { description = "La ronda ha sido modificada exitosamente." });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400; // Bad Request
                return Json(new
                {
                    description = "Error al modificar la ronda. Detalles: " + ex.Message,
                    innerException = ex.InnerException != null ? ex.InnerException.Message : "No se encontró una inner exception"
                });
            }
        }
        private ActionResult ValidateMovement(int roundID, int playerID, int movementID)
        {
            if (!isPlayerRegistered(playerID))
            {
                Response.StatusCode = 400;
                return Json(new { description = "El jugador no existe" });
            }
            if (!IsAValidMovement(movementID))
            {
                Response.StatusCode = 400;
                return Json(new { description = "El movimiento no es valido" });
            }

            if (!IsRoundRegistered(roundID))
            {
                Response.StatusCode = 400;
                return Json(new { description = "La ronda no esta registrada" });
            }
            return null;
        }
        private bool IsMovementInRound(int roundID, int playerID)

        {
            var ronda = _context.Ronda.FirstOrDefault(r => r.Id == roundID &&
                                                        (r.IdJugador1 == playerID && r.MovimientoJugador1 != null) ||
                                                        (r.IdJugador2 == playerID && r.MovimientoJugador2 != null));

            return ronda != null;
        }
        private bool IsRoundRegistered(int roundID)
        {
            var ronda = _context.Ronda.FirstOrDefault(j => j.Id == roundID);

            return ronda != null;
        }
        private static bool IsAValidMovement(int movementID)
        {
            if (movementID == 1 || movementID == 2 || movementID == 3)
            {
                return true;
            }
            return false;
        }
    }
}
