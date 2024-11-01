namespace DataLayer.Persons;

public interface IPersonDataService
{

    List<Person> GetPersons(int pageSize, int pageNumber);

    Person? GetPersonById(string personId);

    int NumberOfPersons();
}
