using DataLayer.Bookmarkings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DBConnection.Bookmarkings
{
    public class BookmarkingDataService : IBookmarkingDataService
    {
        private readonly string connectionString;

        public BookmarkingDataService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public PersonalityBookmarking? AddPersonalityBookmarking(int userid, string nconst)
        {
            using (var db = new ImdbContext(connectionString))
            {
                // Check if the bookmark already exists
                var existingBookmark = db.PersonalityBookmarkings
                    .FirstOrDefault(pb => pb.UserId == userid && pb.NConst == nconst);

                if (existingBookmark != null)
                {
                    // Handle the case when the bookmark already exists
                    return null; // Or throw an exception, or return a specific message
                }

                return db.PersonalityBookmarkings
                    .FromSqlInterpolated($"SELECT * FROM add_personality({userid}, {nconst})")
                    .AsEnumerable()
                    .FirstOrDefault();
            }
        }

        public TitleBookmarking? AddTitleBookmarking(int userid, string titleId)
        {
            using (var db = new ImdbContext(connectionString))
            {
                return db.TitleBookmarkings
                    .FromSqlInterpolated($"SELECT * FROM add_title({userid}, {titleId})")
                    .AsEnumerable()
                    .FirstOrDefault();
            }
        }

        public PersonalityBookmarking? DeletePersonalityBookmarking(int userid, string nconst)
        {
            using (var db = new ImdbContext(connectionString))
            {
                return db.PersonalityBookmarkings
                    .FromSqlInterpolated($"SELECT * FROM delete_personality({userid}, {nconst})")
                    .AsEnumerable()
                    .FirstOrDefault();
            }
        }

        public TitleBookmarking? DeleteTitleBookmarking(int userid, string titleId)
        {
            using (var db = new ImdbContext(connectionString))
            {
                return db.TitleBookmarkings
                    .FromSqlInterpolated($"SELECT * FROM delete_title({userid}, {titleId})")
                    .AsEnumerable()
                    .FirstOrDefault();
            }
        }

        public List<PersonalityBookmarking> GetPersonalitiesBookmarkedByUser(int userId)
        {
            using (var db = new ImdbContext(connectionString))
            {
                return db.PersonalityBookmarkings
                    .FromSqlInterpolated($"SELECT * FROM get_personalities_by_user({userId})")
                    .ToList(); // Fetch all results as a list
            }
        }

        public List<TitleBookmarking> GetTitlesBookmarkedByUsers(int userId)
        {
            using (var db = new ImdbContext(connectionString))
            {
                return db.TitleBookmarkings
                    .FromSqlInterpolated($"SELECT * FROM get_titles_by_user({userId})")
                    .ToList(); // Fetch all results as a list
            }
        }
    }
}
