using System;
using System.Collections.Generic;

namespace MovieTickets.Api.EntityModels;

public partial class Showtime
{
    public int ShowtimeId { get; set; }

    public int? MovieId { get; set; }

    public int? CinemaId { get; set; }

    public DateTime? ShowtimeDatetime { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Cinema? Cinema { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();
}
