using MovieTickets.Api.Dtos;
using MovieTickets.Api.EntityModels;

namespace MovieTickets.Api.Mapping;

public static class GenreMapping
{
    public static GenreDto toDto(this Genre genre)
    {
        return new(genre.GenreId, genre.Name);
    }
}
