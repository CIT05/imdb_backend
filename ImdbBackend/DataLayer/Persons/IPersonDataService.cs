namespace DataLayer.Persons;

public interface IPersonDataService
{

    List<Person> GetPersons(int pageSize, int pageNumber);

    Person? GetPersonById(string personId);

    List<PersonsByMovieResult> GetPersonsByMovie(string tconst);

    int NumberOfPersons();
}
