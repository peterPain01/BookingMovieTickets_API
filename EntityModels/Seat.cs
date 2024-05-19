using System;
using System.Collections.Generic;

namespace MovieTickets.Api.EntityModels;

public partial class Seat
{
    public int SeatId { get; set; }

    public string? Row { get; set; }

    public int? Number { get; set; }

    public int? ShowtimeId { get; set; }

    public string? Status { get; set; }

    public int? Price { get; set; }

    public virtual Showtime? Showtime { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
