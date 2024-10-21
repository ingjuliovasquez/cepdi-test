using MedicamentosUsuariosAPI.Data;
using MedicamentosUsuariosAPI.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MedicamentosUsuariosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;  // Usamos el ApplicationDbContext
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto loginDto)
        {
        /*    var user = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == loginDto.Usuario);

            if (user == null || !VerificarPassword(loginDto.Password, user.Password))*/
            if (loginDto.Usuario != "demo1@mail.com")
            {
                return Unauthorized("Usuario incorrecto");
            }
            if (loginDto.Password != "Demo123#")
            {
                return Unauthorized("Contraseña incorrecta");
            }

            // Generar token JWT
            var token = GenerarToken(loginDto.Usuario);
            return Ok(new { Token = token });
        }

        private bool VerificarPassword(string passwordIngresada, string passwordAlmacenada)
        {
            return passwordIngresada == passwordAlmacenada;
        }

        private string GenerarToken(string usuario)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, usuario),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, usuario.ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddYears(100),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
