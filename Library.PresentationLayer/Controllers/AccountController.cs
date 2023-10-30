using Library.BusinessLayer.Dto;
using Library.BusinessLayer.Services.Interfaces;
using Library.BusinessLayer.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Library.PresentationLayer.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        private readonly IUserService _userService;

        private readonly UserValidator _userValidator;

        public AccountController(IAuthenticationService authenticationService, IUserService userService,
            UserValidator userValidator)
        {
            _authenticationService = authenticationService;
            _userService = userService;
            _userValidator = userValidator;
        }

        [HttpGet("sign-in")]
        public async Task<IActionResult> GetToken([FromQuery] UserDto userDto,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var validationResult = _userValidator.Validate(userDto);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var token = await _authenticationService.GetUserTokenAsync(userDto, cancellationToken);

            return Ok(token);
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> RegisterUser(UserDto userDto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var validationResult = _userValidator.Validate(userDto);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            var user = await _userService.RegisterUserAsync(userDto, cancellationToken);

            return Ok(user);
        }
    }
}