namespace MovieTickets.Api.EndPoints;

public static class MoviesEndPoints
{
    public static RouteGroupBuilder MapMoviesEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("movies");

        group.Map("/", () => { });

        return group;
    }
}
