using DataLayer.Titles;
using Moq;

namespace Tests;

public class DataServiceTests
{
    // Title

    Title TitleStub1 = new Title { TConst = "tt0000001", TitleType = "movie", PrimaryTitle = "The Big Operator", OriginalTitle = "The Big Operator", IsAdult = false, StartYear = "1959", EndYear = null, RuntimeMinutes = 91, Plot = null, Poster = null };
    Title TitleStub2 = new Title { TConst = "tt0000002", TitleType = "movie", PrimaryTitle = "The Big Operator", OriginalTitle = "The Big Operator", IsAdult = false, StartYear = "1959", EndYear = null, RuntimeMinutes = 91, Plot = null, Poster = null };
    Title TitleStub3 = new Title { TConst = "tt0000003", TitleType = "movie", PrimaryTitle = "The Big Operator", OriginalTitle = "The Big Operator", IsAdult = false, StartYear = "1959", EndYear = null, RuntimeMinutes = 91, Plot = null, Poster = null };
    Title TitleStub4 = new Title { TConst = "tt0000004", TitleType = "movie", PrimaryTitle = "The Big Operator", OriginalTitle = "The Big Operator", IsAdult = false, StartYear = "1959", EndYear = null, RuntimeMinutes = 91, Plot = null, Poster = null };
    Title TitleStub5 = new Title { TConst = "tt0000005", TitleType = "movie", PrimaryTitle = "The Big Operator", OriginalTitle = "The Big Operator", IsAdult = false, StartYear = "1959", EndYear = null, RuntimeMinutes = 91, Plot = null, Poster = null };
    Title TitleStub6 = new Title { TConst = "tt0000006", TitleType = "movie", PrimaryTitle = "The Big Operator", OriginalTitle = "The Big Operator", IsAdult = false, StartYear = "1959", EndYear = null, RuntimeMinutes = 91, Plot = null, Poster = null };
    Title TitleStub7 = new Title { TConst = "tt0000007", TitleType = "movie", PrimaryTitle = "The Big Operator", OriginalTitle = "The Big Operator", IsAdult = false, StartYear = "1959", EndYear = null, RuntimeMinutes = 91, Plot = null, Poster = null };
    Title TitleStub8 = new Title { TConst = "tt0000008", TitleType = "movie", PrimaryTitle = "The Big Operator", OriginalTitle = "The Big Operator", IsAdult = false, StartYear = "1959", EndYear = null, RuntimeMinutes = 91, Plot = null, Poster = null };
    Title TitleStub9 = new Title { TConst = "tt0000009", TitleType = "movie", PrimaryTitle = "The Big Operator", OriginalTitle = "The Big Operator", IsAdult = false, StartYear = "1959", EndYear = null, RuntimeMinutes = 91, Plot = null, Poster = null };
    Title TitleStub10 = new Title { TConst = "tt0000010", TitleType = "movie", PrimaryTitle = "The Big Operator", OriginalTitle = "The Big Operator", IsAdult = false, StartYear = "1959", EndYear = null, RuntimeMinutes = 91, Plot = null, Poster = null };

    List<Title> titlesStub = new List<Title>();


    [Fact]
    public void Title_Object_HasTConstTitleTypePrimaryTitleOriginalTitleIsAdultStartYearEndYearRuntimeMinutesPlotAndPoster()
    {
        var title = new Title();
        Assert.Equal("",title.TConst);
        Assert.Equal("",title.TitleType);
        Assert.Equal("",title.PrimaryTitle);
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
        var title = new Title();
        var mockService = new Mock<ITitleDataService>();

        titlesStub.Add(TitleStub1);
        titlesStub.Add(TitleStub2);
        titlesStub.Add(TitleStub3);
        titlesStub.Add(TitleStub4);
        titlesStub.Add(TitleStub5);
        titlesStub.Add(TitleStub6);
        titlesStub.Add(TitleStub7);
        titlesStub.Add(TitleStub8);
        titlesStub.Add(TitleStub9);
        titlesStub.Add(TitleStub10);


        mockService.Setup(service => service.GetTitles(10, 0)).Returns(titlesStub);

        var mockedTitleDataSerivce = mockService.Object;

        var result = mockedTitleDataSerivce.GetTitles(10, 0);
        Assert.IsType<List<Title>>(result);
        Assert.True(result.Count == 10);
        Assert.True(result.ElementAt(0).TConst == "tt0000001"); 

    }

    [Fact]
    public void Returns_Title_By_Id()
    {
        var title = new Title();
        var mockService = new Mock<ITitleDataService>();

        titlesStub.Add(TitleStub1);
        titlesStub.Add(TitleStub2);
        titlesStub.Add(TitleStub3);
        titlesStub.Add(TitleStub4);
        titlesStub.Add(TitleStub5);
        titlesStub.Add(TitleStub6);
        titlesStub.Add(TitleStub7);
        titlesStub.Add(TitleStub8);
        titlesStub.Add(TitleStub9);
        titlesStub.Add(TitleStub10);

        mockService.Setup(service => service.GetTitleById("tt0000001")).Returns(titlesStub.Where(title=> title.TConst == "tt0000001").SingleOrDefault());

        var mockedTitleDataSerivce = mockService.Object;

        var result = mockedTitleDataSerivce.GetTitleById("tt0000001");
        Assert.IsType<Title>(result);
        Assert.True(result.TConst == "tt0000001");


    }
}