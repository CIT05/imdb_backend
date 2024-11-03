
using DataLayer.TitleEpisodes;
using Microsoft.EntityFrameworkCore;

namespace DBConnection.TitleEpisodes
{
    using DataLayer.TitleEpisodes;
    using System.Collections.Generic;

    public class TitleEpisodeDataService : ITitleEpisodeDataService
    {
        private readonly string _connectionString;

        public TitleEpisodeDataService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public TitleEpisode GetTitleEpisode(string titleId)
        {
            var db = new ImdbContext(_connectionString);
            var titleEpisode = db.TitleEpisodes.Find(titleId);
            if (titleEpisode == null)
            {
                throw new KeyNotFoundException($"TitleEpisode with titleId '{titleId}' not found.");
            }
            return titleEpisode;
        }

        public List<TitleEpisode> GetTitleEpisodes(int pageSize, int pageNumber)
        {
            var db = new ImdbContext(_connectionString);
            return db.TitleEpisodes.Skip(pageNumber * pageSize).Take(pageSize).ToList();
        }

        public int NumberOfTitleEpisodes()
        {
            var db = new ImdbContext(_connectionString);
            return db.TitleEpisodes.Count();
        }
    }
}