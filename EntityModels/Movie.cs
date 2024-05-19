using System;
using System.Collections.Generic;

namespace MovieTickets.Api.EntityModels;

public partial class Movie
{
    public int MovieId { get; set; }

    public string Title { get; set; } = null!;

    public int? GenreId { get; set; }

    public int? DurationMinutes { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public decimal? Rating { get; set; }

    public int? Certification { get; set; }

    public string? PlotSummary { get; set; }

    public string? PosterUrl { get; set; }

    public string? PosterVerticalUrl { get; set; }

    public string? TrailerUrl { get; set; }

    public int? DirectorId { get; set; }

    public virtual Director? Director { get; set; }

    public virtual Genre? Genre { get; set; }

    public virtual ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();

    public virtual ICollection<Director> Directors { get; set; } = new List<Director>();

    public virtual ICollection<Star> Stars { get; set; } = new List<Star>();
}
