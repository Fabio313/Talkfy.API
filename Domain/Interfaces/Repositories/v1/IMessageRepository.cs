using Domain.Entities.v1;

namespace Domain.Interfaces.Repositories.v1
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetChat(string? sender = "", string? reciver = "");
        Task<bool> SendMessage(Message user);
    }
}
