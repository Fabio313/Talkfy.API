using AutoMapper;
using Domain.Entities.v1;
using Domain.Interfaces.Repositories.v1;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata;

namespace Domain.Queries.v1.Users.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdQueryResponse>
    {
        private readonly IUserRepository _userRepository;
        public IMapper Mapper { get; set; }
        public ILogger Logger { get; set; }

        public GetUserByIdQueryHandler(
            IUserRepository userRepository,
            IMapper mapper,
            ILoggerFactory logger)
        {
            _userRepository = userRepository;
            Mapper = mapper;
            Logger = logger.CreateLogger<GetUserByIdQueryHandler>();
        }

        async Task<GetUserByIdQueryResponse> IRequestHandler<GetUserByIdQuery, GetUserByIdQueryResponse>.Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Logger.LogInformation($"Inicio metodo {nameof(GetUserByIdQueryHandler)}.{nameof(Handle)}");

                var response = await _userRepository.GetUserById(request.Id);

                Logger.LogInformation($"Fim metodo {nameof(GetUserByIdQueryHandler)}.{nameof(Handle)}");

                return Mapper.Map<GetUserByIdQueryResponse>(response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Erro metodo {nameof(GetUserByIdQueryHandler)}.{nameof(Handle)}");

                throw;
            }
        }
    }
}
