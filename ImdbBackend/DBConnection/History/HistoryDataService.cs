using DataLayer.History;
using Microsoft.EntityFrameworkCore;


namespace DBConnection.History
{
    public class HistoryDataService(string connectionString) : IHistoryDataService
    {

        private readonly string _connectionString = connectionString;

        public List<SearchHistory> GetSearchHistory()
        {
           var db = new ImdbContext(_connectionString);
            return db.SearchHistory.ToList();
        }

        public List<SearchHistory> GetSearchHistoryByUser(int userId)
        {
            var db = new ImdbContext(_connectionString);
            return db.SearchHistory.Where(search => search.UserId == userId).ToList();
        }

        public List<SearchHistory> GetSearchHistoryByPhrase(string phrase)
        {
            var db = new ImdbContext(_connectionString);
            return db.SearchHistory.Where(search => search.Phrase == phrase).ToList();
        }

        public List<SearchHistory> GetSearchHistoryByPhraseAndUser(string phrase, int userId)
        {
            var db = new ImdbContext(_connectionString);
            return db.SearchHistory.Where(search => search.Phrase == phrase && search.UserId == userId).ToList();
        }

        public List<RatingHistory> GetRatingHistory()
        {
            var db = new ImdbContext(_connectionString);
            return db.RatingHistory.ToList();
        }

        public List<RatingHistory> GetRatingHistoryByUser(int userId)
        {
            var db = new ImdbContext(_connectionString);
            return db.RatingHistory.Where(rating => rating.UserId == userId).ToList();
        }

        public List<RatingHistory> GetRatingHistoryByTConst(string tConst)
        {
            var db = new ImdbContext(_connectionString);
            return db.RatingHistory.Where(rating => rating.TConst == tConst).ToList();
        }

        public List<RatingHistory> GetRatingHistoryByTConstAndUser(string tConst, int userId)
        {
            var db = new ImdbContext(_connectionString);
            return db.RatingHistory.Where(rating => rating.TConst == tConst && rating.UserId == userId).ToList();
        }
        
    }
}