using MovieTickets.Api.EndPoints;
using MovieTickets.Api.EntityModels;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("BookingMovieTicketsApp");
builder.Services.AddSqlServer<BookingMovieTicketsContext>(connString);

var app = builder.Build();
app.MapMoviesEndPoints();
await app.MigrateDbAsync();

app.Run();
