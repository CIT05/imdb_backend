using DataLayer.Roles;

namespace DBConnection.Roles;

public class RoleDataService(string connectionString) : IRoleDataService
{
    private readonly string _connectionString = connectionString;

    public List<Role> GetRoles(int pageSize, int pageNumber)
    {
        var db = new ImdbContext(_connectionString);
        

        return db.Roles.Skip(pageNumber * pageSize).Take(pageSize).ToList();
    }

    public Role? GetRoleById(int roleId)
    {
        var db = new ImdbContext(_connectionString);
        return db.Roles.Find(roleId);
    }

    public int NumberOfRoles()
    {
        var db = new ImdbContext(_connectionString);
        return db.Roles.Count();
    }



}
