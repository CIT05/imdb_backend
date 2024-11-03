using DataLayer.TitleAlternatives;
using DataLayer.TitlePrincipals;
using DataLayer.Titles;
using DataLayer.Genres;
using DataLayer.TitleEpisodes;
using Microsoft.EntityFrameworkCore;

namespace DBConnection.Titles
{
    public class TitleDataService(string connectionString) : ITitleDataService
    {
        private readonly string _connectionString = connectionString;


        public List<Title> GetTitles(int pageSize, int pageNumber)
        {
            using var db = new ImdbContext(_connectionString);
            return db.Titles
                .OrderBy(t => t.TConst)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .Include(title => title.Rating)
                .Include(title => title.KnownFors)
                .Include(title => title.ProductionPersons)
                .Include(title => title.TitleAlternatives)
                .Include(title => title.Episodes)
                .Include(title=> title.Genres)
                .Include(title => title.Principals.OrderBy(p => p.Ordering)) // EF Core should apply ordering here
                .AsSplitQuery() // Ensures that related collections are loaded as separate queries
                .ToList();
            //var db = new ImdbContext(_connectionString);
            //return db.Titles.OrderBy(t => t.TConst).Skip(pageNumber * pageSize).Take(pageSize).Include(title => title.Rating).Include(title => title.TitleAlternatives).Include(title => title.Principals).Include(title => title.Genres).Include(title => title.Episodes).ToList();
        }

        public Title? 
            GetTitleById(string tconst)
        {
            using var db = new ImdbContext(_connectionString);
            var title = db.Titles
                .Where(title => title.TConst == tconst)
                .Include(title => title.Rating)
                .Include(title => title.Principals)
                .Include(title => title.KnownFors)
                .Include(title => title.ProductionPersons)
                .Include(title => title.TitleAlternatives)
                .Include(title => title.Episodes)
                .Include(title => title.Genres)
                .AsSplitQuery()
                .Select(title => new
                {
                    Title = title,
                    OrderedPrincipals = title.Principals.OrderBy(p => p.Ordering).ToList() // Convert to List
                })
                .AsEnumerable() // Switch to client-side evaluation here
                .Select(t =>
                {
                    t.Title.Principals = t.OrderedPrincipals; // Assign ordered principals
                    return t.Title; // Return the modified Title
                })
                .SingleOrDefault();

            return title;
          
           //var db = new ImdbContext(_connectionString);
           // return db.Titles.Where(title => title.TConst == tconst).Include(title => title.Rating).Include(title => title.TitleAlternatives).Include(title => title.Principals).Include(title => title.Genres).Include(title => title.Episodes).SingleOrDefault();
        }


    public List<TitleEpisode> GetTitleEpisodes(string tconst)
    {
        var db = new ImdbContext(_connectionString);
        return db.TitleEpisodes.Where(episode => episode.ParentTConst == tconst).ToList();
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
