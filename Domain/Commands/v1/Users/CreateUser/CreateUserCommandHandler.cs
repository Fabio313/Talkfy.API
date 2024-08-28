using AutoMapper;
using CrossCutting.Exceptions;
using CrossCutting.Helper;
using Domain.Entities.v1;
using Domain.Interfaces.Repositories.v1;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata;

namespace Domain.Commands.v1.Users.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserCommandResponse>
    {
        private readonly IUserRepository _userRepository;
        public IMapper Mapper { get; set; }
        public ILogger Logger { get; set; }

        public CreateUserCommandHandler(
            IUserRepository userRepository,
            IMapper mapper,
            ILoggerFactory logger)
        {
            _userRepository = userRepository;
            Mapper = mapper;
            Logger = logger.CreateLogger<CreateUserCommandHandler>();
        }

        async Task<CreateUserCommandResponse> IRequestHandler<CreateUserCommand, CreateUserCommandResponse>.Handle(CreateUserCommand request, CancellationToken cancellationToken)
        { 
            try
            {
                Logger.LogInformation($"Inicio metodo {nameof(CreateUserCommandHandler)}.{nameof(Handle)}");

                var user = Mapper.Map<User>(request);

                user.Password = HashingHelper.ComputeSha256Hash(request.Password);

                if ((await _userRepository.GetUsers(email: request.Email)).Count() > 0)
                    throw new BadRequestException("Já existe um usuário com esse e-mail.");

                var response = await _userRepository.CreateUser(user);

                Logger.LogInformation($"Fim metodo {nameof(CreateUserCommandHandler)}.{nameof(Handle)}");

                return Mapper.Map<CreateUserCommandResponse>(response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Erro metodo {nameof(CreateUserCommandHandler)}.{nameof(Handle)}");

                throw;
            }
        }
    }
}
