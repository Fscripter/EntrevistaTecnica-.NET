using System;
using System.Collections.Generic;

namespace EntrevistaTecnica.Models;

public partial class Movimiento
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Rondum> RondumMovimientoJugador1Navigations { get; set; } = new List<Rondum>();

    public virtual ICollection<Rondum> RondumMovimientoJugador2Navigations { get; set; } = new List<Rondum>();

    public virtual ICollection<Rondum> RondumPartidaNavigations { get; set; } = new List<Rondum>();
}
