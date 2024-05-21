using Microsoft.OpenApi.Models;
using MovieTickets.Api.EndPoints;
using MovieTickets.Api.EntityModels;
using MovieTickets.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("BookingMovieTicketsApp");
builder.Services.AddTransient<GlobalExceptionHandler>();
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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Version = "v1",
            Title = "Booking Movie Tickets - API",
            Description = "A simple example ASP.NET Core Web API",
        }
    );
});

var app = builder.Build();
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Booking Movie Tickets - API V1");
        c.RoutePrefix = string.Empty;
    });
}

// Mapping
app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionHandler>();

app.MapMoviesEndPoints();
app.MapGenresEndPoints();

await app.MigrateDbAsync();

app.Run();
