namespace test;

using System.Diagnostics;
using test.Dtos;
using test.helper;

// implement IDisposable
public class MovieApiTest : IDisposable
{
    private static readonly HttpClient _httpClient =
        new() { BaseAddress = new Uri("http://localhost:5288") };

    public void Dispose()
    {
        _httpClient.DeleteAsync("/state").GetAwaiter().GetResult();
        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task GivenARequest_WhenCallingGetMovieById_ThenTheAPIReturnsExpectedResponse()
    {
        // Arrange.
        var expectedStatusCode = System.Net.HttpStatusCode.OK;
        var expectedContent = new
        {
            MovieId = 1,
            Title = "MAI",
            Genre = "Comedy",
            DurationMinutes = 123,
            ReleaseDate = "2023-01-01",
            Rating = 8.5,
            Certification = 212,
            PlotSummary = "Plot summary for MAI",
            PosterUrl = "/Images/slider-mai.jpg",
            PosterVerticalUrl = "/Images/mv-0.8789199430907808.jpg",
            TrailerUrl = "trailer1.mp4"
        };

        var stopwatch = Stopwatch.StartNew();

        // Act.
        var response = await _httpClient.GetAsync("/movies/1");

        // Assert.
        await TestHelpers.AssertResponseWithContentAsync(
            stopwatch,
            response,
            expectedStatusCode,
            expectedContent
        );
    }

    [Fact]
    public async Task GivenARequest_WhenCallingGetMovies_ThenTheAPIReturnsExpectedResponse()
    {
        // Arrange
        var expectedStatusCode = System.Net.HttpStatusCode.OK;
        var expectedContent = new[]
        {
            new
            {
                MovieId = 1,
                Title = "MAI",
                Genre = "Comedy",
                DurationMinutes = 123,
                ReleaseDate = "2023-01-01",
                Rating = 8.5,
                Certification = 212,
                PlotSummary = "Plot summary for MAI",
                PosterUrl = "/Images/slider-mai.jpg",
                PosterVerticalUrl = "/Images/mv-0.8789199430907808.jpg",
                TrailerUrl = "trailer1.mp4"
            },
            new
            {
                MovieId = 2,
                Title = "Fast and Furious",
                Genre = "Action",
                DurationMinutes = 130,
                ReleaseDate = "2022-01-01",
                Rating = 8.0,
                Certification = 16,
                PlotSummary = "Plot summary for Fast and Furious",
                PosterUrl = "/Images/slider-faf.jpg",
                PosterVerticalUrl = "/Images/mv-faf.jpg",
                TrailerUrl = "trailer1.mp4"
            }
        };

        var stopwatch = Stopwatch.StartNew();

        // Act.
        var response = await _httpClient.GetAsync("/movies");

        // Assert.
        await TestHelpers.AssertResponseWithContentAsync(
            stopwatch,
            response,
            expectedStatusCode,
            expectedContent
        );
    }

    [Fact]
    public async Task GivenARequest_WhenCallingPostMovies_ThenTheAPIReturnsExpectedResponseAndAddMovie()
    {
        // Arrange.
        var expectedStatusCode = System.Net.HttpStatusCode.Created;
        var sendContent = new CreateMovieDto(
            "My Life",
            1,
            100,
            18,
            "My Life Can I Try",
            "123",
            "123",
            "123",
            new DateOnly(2003, 1, 1)
        );

        var expectedContent = new
        {
            MovieId = 12,
            Title = "My Life",
            Genre = "Drama",
            DurationMinutes = 100,
            ReleaseDate = new DateOnly(2003, 1, 1),
            Certification = 18,
            PlotSummary = "My Life Can I Try",
            PosterUrl = "123",
            PosterVerticalUrl = "123",
            TrailerUrl = "123"
        };

        var stopwatch = Stopwatch.StartNew();

        // Act.
        var response = await _httpClient.PostAsync(
            "/movies",
            TestHelpers.GetJsonStringContent(sendContent)
        );

        // Assert.
        await TestHelpers.AssertResponseWithContentAsync(
            stopwatch,
            response,
            expectedStatusCode,
            expectedContent
        );
    }
}
