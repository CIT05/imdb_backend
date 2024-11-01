using DataLayer.Persons;

namespace DBConnection.Persons
{
    public class PersonDataService(string connectionString) : IPersonDataService
    {
        private readonly string _connectionString = connectionString;

        public List<Person> GetPersons(int pageSize, int pageNumber)
        {
            var db = new ImdbContext(_connectionString);
            return db.Persons.Skip(pageNumber * pageSize).Take(pageSize).ToList();
        }

        public Person? GetPersonById(string tconst)
        {
           var db = new ImdbContext(_connectionString);
            return db.Persons.Find(tconst);
        }

        public int NumberOfPersons()
        {
            var db = new ImdbContext(_connectionString);
            return db.Persons.Count();
        }
    }

}
