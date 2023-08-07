using ApiDapper.DB;
using ApiDapper.Models;
using Dapper;
using Dapper.Models;
using Microsoft.AspNetCore.Identity;
using System;
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
            var query = "INSERT INTO Person (Id, FirstName,LastName,Password,Email,dateTimeCreate) VALUES (@Id, @FirstName,@LastName,@Password,@Email,@dateTimeCreate)";
            var parameters = new DynamicParameters();
            Person person = new Person();

            person.Id = Guid.NewGuid();
            person.dateTimeCreate = DateTime.Now.Date;
            person.FirstName = personDto.FirstName;
            person.LastName = personDto.LastName;
            person.Email = personDto.Email;
            person.Password = _passwordHasher.HashPassword(person,personDto.Password);

            parameters.Add("Id", person.Id, DbType.Guid);
            parameters.Add("dateTimeCreate", person.dateTimeCreate, DbType.DateTime);
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
                var person = await connection.QuerySingleOrDefaultAsync<Person>(query, new { FirstName });
                return person;
            }
        }

        public async Task<bool> UpdatePerson(Guid id, PersonUpdateDto personUpdateDto)
        {
            var query = "UPDATE Person SET FirstName = @FirstName, LastName = @LastName, Email = @Email , Password = @Password WHERE Id = @Id";
            var parameters = new DynamicParameters();
            Person person = new Person();
            person.Password = _passwordHasher.HashPassword(person, personUpdateDto.Password);

            parameters.Add("Id", id, DbType.Guid);
            parameters.Add("FirstName", personUpdateDto.FirstName, DbType.String);
            parameters.Add("LastName", personUpdateDto.LastName, DbType.String);
            parameters.Add("Email", personUpdateDto.Email, DbType.String);
            parameters.Add("Password", person.Password, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
            return true;
        }

        public async Task<bool> DeletePerson(Guid id)
        {
            var query = "DELETE FROM Person WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
            return true;
        }

        public async Task<Person> GetPersonById(Guid id)
        {
            var query = "SELECT * FROM Person WHERE Id = @id";
            using (var connection = _context.CreateConnection())
            {
                var person = await connection.QuerySingleOrDefaultAsync<Person>(query, new { id });
                return person;
            }
        }
    }
}
