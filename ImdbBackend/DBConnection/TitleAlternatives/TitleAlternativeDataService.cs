using DataLayer.TitleAlternatives;

namespace DBConnection.TitleAlternatives
{
    public class TitleAlternativeDataService : ITitleAlternativeDataService
    {
        private readonly string _connectionString;

        public TitleAlternativeDataService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<TitleAlternative> GetTitleAlternatives(int pageSize, int pageNumber)
        {
            var db = new ImdbContext(_connectionString);
            return db.TitleAlternatives.Skip(pageNumber * pageSize).Take(pageSize).ToList();
        }

        public TitleAlternative GetTitleAlternative(int akasId, int ordering)
        {
            var db = new ImdbContext(_connectionString);
            return db.TitleAlternatives.FirstOrDefault(akas =>
            akas.AkasId == akasId &&
            akas.Ordering == ordering
       ) ?? new TitleAlternative();
        }

        public int NumberOfTitleAlternatives()
        {
            var db = new ImdbContext(_connectionString);
            return db.TitleAlternatives.Count();
        }
    }
    
}
