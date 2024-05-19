using System;
using System.Collections.Generic;

namespace MovieTickets.Api.EntityModels;

public partial class Genre
{
    public int GenreId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
