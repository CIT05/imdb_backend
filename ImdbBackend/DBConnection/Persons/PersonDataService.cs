using DataLayer.Persons;
using Microsoft.EntityFrameworkCore;

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

        public Person? GetPersonById(string nconst)
        {

                using var db = new ImdbContext(_connectionString);
                var person = db.Persons
                    .Include(p => p.PersonRoles)
                    .SingleOrDefault(p => p.NConst == nconst);

                return person;
        }

        public int NumberOfPersons()
        {
            var db = new ImdbContext(_connectionString);
            return db.Persons.Count();
        }
    }

}
