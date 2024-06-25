using QuestaoCinco.Application.Common.Behaviours;
using QuestaoCinco.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using QuestaoCinco.Application.ContasCorrentes.Commands.CreateContaCorrente;

namespace QuestaoCinco.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    private Mock<ILogger<CreateContaCorrenteCommand>> _logger = null!;
    private Mock<IUser> _user = null!;
    private Mock<IIdentityService> _identityService = null!;

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<CreateContaCorrenteCommand>>();
        _user = new Mock<IUser>();
        _identityService = new Mock<IIdentityService>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _user.Setup(x => x.Id).Returns(Guid.NewGuid().ToString());

        var requestLogger = new LoggingBehaviour<CreateContaCorrenteCommand>(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new CreateContaCorrenteCommand
        {
            Numero = 123,
            Nome = "Fábio",
            Ativo = true
        }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        var requestLogger = new LoggingBehaviour<CreateContaCorrenteCommand>(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new CreateContaCorrenteCommand
        {
            Numero = 123,
            Nome = "Fábio",
            Ativo = true
        }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
    }
}
