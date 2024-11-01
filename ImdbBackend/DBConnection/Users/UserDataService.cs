using DataLayer.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Users;

public class UserDataService(string connectionString) : IUserDataService
{
    public User? GetUserById(int UserId)
    {
        var db = new ImdbContext(connectionString);
        return db.Users.Find(UserId);
    }
}
