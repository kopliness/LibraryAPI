namespace Library.Tests.UnitTests.Services;

public class AuthenticationServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<AuthenticationService>> _loggerMock;
    private readonly AuthenticationService _authenticationService;
    UserDto userDto = new UserDto
    {
        Login = "TestLogin",
        Password = "TestPassword",
    };

    public AuthenticationServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _tokenServiceMock = new Mock<ITokenService>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<AuthenticationService>>();

        _authenticationService = new AuthenticationService(_userRepositoryMock.Object, _tokenServiceMock.Object, _mapperMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task GetUserTokenAsync_ShouldGetUserToken_WhenUserDtoIsValid()
    {
        // Arrange
        var userModel = new User
        {
            Login = "sdgdgdsgdf",
            Password = "dfgdfgfd"
        };

        _mapperMock.Setup(m => m.Map<UserDto, User>(userDto)).Returns(userModel);

        _userRepositoryMock.Setup(r => r.GetUserAsync(userModel, It.IsAny<CancellationToken>()))
            .ReturnsAsync(userModel);

        _tokenServiceMock.Setup(t => t.GenerateTokenAsync(It.IsAny<List<Claim>>()))
            .ReturnsAsync("token");

        // Act
        var result = await _authenticationService.GetUserTokenAsync(userDto);

        // Assert
        Assert.NotNull(result);
        result.Should().Be("token");
        _userRepositoryMock.Verify(r => r.GetUserAsync(userModel, It.IsAny<CancellationToken>()), Times.Once);
        _tokenServiceMock.Verify(t => t.GenerateTokenAsync(It.IsAny<List<Claim>>()), Times.Once);
    }
}

