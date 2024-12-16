using DataLayer.Users;
using DataLayer.History;
using Microsoft.EntityFrameworkCore;

namespace DBConnection.Users;

public class UserDataService(string connectionString) : IUserDataService
{
    public User? GetUserById(int UserId)
    {
        var db = new ImdbContext(connectionString);
        return db.Users.Include(u => u.RatingHistory)
                    .ThenInclude(rating => rating.Title)
                 .Include(u => u.SearchHistory)
                 .Include(u => u.PersonalityBookmarkings)
                    .ThenInclude(personalityBookmarking => personalityBookmarking.Person)
                .Include(u => u.TitleBookmarkings)
                    .ThenInclude(titleBookmarking => titleBookmarking.Title)
                 .FirstOrDefault(u => u.UserId == UserId);
    }

    public User? GetUserByName(string UserName)
    {
        var db = new ImdbContext(connectionString);
        return db.Users.Include(u => u.RatingHistory)
                       .ThenInclude(rating => rating.Title)
                 .Include(u => u.SearchHistory)
                 .Include(u => u.PersonalityBookmarkings)
                       .ThenInclude(personalityBookmarking => personalityBookmarking.Person)
                 .Include(u => u.TitleBookmarkings)
                       .ThenInclude(titleBookmarking => titleBookmarking.Title)
                 .FirstOrDefault(u => u.Username == UserName);
    }

    public User? CreateUser(string username, string password, string language, string salt)
    {
        var db = new ImdbContext(connectionString);

        CreateUserResult createdUser = db.CreateUserResults.FromSqlInterpolated($"select * from create_user({username}, {password}, {language}, {salt})").ToList().FirstOrDefault();

        if (createdUser == null)
        {
            return null;
        }

        User newUser = new() { UserId = createdUser.UserId, Username = username, Password = password, Language = language, Salt = salt };

        return newUser;
    }

    public bool DeleteUser(int userId)
    {
        var db = new ImdbContext(connectionString);

        int affectedRows = db.Users
        .Where(user => user.UserId == userId)
        .ExecuteDelete();

        return affectedRows > 0;
    }

    public User? UpdateUser(int userId, string username, string language)
    {
        var db = new ImdbContext(connectionString);

        UpdateUserResult updatedUserResult = db.UpdateUserResults
            .FromSqlInterpolated($"select * from update_user({userId}, {username}, {language})")
            .ToList()
            .FirstOrDefault();

        if (updatedUserResult == null)
        {
            return null;
        }

        User updatedUser = new() { UserId = userId, Username = username, Language = language };
        return updatedUser;
    }
}

