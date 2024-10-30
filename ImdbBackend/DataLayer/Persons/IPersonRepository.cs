

namespace DataLayer.Persons;

public interface IPersonRepository
{
    List<Person> GetPersons(int pageSize, int pageNumber);

    Person? GetPersonById(string personId);

    int NumberOfPersons();
}
