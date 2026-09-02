using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AINexus.Api.Controllers;

[ApiController, Route("api/v1/auth")]
public sealed class AuthController(IConfiguration config) : ControllerBase
{
    [HttpPost("token")]
    public IActionResult Token(LoginRequest request)
    {
        if (request.Username != config["DemoCredentials:Username"] || request.Password != config["DemoCredentials:Password"])
            return Unauthorized();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Authentication:SigningKey"]!));
        var token = new JwtSecurityToken(config["Authentication:Issuer"] ?? "AINexus", config["Authentication:Audience"] ?? "AINexus.Api", [new Claim(ClaimTypes.Name, request.Username), new Claim(ClaimTypes.Role, "User")], expires: DateTime.UtcNow.AddHours(1), signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
        return Ok(new { access_token = new JwtSecurityTokenHandler().WriteToken(token), token_type = "Bearer", expires_in = 3600 });
    }
}
public sealed record LoginRequest(string Username, string Password);
