using MediatR;

namespace Domain.Queries.v1.Messages.GetChat
{
    public class GetChatQuery : IRequest<GetChatQueryResponse>
    {
        public string Sender { get; set; }

        public string Reciver { get; set; }
    }
}
