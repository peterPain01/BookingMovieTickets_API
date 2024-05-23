using Microsoft.EntityFrameworkCore;
using MovieTickets.Api.EntityModels;

namespace MovieTickets.Api.EntityModels; 

public static class DataExtensions
{
    public static async Task MigrateDbAsync(this WebApplication app) { 
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookingMovieAppContext>();
        await dbContext.Database.MigrateAsync();
    }
}
