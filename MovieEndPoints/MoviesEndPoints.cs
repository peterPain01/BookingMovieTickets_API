using Microsoft.EntityFrameworkCore;
using MovieTickets.Api.EntityModels;

namespace MovieTickets.Api.EndPoints;

public static class MoviesEndPoints
{
    public static RouteGroupBuilder MapMoviesEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("movies");

        group.MapGet(
            "/",
            async (BookingMovieTicketsContext dbContext) =>
            {
                var movies = await dbContext.Movies.Select(movie => movie).ToListAsync();
                if (movies is null)
                    return Results.NotFound();
                return Results.Ok(movies);
            }
        );

        group.MapGet(
            "/now-showing",
            async (BookingMovieTicketsContext dbContext) =>
            {
                var currentTime = DateTime.Now;
                var tempQuery = await dbContext
                    .Showtimes.Where(st => st.ShowtimeDatetime > currentTime)
                    .Select(st => st.MovieId)
                    .Distinct()
                    .ToListAsync();
                var movies = await dbContext
                    .Movies.Where(mv => tempQuery.Contains(mv.MovieId))
                    .ToListAsync();

                if (movies is null)
                    return Results.NotFound();
                return Results.Ok(movies);
            }
        );

        group.MapGet(
            "/trending",
            async (BookingMovieTicketsContext dbContext) =>
            {
                var trendingMovies = await (
                    from bk in dbContext.Bookings
                    join st in dbContext.Showtimes on bk.ShowtimeId equals st.ShowtimeId
                    where st.ShowtimeDatetime > DateTime.Now
                    group st by st.MovieId into g
                    orderby g.Count() descending
                    select g.Key
                )
                    .Take(5)
                    .ToListAsync();

                var movies = await dbContext
                    .Movies.Where(mv => trendingMovies.Contains(mv.MovieId))
                    .ToListAsync();

                if (movies is null)
                    return Results.NotFound();
                return Results.Ok(movies);
            }
        );

        group.MapGet(
            "/{id}",
            async (int id, BookingMovieTicketsContext dbContext) =>
            {
                var movie = await dbContext.Movies.FindAsync(id);
                Console.WriteLine(movie);
                return movie is null ? Results.NotFound() : Results.Ok(movie);
            }
        );

        group.MapDelete(
            "/{id}",
            async (int id, BookingMovieTicketsContext dbContext) =>
            {
                await dbContext.Movies.Where(mv => mv.MovieId == id).ExecuteDeleteAsync();
                return Results.NoContent();
            }
        );

        // need to be upgrade
        group.MapGet(
            "/total/now-showing",
            async (BookingMovieTicketsContext dbContext) =>
            {
                var num_totals = await dbContext
                    .Showtimes.Where(st => st.ShowtimeDatetime > DateTime.Now)
                    .Select(st => st.MovieId)
                    .Distinct()
                    .CountAsync();
                return num_totals > 0 ? Results.Ok(num_totals) : Results.NotFound();
            }
        );

        return group;
    }
}
