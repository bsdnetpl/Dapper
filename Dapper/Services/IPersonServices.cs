using ApiDapper.Models;
using Dapper.Models;

namespace ApiDapper.Services
{
    public interface IPersonServices
    {
        Task<IEnumerable<Person>> GetPerson();
        public Task<Person> GetPerson(string FirstName);
        public Task<Person> CreatePerson(PersonDto person);
        public Task<bool>DeletePerson(Guid id);
        public Task<bool> UpdatePerson(Guid id, PersonUpdateDto personUpdateDto);
        public Task<Person> GetPersonById(Guid id);
    }
}