using DataLayer.TitleAlternatives;
using DataLayer.TitlePrincipals;
using DataLayer.Titles;
using DataLayer.Genres;
using Microsoft.EntityFrameworkCore;

namespace DBConnection.Titles
{
    public class TitleDataService(string connectionString) : ITitleDataService
    {
        private readonly string _connectionString = connectionString;


        public List<Title> GetTitles(int pageSize, int pageNumber)
        {
            var db = new ImdbContext(_connectionString);
            return db.Titles.OrderBy(t => t.TConst).Skip(pageNumber * pageSize).Take(pageSize).Include(title => title.Rating).Include(title => title.TitleAlternatives).Include(title => title.Principals).Include(title => title.Genres).ToList();
        }

        public Title? GetTitleById(string tconst)
        {
           var db = new ImdbContext(_connectionString);
            return db.Titles.Where(title => title.TConst == tconst).Include(title => title.Rating).Include(title => title.TitleAlternatives).Include(title => title.Principals).Include(title => title.Genres).SingleOrDefault();
        }

        public List<Genre> GetGenres(int genreId)
                {
                    var db = new ImdbContext(_connectionString);
                    return db.Genres.Where(genre => genre.GenreId == genreId).ToList();
                }

        public List<TitleAlternative> GetTitleAlternatives(string tconst)
        {
            var db = new ImdbContext(_connectionString);
            return db.TitleAlternatives.Where(alt => alt.TitleId == tconst).ToList();
        }

        public List<TitlePrincipal> GetTitlePrincipals(string tconst)
        {
            var db = new ImdbContext(_connectionString);
            return db.TitlePrincipals.Where(principal => principal.TConst == tconst).ToList();
        }

        public int NumberOfTitles()
        {
            var db = new ImdbContext(_connectionString);
            return db.Titles.Count();
        }
    }

}
