using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
using RoomBooking.Helpers;
using RoomBooking.Models;
using RoomBooking.Database;
using RoomBooking.Models.Dto;

namespace RoomBooking.Repository;

public class AuthenticationRepository : IAuthenticationRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthenticationRepository> _logger;


    public AuthenticationRepository(ApplicationDbContext dBContext, IConfiguration configuration, ILogger<AuthenticationRepository> logger)
    {
        _dbContext = dBContext;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<User?> ValidateUserAsync(string email, string password)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.email == email);

        if (user == null || !PasswordHelper.VerifyPassword(password, user.password))
        {
            return null;
        }

        return user;
    }

    public string CreateToken(User user)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var privateKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"] ?? "");
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(privateKey),
                SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(1),
                Subject = GenerateClaims(user)
            };

            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex, "Erro ao criar o token: parâmetro nulo");
            throw new Exception("Erro ao criar o token: parâmetro nulo", ex);
        }
        catch (FormatException ex)
        {
            _logger.LogError(ex, "Erro de formato ao criar o token");
            throw new Exception("Erro de formato ao criar o token", ex);
        }
        catch (SecurityTokenException ex)
        {
            _logger.LogError(ex, "Erro ao assinar o token JWT");
            throw new Exception("Erro ao assinar o token JWT", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado ao criar o token");
            throw new Exception("Erro inesperado ao criar o token", ex);
        }

    }

    private ClaimsIdentity GenerateClaims(User user)
    {
        try
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.id.ToString()));
            ci.AddClaim(new Claim(ClaimTypes.Name, user.name));
            ci.AddClaim(new Claim(ClaimTypes.GivenName, user.name));
            ci.AddClaim(new Claim(ClaimTypes.Email, user.email));

            foreach (var role in user.roles)
            {
                ci.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return ci;
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado ao criar o token");
            throw;
        }
    }

    public async Task<dynamic?> GetUserAsync(int id)
    {
        var user = await _dbContext.Users.FindAsync(id);

        if (user == null)
        {
            return null;
        }

        return new
        {
            user.id,
            user.name,
            user.email,
        };
    }

}