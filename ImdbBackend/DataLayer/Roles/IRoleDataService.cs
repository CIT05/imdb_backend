

namespace DataLayer.Roles;

public interface IRoleDataService
{

    List<Role> GetRoles(int pageSize, int pageNumber);

    Role? GetRoleById(int roleId);

    int NumberOfRoles();
}
