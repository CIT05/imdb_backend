using DataLayer.PersonRoles;
using Microsoft.EntityFrameworkCore;


namespace DBConnection.PersonRoles
{
    public class PersonRoleDataService(string connectionString) : IPersonRoleDataService
    {
        private readonly string _connectionString = connectionString;

        public List<PersonRole> GetPersonRoles(int pageSize, int pageNumber)
        {
            var db = new ImdbContext(_connectionString);
            return db.PersonRoles.Skip(pageNumber * pageSize).Take(pageSize).ToList();
        }

        public List<PersonRole> GetRoleDetailsByPersonId(string nconst)
        {
            var db = new ImdbContext(_connectionString);
            return db.PersonRoles
                     .Where(pr => pr.NConst == nconst)
                     .ToList();
        }

        public PersonRole GetRoleImportanceOfPerson(string nconst, int roleid)
        {
            var db = new ImdbContext(_connectionString);
            return db.PersonRoles.Find(nconst, roleid);
        }
            

        public int NumberOfPersonRoles()
        {
            var db = new ImdbContext(_connectionString);
            return db.PersonRoles.Count();
        }
    }

}
