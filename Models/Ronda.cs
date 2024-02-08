using System;
using System.Collections.Generic;

namespace EntrevistaTecnica.Models;

public partial class Ronda
{
    public int Id { get; set; }

    public int IdJugador1 { get; set; }

    public int IdJugador2 { get; set; }

    public int? Ganador { get; set; }

    public int? MovimientoJugador1 { get; set; }

    public int? MovimientoJugador2 { get; set; }

    public virtual Jugador IdJugador1Navigation { get; set; } = null!;

    public virtual Jugador IdJugador2Navigation { get; set; } = null!;

    public virtual Movimiento? MovimientoJugador1Navigation { get; set; }

    public virtual Movimiento? MovimientoJugador2Navigation { get; set; }
}
