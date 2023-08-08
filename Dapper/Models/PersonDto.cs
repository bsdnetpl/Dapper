using Dapper.Models;
using FluentValidation;

namespace ApiDapper.Models
{
    public class PersonDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
    public class PersonValidatorDto : AbstractValidator<PersonDto>
    {
        public PersonValidatorDto()
        {
            RuleFor(v => v.Email).EmailAddress().WithMessage("Wrong error adress");

        }
    }
}
