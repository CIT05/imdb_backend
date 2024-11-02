using DataLayer.TitleAlternatives;
using Microsoft.EntityFrameworkCore;

namespace DBConnection.TitleAlternatives
{
    public class TitleAlternativeDataService(string connectionString) : ITitleAlternativeDataService
    {

        private readonly string _connectionString = connectionString;

        public List<TitleAlternative> GetTitleAlternatives(int pageSize, int pageNumber)
        {
            var db = new ImdbContext(_connectionString);
            return db.TitleAlternatives.Skip(pageNumber * pageSize).Take(pageSize).Include(title => title.Types).ToList();
        }

        public TitleAlternative GetTitleAlternative(int akasId, int ordering)
        {
            var db = new ImdbContext(_connectionString);
            var result = db.TitleAlternatives.FirstOrDefault(akas =>
            akas.AkasId == akasId &&
            akas.Ordering == ordering);
            
            if (result == null)
            {
                throw new InvalidOperationException("TitleAlternative not found.");
            }
            
            return result;
        }


        public List<TitleAlternative> GetTitleAlternativesByType(string type)
        {
            var db = new ImdbContext(_connectionString);
            return db.TitleAlternatives
                 .Include(title => title.Types)
                 .Where(title => title.Types.Any(t => t.TypeName == type))
                 .ToList();
        }


        public int NumberOfTitleAlternatives()
        {
            var db = new ImdbContext(_connectionString);
            return db.TitleAlternatives.Count();
        }
    }
    
}
