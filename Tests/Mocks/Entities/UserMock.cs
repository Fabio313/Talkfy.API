using Bogus;
using Domain.Entities.v1;

namespace Tests.Mocks.Entities
{
    public static class UserMock
    {
        public static User GenerateDefault()
        {
            return new Faker<User>()
                .RuleFor(fake => fake.Username, faker => faker.Random.String2(15))
                .RuleFor(fake => fake.Password, faker => faker.Random.String2(10))
                .RuleFor(fake => fake.Email, faker => faker.Internet.Email())
                .RuleFor(fake => fake.CreatedDate, DateTime.Now);
        }
    }
}