using MovieTickets.Api.EndPoints;
using MovieTickets.Api.EntityModels;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("BookingMovieTicketsApp");
builder.Services.AddSqlServer<BookingMovieTicketsContext>(connString);

var app = builder.Build();

// Mapping
app.MapMoviesEndPoints();
app.MapGenresEndPoints();


await app.MigrateDbAsync();

app.Run();
