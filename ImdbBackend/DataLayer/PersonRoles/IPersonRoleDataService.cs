namespace DataLayer.PersonRoles;

public interface IPersonRoleDataService
{

    List<PersonRole> GetPersonRoles(int pageSize, int pageNumber);

    List<PersonRole> GetRoleDetailsByPersonId(string personId);

    PersonRole GetRoleImportanceOfPerson(string personId, int roleId);

    int NumberOfPersonRoles();
}
