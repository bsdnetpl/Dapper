using ApiDapper.Controllers;
using ApiDapper.Models;
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
        [Fact]
        public async Task TestGetPersonByNameReturnPerson()
        {
          Person pers =  new Person
            {
                Id = Guid.NewGuid(),
                dateTimeCreate = DateTime.Now,
                Email = "email@gmail.com",
                FirstName = "xxx",
                LastName = "xxx",
                Password = "123123"
            };
            var mocGetPersonByName = new Mock<IPersonServices>();
            mocGetPersonByName.Setup(m => m.GetPerson("xxx").Result).Returns(pers);
            var result = mocGetPersonByName.Object.GetPerson("xxx");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestCreatePersonReturnPerson()
        {
            PersonDto pers = new PersonDto
            {
                Email = "email@gmail.com",
                FirstName = "xxx",
                LastName = "xxx",
                Password = "123123"
            };

            Person perss = new Person
            {
                Id = Guid.NewGuid(),
                dateTimeCreate = DateTime.Now,
                Email = "email@gmail.com",
                FirstName = "xxx",
                LastName = "xxx",
                Password = "123123"
            };

            var mocGetPersonByName = new Mock<IPersonServices>();
            mocGetPersonByName.Setup(m => m.CreatePerson(pers).Result).Returns(perss);
            var result = mocGetPersonByName.Object.CreatePerson(pers);

            Assert.Equal(perss.FirstName, result.Result.FirstName);
        }

        [Fact]
        public async Task TestGetPersonByIdReturnPerson()
        {
            Person pers = new Person
            {
                Id = Guid.NewGuid(),
                dateTimeCreate = DateTime.Now,
                Email = "email@gmail.com",
                FirstName = "xxx",
                LastName = "xxx",
                Password = "123123"
            };
            var mocGetPersonByName = new Mock<IPersonServices>();
            mocGetPersonByName.Setup(m => m.GetPersonById(pers.Id).Result).Returns(pers);
            var result = mocGetPersonByName.Object.GetPersonById(pers.Id);

            Assert.Equal("xxx", result.Result.FirstName);
        }
        [Fact]
        public async Task TestDeletePersonReturnTrue()
        {
            Person pers = new Person
            {
                Id = Guid.NewGuid(),
                dateTimeCreate = DateTime.Now,
                Email = "email@gmail.com",
                FirstName = "xxx",
                LastName = "xxx",
                Password = "123123"
            };
            var mocGetPersonByName = new Mock<IPersonServices>();
            mocGetPersonByName.Setup(m => m.DeletePerson(pers.Id).Result).Returns(true);
            var result = mocGetPersonByName.Object.DeletePerson(pers.Id);

            Assert.True(result.Result);
        }
        [Fact]
        public async Task TestUpdatePersonReturnTrue()
        {
            PersonUpdateDto persUpDto = new PersonUpdateDto
            {
                Email = "email@gmail.com",
                FirstName = "xxxx",
                LastName = "xxxx",
                Password = "1231231"
            };
            Person pers = new Person
            {
                Id = Guid.NewGuid(),
                dateTimeCreate = DateTime.Now,
                Email = "email@gmail.com",
                FirstName = "xxx",
                LastName = "xxx",
                Password = "123123"
            };

            var mocGetPersonByName = new Mock<IPersonServices>();
            mocGetPersonByName.Setup(m => m.UpdatePerson(pers.Id,persUpDto).Result).Returns(true);
            var result = mocGetPersonByName.Object.UpdatePerson(pers.Id, persUpDto);

            Assert.True(result.Result);
        }
    }
}