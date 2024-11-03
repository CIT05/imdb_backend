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
                 .Include(u => u.SearchHistory)
                 .Include(u => u.PersonalityBookmarkings)
                .Include(u => u.TitleBookmarkings)
                 .FirstOrDefault(u => u.UserId == UserId);
    }

    public User? CreateUser(string username, string password, string language)
    {
        var db = new ImdbContext(connectionString);

        CreateUserResult createdUser = db.CreateUserResults.FromSqlInterpolated($"select * from create_user({username}, {password}, {language})").ToList().FirstOrDefault();

        if(createdUser == null)
        {
            return null;
        }

        User newUser = new() { UserId = createdUser.UserId, Username = username, Password = password, Language = language };

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

    public User? UpdateUser(int userId, string username, string password, string language)
    {
        var db = new ImdbContext(connectionString);

        UpdateUserResult updatedUserResult = db.UpdateUserResults.FromSqlInterpolated($"select * from update_user({userId}, {username}, {password}, {language})").ToList().FirstOrDefault();

        if (updatedUserResult == null)
        {
            return null;
        }

        User updatedUser = new() { UserId = userId, Username = username, Password = password, Language = language };
        return updatedUser;


    }

}
