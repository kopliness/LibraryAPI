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
    public async Task GenerateTokenAsync_ShouldGenerateToken_WhenClaimsAreValid()
    {
        // Arrange
        var claims = new List<Claim>
        {
          new Claim("unique_name", "test user")
        };


        // Act
        var result = await _tokenService.GenerateTokenAsync(claims);

        // Assert
        Assert.NotNull(result);

        // Decode token and validate claims
        var handler = new JwtSecurityTokenHandler();
        var tokenS = handler.ReadToken(result) as JwtSecurityToken;

        Assert.NotNull(tokenS);
        Assert.Equal("test user", tokenS.Claims.First(claim => claim.Type == "unique_name").Value);
    }
}
