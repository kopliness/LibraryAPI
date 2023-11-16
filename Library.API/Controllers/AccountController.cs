using Library.Business.Dto;
using Library.Business.Services.Interfaces;
using Library.Business.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.API.Controllers
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
        [SwaggerOperation(Summary = "Authorize", Description = "Authorize and receive a token for access")]
        [SwaggerResponse(200, "Return a token", typeof(List<AuthorReadDto>))]
        [SwaggerResponse(404, "User not found")]
        [SwaggerResponse(500, "If there is an internal server error")]
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
        [SwaggerOperation(Summary = "Registration", Description = "Registration in the service")]
        [SwaggerResponse(200, "Return a user")]
        [SwaggerResponse(500, "If there is an internal server error")]
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