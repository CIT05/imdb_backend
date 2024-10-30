using DataLayer.Roles;




namespace DBConnection.Roles;

public class RoleDataService(IRoleRepository roleRepository) : IRoleDataService
{
    private readonly IRoleRepository _roleRepository = roleRepository;

    public List<Role> GetRoles(int pageSize, int pageNumber)
    {
       return this._roleRepository.GetRoles(pageSize, pageNumber);
    }

    public Role? GetRoleById(int roleId)
    {
        return this._roleRepository.GetRoleById(roleId);        
    }

    public int NumberOfRoles()
    {
        return this._roleRepository.NumberOfRoles();
    }


}
