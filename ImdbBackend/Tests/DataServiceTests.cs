using DataLayer.Titles;
using DataLayer.Bookmarkings;
using Moq;
using DBConnection.Titles;


namespace Tests;

public class DataServiceTests
{

    string connectionString = "";



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
        var service = new TitleDataService(connectionString);

        var result = service.GetTitles(10, 0);
            
        Console.WriteLine("Result count: " + result.Count);

        Assert.IsType<List<Title>>(result);
        Assert.True(result.Count == 10);
        Assert.True(result.ElementAt(0).TConst == "tt0052520");

    }

    [Fact]
    public void Returns_Title_By_Id()
    {
        var service = new TitleDataService(connectionString);

        var result = service.GetTitleById("tt0052520");
        Assert.IsType<Title>(result);
        Assert.True(result.TConst == "tt0052520");

    }

    [Fact]
    public void Title_Has_RatingTitleAlternativesTitlePrinciplesKnownForProductionTitleEpisodesGenres()
    {
        

        try
        {
            var service = new TitleDataService(connectionString);
            var title = service.GetTitleById("tt0052520");

            Assert.NotNull(title.Rating);
            Assert.NotNull(title.TitleAlternatives);
            Assert.NotNull(title.Principals);
            Assert.NotNull(title.KnownFors);
            Assert.NotNull(title.ProductionPersons);
            Assert.NotNull(title.Episodes);
            Assert.NotNull(title.Genres);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            throw;
        }

       
    }


    [Fact]
    public void Adds_Personality_Bookmark()
    {
        var mockService = new Mock<IBookmarkingDataService>();
        var userId = 1;
        var nconst = "nm0000001";

        mockService.Setup(service => service.AddPersonalityBookmarking(userId, nconst))
                    .Returns((PersonalityBookmarking)new PersonalityBookmarking { UserId = userId, NConst = nconst, Timestamp = DateTime.Now });

        var mockedDataService = mockService.Object;

        var result = mockedDataService.AddPersonalityBookmarking(userId, nconst);

        Assert.NotNull(result);
        Assert.Equal(userId, result.UserId);
        Assert.Equal(nconst, result.NConst);
        Assert.True(result.Timestamp != default(DateTime));
    }

    [Fact]
    public void DeletePersonalityBookmarking_Removes_Record_When_Exists()
    {
        var mockService = new Mock<IBookmarkingDataService>(); 
        var userId = 1;
        var nconst = "nm0000001";

        mockService.Setup(service => service.DeletePersonalityBookmarking(userId, nconst))
                    .Returns((PersonalityBookmarking)new PersonalityBookmarking { UserId = userId, NConst = nconst, Timestamp = DateTime.Now });

        var mockedDataService = mockService.Object;

        var result = mockedDataService.DeletePersonalityBookmarking(userId, nconst);

        Assert.NotNull(result);
        Assert.Equal(userId, result.UserId);
        Assert.Equal(nconst, result.NConst);
    }




}