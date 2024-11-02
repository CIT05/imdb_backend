using DataLayer.Persons;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

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

        public List<PersonsByMovieResult> GetPersonsByMovie(string tconst)
        {
            var db = new ImdbContext(_connectionString);
            return db.PersonsByMovieResult.FromSqlInterpolated($"SELECT * FROM get_actors_for_movie({tconst})").Include(result => result.Person).ToList();
        }


        public int NumberOfPersons()
        {
            var db = new ImdbContext(_connectionString);
            return db.Persons.Count();
        }
    }

}
