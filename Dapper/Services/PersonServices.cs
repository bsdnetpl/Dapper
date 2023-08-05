using ApiDapper.DB;
using ApiDapper.Models;
using Dapper;
using Dapper.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace ApiDapper.Services
{
    public class PersonServices : IPersonServices
    {
        private readonly DapperContext _context;
        private readonly IPasswordHasher<Person> _passwordHasher;

        public PersonServices(DapperContext context, IPasswordHasher<Person> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<Person> CreatePerson(PersonDto personDto)
        {
            var query = "INSERT INTO Person (Id, FirstName,LastName,Password,Email,dateTimeCreate) VALUES (@Id, @dateTimeCreate, @FirstName,@LastName,@Password,@Email)";
            var parameters = new DynamicParameters();
            Person person = new Person();

            person.Id = Guid.NewGuid();
            person.dateTimeCreate = DateTime.Now;
            person.FirstName = personDto.FirstName;
            person.LastName = personDto.LastName;
            person.Email = personDto.Email;
            person.Password = _passwordHasher.HashPassword(person,personDto.Password);

            parameters.Add("Id", person.Id, DbType.Guid);
            parameters.Add("dateTimeCreate", person.dateTimeCreate, DbType.DateTime2);
            parameters.Add("FirstName", person.FirstName, DbType.String);
            parameters.Add("LastName", person.LastName, DbType.String);
            parameters.Add("Password", person.Password, DbType.String);
            parameters.Add("Email", person.Email, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
            return person;
        }

        public async Task<IEnumerable<Person>> GetPerson()
        {
            var query = "SELECT * FROM Person";
            using (var connection = _context.CreateConnection())
            {
                var person = await connection.QueryAsync<Person>(query);
                return person.ToList();
            }
        }

        public async Task<Person> GetPerson(string FirstName)
        {
            var query = "SELECT * FROM Person WHERE FirstName = @FirstName";
            using (var connection = _context.CreateConnection())
            {
                var company = await connection.QuerySingleOrDefaultAsync<Person>(query, new { FirstName });
                return company;
            }
        }
    }
}
