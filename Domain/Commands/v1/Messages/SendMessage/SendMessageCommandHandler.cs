using AutoMapper;
using Domain.Entities.v1;
using Domain.Interfaces.Repositories.v1;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata;

namespace Domain.Commands.v1.Messages.SendMessage
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, bool>
    {
        private readonly IMessageRepository _messageRepository;
        public IMapper Mapper { get; set; }
        public ILogger Logger { get; set; }

        public SendMessageCommandHandler(
            IMessageRepository userRepository,
            IMapper mapper,
            ILoggerFactory logger)
        {
            _messageRepository = userRepository;
            Mapper = mapper;
            Logger = logger.CreateLogger<SendMessageCommandHandler>();
        }

        async Task<bool> IRequestHandler<SendMessageCommand, bool>.Handle(SendMessageCommand request, CancellationToken cancellationToken)
        { 
            try
            {
                Logger.LogInformation($"Inicio metodo {nameof(SendMessageCommandHandler)}.{nameof(Handle)}");

                var message = Mapper.Map<Message>(request);

                var response = await _messageRepository.SendMessage(message);

                Logger.LogInformation($"Fim metodo {nameof(SendMessageCommandHandler)}.{nameof(Handle)}");

                return response;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Erro metodo {nameof(SendMessageCommandHandler)}.{nameof(Handle)}");

                throw;
            }
        }
    }
}
