using MovieTickets.Api.EndPoints;
using MovieTickets.Api.EntityModels;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("BookingMovieTicketsApp");
builder.Services.AddSqlServer<BookingMovieTicketsContext>(connString);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .WithOrigins("http://localhost:5288")
            .AllowAnyHeader()
            .WithMethods("GET", "POST", "PATCH", "DELETE")
            .SetIsOriginAllowed((host) => true)
            .AllowCredentials();
    });
});

var app = builder.Build();
app.UseCors(); // this line is added

// Mapping
app.MapMoviesEndPoints();
app.MapGenresEndPoints();

await app.MigrateDbAsync();

app.Run();
