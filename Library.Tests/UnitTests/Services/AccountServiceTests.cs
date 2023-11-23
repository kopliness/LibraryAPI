namespace Library.Tests.UnitTests.Services;

public class AccountServiceTests
{
    private readonly Mock<IAccountRepository> _accountRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly AccountService _accountService;
    private readonly Mock<ILogger<AccountService>> _loggerMock;
    AccountDto _accountDto = new ()
    {
        Login = "TestLogin",
        Password = "TestPassword",
    };
    public AccountServiceTests()
    {
        _accountRepositoryMock = new Mock<IAccountRepository>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<AccountService>>();

        _accountService = new AccountService(_accountRepositoryMock.Object, _mapperMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task RegisterAccountAsync_ShouldRegisterAccount_WhenAccountDtoIsValid()
    {
        // Arrange
        var accountModel = new Account
        {
            Login = "TestLogin",
            Password = "TestPassword"
        };
        _mapperMock.Setup(m => m.Map<AccountDto, Account>(_accountDto)).Returns(accountModel);
        _mapperMock.Setup(m => m.Map<Account, AccountDto>(accountModel)).Returns(_accountDto);

        _accountRepositoryMock.Setup(r => r.RegisterAccountAsync(accountModel, It.IsAny<CancellationToken>()))
            .ReturnsAsync(accountModel);


        // Act
        var result = await _accountService.RegisterAccountAsync(_accountDto);

        // Assert
        result.Should().NotBeNull();
        result.Login.Should().BeEquivalentTo(accountModel.Login);
        _accountRepositoryMock.Verify(r => r.RegisterAccountAsync(accountModel, It.IsAny<CancellationToken>()), Times.Once);
    }

}

