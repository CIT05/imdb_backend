using DataLayer.Persons;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace DBConnection.Persons
{
    public class PersonDataService(string connectionString) : IPersonDataService
    {
        private readonly string _connectionString = connectionString;

        public List<Person> GetPersons(int pageSize, int pageNumber)
        {
            var db = new ImdbContext(_connectionString);
            var persons = db.Persons
            .OrderBy(persons => persons.NConst)
            .Include(p => p.PersonRoles)
            .Include(kf => kf.KnownFors)
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .Select(person => new Person
             {
                 NConst = person.NConst,
                 PrimaryName = person.PrimaryName,
                 BirthYear = person.BirthYear,
                 DeathYear = person.DeathYear,
                 PersonRoles = person.PersonRoles.OrderBy(pr => pr.Ordering).ToList(),
                 KnownFors = person.KnownFors.OrderBy(kf => kf.Ordering).ToList()


             })
            .ToList();

            return persons;
        }

        public Person? GetPersonById(string nconst)
        {

                using var db = new ImdbContext(_connectionString);
                var person = db.Persons
                    .Include(p => p.PersonRoles)
                    .ThenInclude(r => r.Role)
                    .Include(kf => kf.KnownFors.OrderBy(kf => kf.Ordering))
                    .ThenInclude(t=> t.Title)
                    .AsSplitQuery()
                    .SingleOrDefault(p => p.NConst == nconst);

                return person;
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
