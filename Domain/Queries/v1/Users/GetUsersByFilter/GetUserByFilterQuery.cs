using MediatR;

namespace Domain.Queries.v1.Users.GetUsersByFilter
{
    public class GetUserByFilterQuery : IRequest<GetUserByFilterQueryResponse>
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
    }
}
