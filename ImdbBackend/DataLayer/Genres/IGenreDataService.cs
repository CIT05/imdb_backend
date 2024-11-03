namespace DataLayer.Genres;

public interface IGenreDataService
{
    List<Genre> GetGenres(int pageSize, int pageNumber);

    Genre? GetGenreById(int genreId);

    int NumberOfGenres();
}