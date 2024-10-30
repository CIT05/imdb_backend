using DataLayer.Roles;
using DbConnection.Roles;

namespace DBConnection.Roles;

public class RoleRepository(string connectionString) : IRoleRepository
{
    private readonly string _connectionString = connectionString;

    public List<Role> GetRoles(int pageSize, int pageNumber)
    {
        var db = new RoleContext(_connectionString);
        

        return db.Roles.Skip(pageNumber * pageSize).Take(pageSize).ToList();
    }

    public Role? GetRoleById(int roleId)
    {
        var db = new RoleContext(_connectionString);
        return db.Roles.Find(roleId);
    }

    public int NumberOfRoles()
    {
        var db = new RoleContext(_connectionString);
        return db.Roles.Count();
    }



}
