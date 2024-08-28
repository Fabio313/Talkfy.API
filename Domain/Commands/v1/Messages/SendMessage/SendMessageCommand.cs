using MediatR;
using System.Text.Json.Serialization;

namespace Domain.Commands.v1.Messages.SendMessage
{
    public class SendMessageCommand : IRequest<bool>
    {
        public string Sender { get; set; }

        public string Reciver { get; set; }

        public string Text { get; set; }

        [JsonIgnore]
        public DateTime SendDate { get; set; } = DateTime.Now;
    }
}
