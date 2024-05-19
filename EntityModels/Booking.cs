using System;
using System.Collections.Generic;

namespace MovieTickets.Api.EntityModels;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? UserId { get; set; }

    public int? ShowtimeId { get; set; }

    public DateTime? BookingDatetime { get; set; }

    public int? OriginalPrice { get; set; }

    public int? TotalPrice { get; set; }

    public virtual Showtime? Showtime { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();
}
