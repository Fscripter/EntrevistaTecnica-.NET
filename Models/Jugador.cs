using System;
using System.Collections.Generic;

namespace EntrevistaTecnica.Models;

public partial class Jugador
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellidos { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Partidum> PartidumGanadorNavigations { get; set; } = new List<Partidum>();

    public virtual ICollection<Partidum> PartidumIdJugador1Navigations { get; set; } = new List<Partidum>();

    public virtual ICollection<Partidum> PartidumIdJugador2Navigations { get; set; } = new List<Partidum>();

    public virtual ICollection<Rondum> RondumIdJugador1Navigations { get; set; } = new List<Rondum>();

    public virtual ICollection<Rondum> RondumIdJugador2Navigations { get; set; } = new List<Rondum>();
}
