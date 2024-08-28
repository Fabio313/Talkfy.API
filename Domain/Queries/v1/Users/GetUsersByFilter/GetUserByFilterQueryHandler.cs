using AutoMapper;
using Domain.Entities.v1;
using Domain.Interfaces.Repositories.v1;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata;

namespace Domain.Queries.v1.Users.GetUsersByFilter
{
    public class GetUserByFilterQueryHandler : IRequestHandler<GetUserByFilterQuery, GetUserByFilterQueryResponse>
    {
        private readonly IUserRepository _userRepository;
        public IMapper Mapper { get; set; }
        public ILogger Logger { get; set; }

        public GetUserByFilterQueryHandler(
            IUserRepository userRepository,
            IMapper mapper,
            ILoggerFactory logger)
        {
            _userRepository = userRepository;
            Mapper = mapper;
            Logger = logger.CreateLogger<GetUserByFilterQueryHandler>();
        }

        async Task<GetUserByFilterQueryResponse> IRequestHandler<GetUserByFilterQuery, GetUserByFilterQueryResponse>.Handle(GetUserByFilterQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Logger.LogInformation($"Inicio metodo {nameof(GetUserByFilterQueryHandler)}.{nameof(Handle)}");

                var response = await _userRepository.GetUsers(request.Username, request.Email);

                Logger.LogInformation($"Fim metodo {nameof(GetUserByFilterQueryHandler)}.{nameof(Handle)}");

                return new GetUserByFilterQueryResponse() { Users = Mapper.Map<IEnumerable<UsersResponse>>(response) };
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Erro metodo {nameof(GetUserByFilterQueryHandler)}.{nameof(Handle)}");

                throw;
            }
        }
    }
}
