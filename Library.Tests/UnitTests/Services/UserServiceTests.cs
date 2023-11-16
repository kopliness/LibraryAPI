namespace Library.Tests.UnitTests.Services;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UserService _userService;
    UserDto userDto = new UserDto
    {
         Login = "TestLogin",
         Password = "TestPassword",
    };
    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _mapperMock = new Mock<IMapper>();

        _userService = new UserService(_userRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task RegisterUserAsync_ShouldRegisterUser_WhenUserDtoIsValid()
    {
        // Arrange
        var userModel = new User();

        _mapperMock.Setup(m => m.Map<UserDto, User>(userDto)).Returns(userModel);

        _userRepositoryMock.Setup(r => r.RegisterUserAsync(userModel, It.IsAny<CancellationToken>()))
            .ReturnsAsync(userModel);

        // Act
        var result = await _userService.RegisterUserAsync(userDto);

        // Assert
        Assert.NotNull(result);
        result.Should().BeEquivalentTo(userModel);
        _userRepositoryMock.Verify(r => r.RegisterUserAsync(userModel, It.IsAny<CancellationToken>()), Times.Once);
    }
}
