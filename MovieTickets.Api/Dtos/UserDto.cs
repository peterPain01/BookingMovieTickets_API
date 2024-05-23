namespace MovieTickets.Api;

public record UserDto(
    int Id,
    string Username,
    string Email,
    string Password,
    string FullName,
    DateOnly BirthDate,
    int IsAdmin,
    string Gender
);
