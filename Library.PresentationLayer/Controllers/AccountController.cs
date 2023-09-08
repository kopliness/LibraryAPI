using Library.BusinessLayer.Services.Interfaces;
using Library.DataLayer.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Library.PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        private readonly IUserService _userService;

        public AccountController(IAuthenticationService authenticationService, IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }

        [HttpGet("getToken")]
        public async Task<IActionResult> GetToken([FromQuery] UserDto userDto,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var token = await _authenticationService.GetUserTokenAsync(userDto, cancellationToken);

            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserDto userDto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userService.RegisterUserAsync(userDto, cancellationToken);

            return Ok(user);
        }
    }
}