using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataLayer.History
{
    public interface IHistoryDataService
    {
        List<SearchHistory> GetSearchHistory();
        List<SearchHistory> GetSearchHistoryByUser(int userId);

        List<SearchHistory> GetSearchHistoryByPhrase(string phrase);


        List<RatingHistory> GetRatingHistory();

        List<RatingHistory> GetRatingHistoryByUser(int userId);

        List<RatingHistory> GetRatingHistoryByTConst(string tConst);

    }
}