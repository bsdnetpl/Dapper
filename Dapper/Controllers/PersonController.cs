using ApiDapper.Models;
using ApiDapper.Services;
using Dapper.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ApiDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonServices _personServices;
        private readonly IValidator<Person> _validator;
        private readonly IValidator<PersonDto> _validatorDto;


        public PersonController(IPersonServices personServices, IValidator<Person> validator, IValidator<PersonDto> validatorDto)
        {
            _personServices = personServices;
            _validator = validator;
            _validatorDto = validatorDto;
        }

        [HttpGet]
        public async Task<ActionResult> GetPerson()
        {
            try
            {
                var persons = await _personServices.GetPerson();
                return Ok(persons);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("PersonByFirstName")]
        public async Task<ActionResult> GetPerson(string name)
        {
            try
            {
                var person = await _personServices.GetPerson(name);
                if (person == null)
                    return NotFound();
                return Ok(person);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("AddPerson")]
        public async Task<ActionResult> AddPerson(PersonDto personDto)
        {
            ValidationResult result = await _validatorDto.ValidateAsync(personDto);
            if (result.IsValid)
            {

                try
                {
                    var createdPerson = await _personServices.CreatePerson(personDto);
                    return Ok(createdPerson);
                }
                catch (Exception ex)
                {
                    //log error
                    return StatusCode(500, ex.Message);
                }
            }
            return BadRequest(result);
        }
        [HttpPut("UbdatePerson")]
        public async Task<ActionResult> UpdatePerson(Guid id, PersonUpdateDto personUpdateDto)
        {
            try
            {
                var dbCompany = await _personServices.GetPersonById(id);
                if (dbCompany == null)
                    return NotFound();
                await _personServices.UpdatePerson(id, personUpdateDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("DeletePerson")]
        public async Task<ActionResult> DeletePerson(Guid id)
        {
            try
            {
                var dbCompany = await _personServices.GetPersonById(id);
                if (dbCompany == null)
                    return NotFound();
                await _personServices.DeletePerson(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
