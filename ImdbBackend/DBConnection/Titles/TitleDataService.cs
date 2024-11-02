using DataLayer.Titles;
using Microsoft.EntityFrameworkCore;

namespace DBConnection.Titles
{
    public class TitleDataService(string connectionString) : ITitleDataService
    {
        private readonly string _connectionString = connectionString;


        public List<Title> GetTitles(int pageSize, int pageNumber)
        {
            var db = new ImdbContext(_connectionString);
            return db.Titles
                 .OrderBy(t => t.TConst)
                 .Skip(pageNumber * pageSize)
                 .Take(pageSize)
                 .Include(title => title.Rating)
                 .Select(title => new
                    {
                     Title = title,
                     OrderedPrincipals = title.Principals.OrderBy(p => p.Ordering).ToList()
                     })
                 .ToList()
                 .Select(t =>
                     {
                      t.Title.Principals = t.OrderedPrincipals;
                      return t.Title;
                     })
                 .ToList();
        }

        public Title? GetTitleById(string tconst)
        {
           var db = new ImdbContext(_connectionString);
            var title = db.Titles
                .Where(title => title.TConst == tconst)
                .Include(title => title.Rating)
                .Select(title => new
     {
         Title = title,
         OrderedPrincipals = title.Principals.OrderBy(p => p.Ordering).ToList()
     })
      .ToList()
     .Select(t =>
     {
         t.Title.Principals = t.OrderedPrincipals;
         return t.Title;
     })
     .SingleOrDefault();

            return title;
        }

        public int NumberOfTitles()
        {
            var db = new ImdbContext(_connectionString);
            return db.Titles.Count();
        }
    }

}
