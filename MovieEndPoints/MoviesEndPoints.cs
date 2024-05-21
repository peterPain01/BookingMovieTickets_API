using Microsoft.EntityFrameworkCore;
using MovieTickets.Api.Dtos;
using MovieTickets.Api.EntityModels;
using MovieTickets.Api.Mapping;

namespace MovieTickets.Api.EndPoints;

public static class MoviesEndPoints
{
    public static RouteGroupBuilder MapMoviesEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("movies");
        var GetMovieEndPointName = "GetName";

        group.MapGet(
            "/",
            async (BookingMovieTicketsContext dbContext) =>
            {
                var movies = await dbContext
                    .Movies.Include(movie => movie.Genre)
                    .Select(movie => movie.ToMovieDto())
                    .AsNoTracking()
                    .ToListAsync();
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
            async (BookingMovieTicketsContext dbContext, int limit = 5) =>
            {
                var trendingMovies = await (
                    from bk in dbContext.Bookings
                    join st in dbContext.Showtimes on bk.ShowtimeId equals st.ShowtimeId
                    where st.ShowtimeDatetime > DateTime.Now
                    group st by st.MovieId into g
                    orderby g.Count() descending
                    select g.Key
                )
                    .Take(limit)
                    .ToListAsync();

                var movies = await dbContext
                    .Movies.Where(mv => trendingMovies.Contains(mv.MovieId))
                    .ToListAsync();

                if (movies is null)
                    return Results.NotFound();
                return Results.Ok(movies);
            }
        );

        group
            .MapGet(
                "/{id}",
                async (int id, BookingMovieTicketsContext dbContext) =>
                {
                    var movie = await dbContext
                        .Movies.Include(mv => mv.Genre)
                        .FirstOrDefaultAsync(mv => mv.MovieId == id);
                    return movie is null ? Results.NotFound() : Results.Ok(movie.ToMovieDto());
                }
            )
            .WithName(GetMovieEndPointName);

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

        group.MapGet(
            "$/{pattern}",
            async (string pattern, BookingMovieTicketsContext dbContext) =>
            {
                var movies = await dbContext
                    .Movies.Where(mv => mv.Title.Contains(pattern))
                    .ToListAsync();
            }
        );

        // POST /movie
        group.MapPost(
            "/",
            async (CreateMovieDto newMovie, BookingMovieTicketsContext dbContext) =>
            {
                try
                {
                    Movie movie = newMovie.ToEntity();
                    await dbContext.Movies.AddAsync(movie);
                    await dbContext.SaveChangesAsync();

                    // new way to get create movie
                    var createdMovie = await dbContext
                        .Movies.Include(m => m.Genre)
                        .FirstOrDefaultAsync(m => m.MovieId == movie.MovieId);

                    return Results.CreatedAtRoute(
                        GetMovieEndPointName,
                        new { id = createdMovie?.MovieId },
                        createdMovie?.ToMovieDto()
                    );
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    Console.WriteLine(ex.Message);
                    return Results.StatusCode(500);
                }
            }
        );

        // PATCH /movie
        // group.MapPatch(
        //     "/",
        //     async (CreateGameDto newMovie, BookingMovieTicketsContext dbContext) => {


        //      }
        // );


        // public async Task UpdateMovieAsync(Movie updatedMovie)
        // {
        //     var movie = await _context.Movies.FirstOrDefaultAsync(mv => mv.MovieId == updatedMovie.MovieId);
        //     if (movie != null)
        //     {
        //         movie.Title = updatedMovie.Title;
        //         movie.DurationMinutes = updatedMovie.DurationMinutes;
        //         movie.ReleaseDate = updatedMovie.ReleaseDate;
        //         movie.Rating = updatedMovie.Rating;
        //         movie.Certification = updatedMovie.Certification;
        //         movie.PlotSummary = updatedMovie.PlotSummary;
        //         movie.PosterUrl = updatedMovie.PosterUrl;
        //         movie.PosterVerticalUrl = updatedMovie.PosterVerticalUrl;

        //         await _context.SaveChangesAsync();
        //     }
        // }


        // public async Task<(ObservableCollection<Movie> Movies, int TotalRecords)> GetMoviesByNameAsync(string title, int page = 1, string filter = "", string sort = "", string sortType = "")
        // {
        //     const int PAGE_SIZE = 3;

        //     var query = _context.Movies.AsQueryable();

        //     if (!string.IsNullOrEmpty(title))
        //     {
        //         query = query.Where(mv => mv.Title.Contains(title));
        //     }

        //     if (!string.IsNullOrEmpty(filter))
        //     {
        //         query = query.Where(mv => mv.Genres.Any(g => g.GenreId.ToString() == filter));
        //     }

        //     if (!string.IsNullOrEmpty(sort))
        //     {
        //         query = sortType.ToLower() == "desc" ? query.OrderByDescending(mv => EF.Property<object>(mv, sort))
        //                                               : query.OrderBy(mv => EF.Property<object>(mv, sort));
        //     }

        //     var totalRecords = await query.CountAsync();

        //     var movies = await query.Skip((page - 1) * PAGE_SIZE)
        //                             .Take(PAGE_SIZE)
        //                             .ToListAsync();

        //     return (new ObservableCollection<Movie>(movies), totalRecords);
        // }


        // public async Task<int> GetTotalShowTimeAsync(string filter)
        // {
        //     DateTime startDate;
        //     DateTime endDate;

        //     switch (filter.ToLower())
        //     {
        //         case "today":
        //             startDate = DateTime.Today;
        //             endDate = startDate.AddDays(1);
        //             break;
        //         case "month":
        //             startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        //             endDate = startDate.AddMonths(1);
        //             break;
        //         case "week":
        //             startDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
        //             endDate = startDate.AddDays(7);
        //             break;
        //         default:
        //             throw new ArgumentException("Invalid filter value. Supported values are 'today', 'month', and 'week'.");
        //     }

        //     return await _context.Showtimes
        //                          .Where(st => st.ShowtimeDatetime >= startDate && st.ShowtimeDatetime < endDate)
        //                          .CountAsync();
        // }



        return group;
    }
}
