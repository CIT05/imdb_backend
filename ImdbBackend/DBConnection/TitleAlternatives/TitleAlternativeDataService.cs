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

        public TitleAlternative GetTitleAlternative(int akasId)
        {
            var db = new ImdbContext(_connectionString);
            var result = db.TitleAlternatives.Include(akas => akas.Types).FirstOrDefault(akas =>
            akas.AkasId == akasId);
            
            if (result == null)
            {
                throw new InvalidOperationException("TitleAlternative not found.");
            }
            
            return result;
        }

        public List<TitleAlternative> GetTitleAlternativeForTitle(string tconst)
        {
            var db = new ImdbContext(_connectionString);
            var result = db.TitleAlternatives.Include(akas => akas.Types).Where(ta => ta.TConst == tconst).ToList();

            if (result.Count == 0)
            {
                throw new InvalidOperationException("TitleAlternative not found.");
            }

            return result;

        }



        public int NumberOfTitleAlternatives()
        {
            var db = new ImdbContext(_connectionString);
            return db.TitleAlternatives.Count();
        }
    }
    
}
