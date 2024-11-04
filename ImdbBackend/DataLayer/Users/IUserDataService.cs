namespace DataLayer.Users;

public interface IUserDataService
{

    User? GetUserById(int UserId);
    User? GetUserByName(string UserName);
    User? CreateUser(string username, string password, string language, string salt);

    bool DeleteUser(int UserId);

    User? UpdateUser(int UserId, string username, string password, string language);

}
