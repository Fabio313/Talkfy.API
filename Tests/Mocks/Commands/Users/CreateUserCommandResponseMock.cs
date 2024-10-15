using Bogus;
using Domain.Commands.v1.Users.CreateUser;

namespace Tests.Mocks.Commands.Users
{
    public static class CreateUserCommandResponseMock
    {
        public static CreateUserCommandResponse GenerateDefault()
        {
            return new Faker<CreateUserCommandResponse>()
                .RuleFor(x => x.Id, faker => faker.Random.Guid().ToString())
                .RuleFor(x => x.Username, faker => faker.Random.String2(15))
                .RuleFor(x => x.Email, faker => faker.Internet.Email())
                .RuleFor(x => x.CreatedDate, faker => faker.Date.Recent(1));
        }
    }
}
