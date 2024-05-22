namespace unit_test.Dtos;

public record CreateMovieDto(
    string Title,
    int GenreId,
    int? DurationMinutes,
    DateOnly? ReleaseDate,
    int? Certification,
    string? PlotSummary,
    string? PosterUrl,
    string? PosterVerticalUrl,
    string? TrailerUrl
);
