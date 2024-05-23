namespace test.Dtos;

public record MovieDto(
    int MovieId,
    string Title,
    string? Genre,
    int? DurationMinutes,
    DateOnly? ReleaseDate,
    decimal? Rating,
    int? Certification,
    string? PlotSummary,
    string? PosterUrl,
    string? PosterVerticalUrl,
    string? TrailerUrl
);
