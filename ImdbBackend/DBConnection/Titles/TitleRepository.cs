using DataLayer.Titles;
using Microsoft.EntityFrameworkCore;

namespace DBConnection.Titles
{
    public class TitleRepository(string connectionString) : ITitleRepository
    {
        private readonly string _connectionString = connectionString;

        public List<Title> GetTitles(int pageSize, int pageNumber)
        {
            var db = new TitleContext(_connectionString);
            return db.Titles.Skip(pageNumber * pageSize).Take(pageSize).ToList();
        }

        public Title? GetTitleById(string tconst)
        {
           var db = new TitleContext(_connectionString);
            return db.Titles.FirstOrDefault(title => title.TConst == tconst);
        }

        public int NumberOfTitles()
        {
            var db = new TitleContext(_connectionString);
            return db.Titles.Count();
        }
    }

}
