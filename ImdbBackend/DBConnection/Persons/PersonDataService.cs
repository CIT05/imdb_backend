

using DataLayer.Persons;

namespace DBConnection.Persons
{
    public class PersonDataService(IPersonRepository PersonRepository): IPersonDataService
    {

        private readonly IPersonRepository _PersonRepository = PersonRepository;

        public List<Person> GetPersons(int pageSize, int pageNumber)
        {
           return _PersonRepository.GetPersons(pageSize, pageNumber);
        }

        public Person? GetPersonById(string tconst)
        {
            return _PersonRepository.GetPersonById(tconst);
        }

        public int NumberOfPersons()
        {
            return _PersonRepository.NumberOfPersons();
        }
    }
}
