using Microsoft.AspNetCore.Mvc;
using RoomBooking.Repository;
using RoomBooking.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RoomBooking.Controllers
{
    [Route("api/v1/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository _authRepository;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IAuthenticationRepository authRepository, ILogger<AuthenticationController> logger)
        {
            _authRepository = authRepository;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> AuthenticateUser([FromBody] LoginDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (dto == null || string.IsNullOrEmpty(dto.email) || string.IsNullOrEmpty(dto.password))
                {
                    return BadRequest("Invalid client request");
                }

                var user = await _authRepository.ValidateUserAsync(dto.email, dto.password);

                if (user == null)
                {
                    return Unauthorized();
                }

                var token = _authRepository.CreateToken(user);

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return Problem(
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: "Internal Server Error"
                );
            }
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetUserData()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new { Message = "Token inválido" });
            }

            var user = await _authRepository.GetUserAsync(userId);

            if (user == null)
            {
                return NotFound(new { Message = "Usuário não encontrado" });
            }

            return Ok(user);
        }
    }
}