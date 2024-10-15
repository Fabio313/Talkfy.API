using AutoMapper;
using CrossCutting.Exceptions;
using Domain.Commands.v1.Users.CreateUser;
using Domain.Entities.v1;
using Domain.Interfaces.Repositories.v1;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using Tests.Mocks.Commands.Users;
using Tests.Mocks.Entities;
using Xunit;

namespace Tests.Tests.Commands.Users
{
    public class CreateUserCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepository;
        private Mock<IMapper> _mapper;
        private readonly Mock<ILoggerFactory> _loggerFactory;
        public Mock<ILogger<CreateUserCommandHandler>> _logger;

        public CreateUserCommandHandlerTests()
        {
            _userRepository = new Mock<IUserRepository>();
            _mapper = new Mock<IMapper>();
            _loggerFactory = new Mock<ILoggerFactory>();
            _logger = new Mock<ILogger<CreateUserCommandHandler>>();

            _loggerFactory.Setup(x => x.CreateLogger(It.IsAny<string>()))
                .Returns(_logger.Object);
        }

        public CreateUserCommandHandler CreateHandler()
        {
            return new CreateUserCommandHandler(
                _userRepository.Object,
                _mapper.Object,
                _loggerFactory.Object);
        }

        [Fact (DisplayName="Deve criar usuário")]
        public async Task ShouldCreateUser()
        {
            var handler = CreateHandler();

            var request = (IRequestHandler<CreateUserCommand, CreateUserCommandResponse>) handler;

            var command = CreateUserCommandMock.GenerateDefault();

            var user = UserMock.GenerateDefault();

            var commandResponse = CreateUserCommandResponseMock.GenerateDefault();

            _mapper.Setup(m => m.Map<User>(command)).Returns(user);
            _mapper.Setup(m => m.Map<CreateUserCommandResponse>(user)).Returns(commandResponse);

            _userRepository.Setup(repo => repo.GetUsers(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new List<User>());

            _userRepository.Setup(repo => repo.CreateUser(user)).ReturnsAsync(user);

            var response = await request.Handle(command, It.IsAny<CancellationToken>());

            Assert.NotNull(response);
            _userRepository.Verify(repo => repo.CreateUser(It.IsAny<User>()), Times.Once);
        }

        [Fact(DisplayName = "Não Deve criar, usuário já existe")]
        public async Task ShouldNotCreateUserAlreadyExists()
        {
            var handler = CreateHandler();

            var request = (IRequestHandler<CreateUserCommand, CreateUserCommandResponse>)handler;

            var command = CreateUserCommandMock.GenerateDefault();

            var user = UserMock.GenerateDefault();

            var commandResponse = CreateUserCommandResponseMock.GenerateDefault();

            _mapper.Setup(m => m.Map<User>(command)).Returns(user);
            _mapper.Setup(m => m.Map<CreateUserCommandResponse>(user)).Returns(commandResponse);

            _userRepository.Setup(repo => repo.GetUsers(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new List<User> {user});

            _userRepository.Setup(repo => repo.CreateUser(user)).ReturnsAsync(user);


            await Assert.ThrowsAsync<BadRequestException>(() => request.Handle(command, new CancellationToken()));
            _userRepository.Verify(repo => repo.CreateUser(It.IsAny<User>()), Times.Never);
        }
    }
}