namespace Library.Tests.UnitTests.Services;

public class AuthenticationServiceTests
{
    private readonly Mock<IAccountRepository> _accountRepositoryMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<AuthenticationService>> _loggerMock;
    private readonly AuthenticationService _authenticationService;

    public AuthenticationServiceTests()
    {
        _accountRepositoryMock = new Mock<IAccountRepository>();
        _tokenServiceMock = new Mock<ITokenService>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<AuthenticationService>>();

        _authenticationService = new AuthenticationService(_accountRepositoryMock.Object, _tokenServiceMock.Object,
            _mapperMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task GetAccountTokenAsync_ValidAccount_ReturnsToken()
    {
        // Arrange
        var accountDto = new AccountDto { Login = "test", Password = "hashedPassword" };
        var salt = RandomNumberGenerator.GetBytes(16);
        var pbkdf2 = new Rfc2898DeriveBytes(accountDto.Password, salt, 100000, HashAlgorithmName.SHA256);
        var hashedPassword = pbkdf2.GetBytes(32);
        var account = new Account
            { Login = "test", Password = Convert.ToBase64String(hashedPassword), Salt = Convert.ToBase64String(salt) };
        var token = "token";

        _mapperMock.Setup(m => m.Map<AccountDto, Account>(accountDto)).Returns(account);
        _accountRepositoryMock.Setup(r => r.GetAccountAsync(account, It.IsAny<CancellationToken>()))
            .ReturnsAsync(account);
        _tokenServiceMock.Setup(t =>
            t.GenerateTokenAsync(It.Is<List<Claim>>(c =>
                c.Count == 1 && c[0].Type == ClaimTypes.Name && c[0].Value == account.Login))).Returns(token);

        // Act
        var result = await _authenticationService.GetAccountTokenAsync(accountDto);

        // Assert
        result.Should().Be(token);
    }
}