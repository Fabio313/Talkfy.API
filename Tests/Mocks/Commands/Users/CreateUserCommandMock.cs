using Bogus;
using Domain.Commands.v1.Users.CreateUser;

namespace Tests.Mocks.Commands.Users
{
    public static class CreateUserCommandMock
    {
        public static CreateUserCommand GenerateDefault() 
        {
            return new Faker<CreateUserCommand>()
                .RuleFor(fake => fake.Username, faker => faker.Random.String2(15))
                .RuleFor(fake => fake.Password, faker => faker.Random.String2(10))
                .RuleFor(fake => fake.Email, faker => faker.Internet.Email())
                .RuleFor(fake => fake.CreatedDate, DateTime.Now);
        }
    }
}