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

        public List<PersonByMovieResultWithPerson> GetPersonsByMovie(string tconst)
        {
            var db = new ImdbContext(_connectionString);
            var persons = db.PersonsByMovieResult.FromSqlInterpolated($"SELECT * FROM get_actors_for_movie({tconst})").ToList();

            var personByMovieResultWithPerson = persons.Select(person =>
            {
                return new PersonByMovieResultWithPerson
                {
                    Person = GetPersonById(person.NConst),
                    PersonRating = person.PersonRating,
                    NConst = person.NConst,


                };
             }).ToList();

            return personByMovieResultWithPerson;
        }


        public int NumberOfPersons()
        {
            var db = new ImdbContext(_connectionString);
            return db.Persons.Count();
        }
    }

}
