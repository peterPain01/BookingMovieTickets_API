using System;
using System.Collections.Generic;

namespace MovieTickets.Api.EntityModels;

public partial class Cinema
{
    public int CinemaId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();
}
