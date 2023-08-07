using ApiDapper.Controllers;
using ApiDapper.Services;
using Dapper.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace ApiDapper.Tests
{
    public class PersonControllerTest
    {
        private static List<Person> GetFakePersonList()
        {
            return new List<Person>()
           {  
         new Person
            {
                Id = Guid.NewGuid(),
                dateTimeCreate = DateTime.Now,
                Email = "email@gmail.com",
                FirstName = "xxx",
                LastName = "xxx",
                Password = "123123"
            } 
            };
        }

        [Fact]
        public async Task TestGetPersonReturnPerson()
        {
            var mocGetPerson = new Mock<IPersonServices>();
            mocGetPerson.Setup(m => m.GetPerson().Result).Returns(PersonControllerTest.GetFakePersonList);
            var result = mocGetPerson.Object.GetPerson();

            Assert.NotNull(result);
        }
    }
}