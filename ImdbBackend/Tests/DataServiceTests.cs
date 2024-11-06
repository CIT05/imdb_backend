using Microsoft.Extensions.Configuration;
using DataLayer.Titles;
using DataLayer.Bookmarkings;
using DBConnection.Titles;
using DBConnection.Bookmarkings;
using DBConnection.Users;
using DBConnection.Ratings;
using DataLayer.Ratings;


namespace Tests;

public class DataServiceTests
{
    private readonly IConfiguration _configuration;

    public DataServiceTests()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("config.json", optional: false, reloadOnChange: true)
            .Build();
    }



    // Title

    [Fact]
    public void Title_Object_HasTConstTitleTypePrimaryTitleOriginalTitleIsAdultStartYearEndYearRuntimeMinutesPlotAndPoster()
    {
        var title = new Title();
        Assert.Equal("", title.TConst);
        Assert.Equal("", title.TitleType);
        Assert.Equal("", title.PrimaryTitle);
        Assert.Equal("", title.OriginalTitle);
        Assert.False(title.IsAdult);
        Assert.Null(title.StartYear);
        Assert.Null(title.EndYear);
        Assert.Null(title.RuntimeMinutes);
        Assert.Null(title.Plot);
        Assert.Null(title.Poster);

    }

    [Fact]
    public void Returns_List_Of_Titles()
    {
        string connectionString = _configuration["ConnectionString"];
        var service = new TitleDataService(connectionString);

        var result = service.GetTitles(10, 0);
        Assert.IsType<List<Title>>(result);
        Assert.True(result.Count == 10);
        Assert.True(result.ElementAt(0).TConst == "tt0052520");

    }

    [Fact]
    public void Returns_Title_By_Id()
    {
        string connectionString = _configuration["ConnectionString"];
        var service = new TitleDataService(connectionString);

        var result = service.GetTitleById("tt0052520");
        Assert.IsType<Title>(result);
        Assert.True(result.TConst == "tt0052520");

    }

    [Fact]
    public void Title_Has_TitleAlternativesTitlePrincipalsRatingKnownForsProductionPersonsEpisodesAndGenres()
    {
        string connectionString = _configuration["ConnectionString"];

        var service = new TitleDataService(connectionString);

        var title = service.GetTitleById("tt0052520");
        Assert.NotNull(title.TitleAlternatives);
        Assert.NotNull(title.Principals);
        Assert.NotNull(title.Rating);
        Assert.NotNull(title.KnownFors);
        Assert.NotNull(title.ProductionPersons);
        Assert.NotNull(title.Episodes);
        Assert.NotNull(title.Genres);
    }

    [Fact]
    public void GetNumberOfTitles_Returns_Number_Of_Titles()
    {
        string connectionString = _configuration["ConnectionString"];

        var service = new TitleDataService(connectionString);

        var result = service.NumberOfTitles();
        Assert.IsType<int>(result);
        Assert.True(result > 0);
    }


    // Personality_Bookmark

    [Fact]
    public void PersonalityBookmarking_Object_HasUserIdNConstAndTimestamp()
    {
        var bookmarking = new PersonalityBookmarking();
        Assert.Equal(0, bookmarking.UserId);
        Assert.Equal("", bookmarking.NConst);
        Assert.Equal(default(DateTime), bookmarking.Timestamp);
    }

    [Fact]
    public void Get_Personality_Bookmarks_Returns_List_Of_Bookmarks()
    {
        string connectionString = _configuration["ConnectionString"];

        var service = new BookmarkingDataService(connectionString);
        var userService = new UserDataService(connectionString);

        var user = userService.CreateUser("testUser123", "testPassword", "en", "someSaltValue");
        var userId = user.UserId;

        string nconst = "nm0000002";

        service.AddPersonalityBookmarking(userId, nconst);
        var result = service.GetPersonalitiesBookmarkedByUser(userId);

        service.DeletePersonalityBookmarking(userId, nconst);

        userService.DeleteUser(userId);

        Assert.IsType<List<PersonalityBookmarking>>(result);
        Assert.True(result.Count > 0);
    }

    [Fact]
    public void Adds_Personality_BookmarkShouldAddBookmark()
    {
        string connectionString = _configuration["ConnectionString"];

        var service = new BookmarkingDataService(connectionString);

        int userId = 1;
        string nconst = "nm0000001";

        service.AddPersonalityBookmarking(userId, nconst);
        var result = service.GetPersonalitiesBookmarkedByUser(userId).LastOrDefault();
        service.DeleteTitleBookmarking(userId, nconst);

        Assert.NotNull(result);
        Assert.Equal(userId, result.UserId);
        Assert.Equal(nconst, result.NConst);
        Assert.True(result.Timestamp != default(DateTime));
    }

    [Fact]
    public void DeletePersonalityBookmarking_Removes_Record_When_Exists()
    {

        string connectionString = _configuration["ConnectionString"];

        var userId = 1;
        var nconst = "nm0000001";

        var service = new BookmarkingDataService(connectionString);

        service.AddPersonalityBookmarking(userId, nconst);
        var result1 = service.GetPersonalitiesBookmarkedByUser(userId);

        service.DeletePersonalityBookmarking(userId, nconst);
        var result2 = service.GetPersonalitiesBookmarkedByUser(userId);

        Assert.Equal(1, result1.Count - result2.Count);
    }

    // Rating

    [Fact]
    public void Rating_Object_HasTConstAverageRatingAndNumberOfVotes()
    {
        var rating = new Rating();
        Assert.Equal("", rating.TConst);
        Assert.Equal(0, rating.AverageRating);
        Assert.Equal(0, rating.NumberOfVotes);
    }

    [Fact]
    public void GetRatingByTConst_Returns_Rating()
    {

        string connectionString = _configuration["ConnectionString"];

        var service = new RatingDataService(connectionString);

        int pageSize = 10;
        int pageNumber = 0;

        var result = service.GetRatings(pageSize, pageNumber);
        Assert.IsType<List<Rating>>(result);
        Assert.True(result.Count > 0);
    }

    [Fact]
    public void GetRatingByTConst_Returns_Rating_By_TConst_With_AverageRatingAndNumberOfVotes()
    {

        string connectionString = _configuration["ConnectionString"];

        var service = new RatingDataService(connectionString);

        var result = service.GetRatingById("tt0052520");
        Assert.IsType<Rating>(result);
        Assert.Equal("tt0052520", result.TConst);
        Assert.True(result.AverageRating > 0);
        Assert.True(result.NumberOfVotes > 0);
    }

    [Fact]
    public void GetRatingByTConst_Returns_Null_When_Rating_Not_Found()
    {

        string connectionString = _configuration["ConnectionString"];

        var service = new RatingDataService(connectionString);

        var result = service.GetRatingById("tt0000000");
        Assert.Null(result);
    }

    [Fact]
    public void AddRating_Returns_AddRatingResult()
    {

        string connectionString = _configuration["ConnectionString"];

        var service = new RatingDataService(connectionString);

        var result = service.AddRating(1, "tt0052520", 8.0m);
        Assert.IsType<bool>(result);
        Assert.True(result);
    }





}
