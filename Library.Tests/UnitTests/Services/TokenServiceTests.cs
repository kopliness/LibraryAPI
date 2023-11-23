namespace Library.Tests.UnitTests.Services;

public class TokenServiceTests
{
    private readonly Mock<IOptions<JwtOptions>> _jwtOptionsMock;
    private readonly TokenService _tokenService;

    public TokenServiceTests()
    {
        _jwtOptionsMock = new Mock<IOptions<JwtOptions>>();
        _jwtOptionsMock.Setup(j => j.Value).Returns(new JwtOptions
        {
            SecretKey = "this is my custom Secret key for authneticat ion",
            Audience = "Audience",
            Issuer = "Issuer"
        });

        _tokenService = new TokenService(_jwtOptionsMock.Object);
    }

    [Fact]
    public void GenerateTokenAsync_ShouldGenerateToken_WhenClaimsAreValid()
    {
        // Arrange
        var claims = new List<Claim>
        {
            new("unique_name", "test user")
        };


        // Act
        var result = _tokenService.GenerateTokenAsync(claims);

        // Assert
        Assert.NotNull(result);

        // Decode token and validate claims
        var handler = new JwtSecurityTokenHandler();
        var testUser = "test user";
        var tokenS = handler.ReadToken(result) as JwtSecurityToken;

        tokenS.Should().NotBeNull();
        testUser.Should().BeEquivalentTo(tokenS.Claims.First(claim => claim.Type == "unique_name").Value);
    }
}