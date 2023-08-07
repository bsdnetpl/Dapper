using ApiDapper.Controllers;
using ApiDapper.Services;
using Dapper.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace ApiDapper.Tests
{
    public class PersonControllerTest
    {
        [Fact]
        public void TestGetPersonReturnPerson()

        {
            var mocGetPerson = new Mock<IPersonServices>();
            mocGetPerson.Setup(m => m.GetPerson().Result).Returns(Task.FromResult<IEnumerable<Person>>) ;
            IPersonServices personServices = mocGetPerson.Object;

            Assert.NotNull(personServices);
        }
    }
}