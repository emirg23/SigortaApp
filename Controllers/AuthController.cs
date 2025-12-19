using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SigortaApp.DTOs;
using SigortaApp.Services.Interfaces;

namespace SigortaApp.Controllers
{
[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public AuthController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDTO input)
    {
        var user = await _userService.ValidateUserAsync(input.Email, input.Password);
        if (user == null)
            return Unauthorized();

        var token = _tokenService.GenerateToken(user);
        return Ok(new { token });
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterUserDTO input)
    {
        var user = await _userService.RegisterAsync(input);
        return Ok(new { user.Id, user.Email });
    }
}

}