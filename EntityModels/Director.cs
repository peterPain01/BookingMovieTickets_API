using System;
using System.Collections.Generic;

namespace MovieTickets.Api.EntityModels;

public partial class Director
{
    public int DirectorId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();

    public virtual ICollection<Movie> MoviesNavigation { get; set; } = new List<Movie>();
}
