namespace DataLayer.Users;

public interface IUserDataService
{

    User? GetUserById(int UserId);

    User? CreateUser(string username, string password, string language);

    bool DeleteUser(int UserId);

    User? UpdateUser(int UserId, string username, string password, string language);

}
