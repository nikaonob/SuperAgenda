using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SuperAgenda.Api.Data;
using SuperAgenda.Api.Models;
using SuperAgenda.Api.Models.Auth;

namespace SuperAgenda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly AgendaDbContext _context;
    private readonly IConfiguration _config;
    private readonly PasswordHasher<Usuario> _hasher = new();

    public AuthController(AgendaDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Name == request.Name);
        if (usuario is null || usuario.Password is null)
        {
            return Unauthorized(new { message = "Usuario o contraseña incorrectos" });
        }

        var result = _hasher.VerifyHashedPassword(usuario, usuario.Password, request.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            return Unauthorized(new { message = "Usuario o contraseña incorrectos" });
        }

        var expireMinutes = int.Parse(_config["Jwt:ExpireMinutes"] ?? "43200");
        var expiresAt = DateTime.UtcNow.AddMinutes(expireMinutes);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Name ?? string.Empty)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: expiresAt,
            signingCredentials: creds);

        return new LoginResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            UserId = usuario.Id,
            UserName = usuario.Name ?? string.Empty,
            ExpiresAt = expiresAt
        };
    }
}
