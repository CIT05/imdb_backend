namespace DataLayer.Roles;

public interface IRoleRepository
{
    
    List<Role> GetRoles(int pageSize, int pageNumber);

    Role? GetRoleById(int roleId);

    int NumberOfRoles();

}
