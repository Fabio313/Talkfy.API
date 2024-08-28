using MediatR;

namespace Domain.Queries.v1.Users.GetUserById
{
    public class GetUserByIdQuery : IRequest<GetUserByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}
