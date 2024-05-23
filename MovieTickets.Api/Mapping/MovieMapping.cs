namespace MovieTickets.Api.Mapping;

using MovieTickets.Api.Dtos;
using MovieTickets.Api.EntityModels;

public static class MovieMapping
{
    public static Movie ToEntity(this CreateMovieDto movie)
    {
        return new Movie()
        {
            Title = movie.Title,
            GenreId = movie.GenreId,
            DurationMinutes = movie.DurationMinutes,
            ReleaseDate = movie.ReleaseDate,
            Certification = movie.Certification,
            PlotSummary = movie.PlotSummary,
            PosterUrl = movie.PosterUrl,
            PosterVerticalUrl = movie.PosterVerticalUrl,
            TrailerUrl = movie.TrailerUrl,
        };
    }

    public static MovieDto ToMovieDto(this Movie movie)
    {
        return new(
            movie.MovieId,
            movie.Title,
            movie.Genre!.Name,
            movie.DurationMinutes,
            movie?.ReleaseDate,
            movie?.Rating,  
            movie!.Certification,
            movie.PlotSummary,
            movie.PosterUrl,
            movie.PosterVerticalUrl,
            movie.TrailerUrl
        );
    }
}
