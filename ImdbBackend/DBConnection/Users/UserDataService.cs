using DataLayer.Users;
using Microsoft.EntityFrameworkCore;

namespace DBConnection.Users;

public class UserDataService(string connectionString) : IUserDataService
{
    public User? GetUserById(int UserId)
    {
        var db = new ImdbContext(connectionString);
        return db.Users.Find(UserId);
    }

    public User? CreateUser(string username, string password, string language)
    {
        var db = new ImdbContext(connectionString);

        CreatedUserId createdUser = db.CreatedUserIds.FromSqlInterpolated($"select * from create_user({username}, {password}, {language})").ToList().FirstOrDefault();

        if(createdUser == null)
        {
            return null;
        }

        User newUser = new() { UserId = createdUser.UserId, Username = username, Password = password, Language = language };

        return newUser;
    }
}
