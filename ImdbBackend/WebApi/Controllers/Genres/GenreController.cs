using Microsoft.AspNetCore.Mvc;
using DataLayer.Genres;
using WebApi.Models.Genres;
using Mapster;


namespace WebApi.Controllers.Genres;

[ApiController]
[Route("api/genre")]


public class GenreController(IGenreDataService dataService, LinkGenerator linkGenerator) : BaseController(linkGenerator)
{
    private readonly IGenreDataService _dataService = dataService;


    private readonly LinkGenerator _linkGenerator = linkGenerator;

    [HttpGet(Name = nameof(GetGenres))]

    public IActionResult GetGenres(int pageSize = 2, int pageNumber = 0)
    {
        var genres = _dataService.GetGenres(pageSize, pageNumber);

        List<GenreModel> genreModels = genres.Select(genre => AdaptGenreToGenreModel(genre)).ToList();

        var numberOfItmes = _dataService.NumberOfGenres();

        string linkName = nameof(GetGenres);

        object result = CreatePaging(pageNumber, pageSize, numberOfItmes, linkName, genreModels);

        return Ok(result);
    }

    [HttpGet("{genreId}", Name = nameof(GetGenreById))]

    public IActionResult GetGenreById(int genreId)
    {
        var genre = _dataService.GetGenreById(genreId);
        if (genre == null)
        {
            return NotFound();
        }

        var genreModel = AdaptGenreToGenreModel(genre);
        return Ok(genreModel);
    }


        private GenreModel AdaptGenreToGenreModel(Genre genre)
    {

        var genreModel = genre.Adapt<GenreModel>();
        genreModel.Url = GetUrl(nameof(GetGenreById), new {genreId = genre.GenreId});
        return genreModel;

    }
}

