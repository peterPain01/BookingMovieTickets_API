namespace unit_test.Dtos;

public record MovieDto(
    int MovieId,
    string Title,
    string Genre,
    int? DurationMinutes,
    DateOnly? ReleaseDate,
    int? Certification,
    int Rating,
    string? PlotSummary,
    string? PosterUrl,
    string? PosterVerticalUrl,
    string? TrailerUrl
);
