using FluentValidation;

namespace Dapper.Models
{
    public class Person
    {
        public Guid Id { set; get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime dateTimeCreate { get; set; }
    }

    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(v => v.dateTimeCreate).NotNull();
            RuleFor(v => v.Email).EmailAddress();

        }
    }
}
