using DataLayer.TitleAlternatives;
using DBConnection.TitleAlternativeAlternatives;

namespace DBConnection.TitleAlternatives
{
    public class TitleAlternativeRepository : ITitleAlternativeRepository
    {
        private readonly string _connectionString;

        public TitleAlternativeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<TitleAlternative> GetTitleAlternatives(int pageSize, int pageNumber)
        {
            var db = new TitleAlternativeContext(_connectionString);
            return db.TitleAlternatives.Skip(pageNumber * pageSize).Take(pageSize).ToList();
        }

        public TitleAlternative GetTitleAlternative(int akasId, int ordering)
        {
            var db = new TitleAlternativeContext(_connectionString);
            return db.TitleAlternatives.FirstOrDefault(akas =>
            akas.AkasId == akasId &&
            akas.Ordering == ordering
       ) ?? new TitleAlternative();
        }

        public int NumberOfTitleAlternatives()
        {
            var db = new TitleAlternativeContext(_connectionString);
            return db.TitleAlternatives.Count();
        }
    }
    
}
