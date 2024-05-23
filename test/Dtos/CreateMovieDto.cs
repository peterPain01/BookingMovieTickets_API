using System.ComponentModel.DataAnnotations;

namespace test.Dtos;

public record class CreateMovieDto(
    [Required] string Title, 
    [Required] int GenreId,
    [Required] int DurationMinutes,
    [Required] int Certification,
    [Required] string PlotSummary,
    [Required] string PosterUrl,
    [Required] string PosterVerticalUrl,
    [Required] string TrailerUrl,
    DateOnly ReleaseDate
);
