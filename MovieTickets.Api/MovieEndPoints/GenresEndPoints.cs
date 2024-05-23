using Microsoft.EntityFrameworkCore;
using MovieTickets.Api.EntityModels;
using MovieTickets.Api.Mapping;

namespace MovieTickets.Api.EndPoints;

public static class GenresEndPoint
{
    public static RouteGroupBuilder MapGenresEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("genres");

        group.MapGet(
            "/",
            async (BookingMovieAppContext dbContext) =>
            {
                var genres = await dbContext
                    .Genres.Select(genre => genre.toDto())
                    .AsNoTracking()
                    .ToListAsync();
                return genres is null ? Results.NotFound() : Results.Ok(genres);
            }
        );
        return group;
    }
}
