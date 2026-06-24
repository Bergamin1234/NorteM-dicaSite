using Microsoft.AspNetCore.Mvc;
using Nortemedica.API.Contracts.Authentication;

namespace Nortemedica.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        // Lógica de registro com ASP.NET Core Identity virá aqui.
        await Task.CompletedTask; // Placeholder
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        // Lógica de login e geração de token JWT virá aqui.
        await Task.CompletedTask; // Placeholder
        var response = new AuthResponse("fake-jwt-token", request.Email);
        return Ok(response);
    }
}